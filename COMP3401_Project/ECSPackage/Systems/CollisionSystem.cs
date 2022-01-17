using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses HitBox Components to test whether entities have collided with other entities
    /// Author: William Smith & Marc Price
    /// Date: 17/01/22
    /// </summary>
    /// <REFERENCE> Price, M. (2021) ‘Session 16 - Collision Management’, Games Design & Engineering: Sessions. Available at: https://worcesterbb.blackboard.com. (Accessed: 14 January 2022).</REFERENCE>
    public class CollisionSystem : IInitialiseICollisionResponder, IInitialiseIROIEntityDictionary, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        private IReadOnlyDictionary<int, IEntity> _roEntityDict;

        // DECLARE an IDictionary<int, IContainHitBox>, name it '_hitBoxCompDict':
        private IDictionary<int, IContainHitBox> _hitBoxCompDict;

        // DECLARE an IDictionary<int, ITexture>, name it '_textureDict':
        private IDictionary<int, ITexture> _textureCompDict;

        // DECLARE an IDictionary<int, IPosition>, name it '_transformCompDict':
        private IDictionary<int, IPosition> _transformCompDict;

        // DECLARE an ICollisionResponder, name it '_collisionResponder':
        private ICollisionResponder _collisionResponder;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of CollisionSystem
        /// </summary>
        public CollisionSystem()
        {
            // INSTANTIATE _hitBoxCompDict as a new Dictionary<int, IContainHitBox>():
            _hitBoxCompDict = new Dictionary<int, IContainHitBox>();

            // INSTANTIATE _transformCompDict as a new Dictionary<int, IPosition>():
            _transformCompDict = new Dictionary<int, IPosition>();

            // INSTANTIATE _textureCompDict as a new Dictionary<int, ITexture>():
            _textureCompDict = new Dictionary<int, ITexture>();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEICOLLISIONRESPONDER

        /// <summary>
        /// Initialises an object with an ICollisionResponder object
        /// </summary>
        /// <param name="pCollisionResponder"> ICollisionResponder object </param>
        public void Initialise(ICollisionResponder pCollisionResponder)
        {
            // INITIALISE _collisionResponder with instance of pCollisionResponder:
            _collisionResponder = pCollisionResponder;
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIROIENTITYDICTIONARY

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        public void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict)
        {
            // INITIALISE _roEntityDict with instance of pIRODict:
            _roEntityDict = pIRODict;
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public void Update(GameTime pGameTime)
        {
            // CALL AddToCompDictionaries() iteratively so references are not kept:
            AddToCompDictionaries();

            // FOREACH Collidable entity:
            foreach (int pInt in _hitBoxCompDict.Keys)
            {
                // SET HitBox property of HitBoxComponent to a new Rectangle created using TransformComponent's Position property and TextureComponent's Texture property:
                _hitBoxCompDict[pInt].HitBox = new Rectangle((int)_transformCompDict[pInt].Position.X, (int)_transformCompDict[pInt].Position.Y, _textureCompDict[pInt].Texture.Width, _textureCompDict[pInt].Texture.Height);
            }

            // FOR LOOP, Iterates over count of _hitBoxCompDict - 1 so that first entity cannot collide with itself:
            for (int i = 0; i < (_hitBoxCompDict.Keys.Count - 1); i++)
            {
                // FOR LOOP, Iterates over count of _hitBoxCompDict + 1 so that second entity cannot collide with itself:
                for (int j = i + 1; j < _hitBoxCompDict.Keys.Count; j++)
                {
                    // CALL 'CollideResponse()' passing two Entity IDs as parameters:
                    CollideResponse(i, j);
                }
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        private void AddToCompDictionaries()
        {
            // CALL Clear() on _hitBoxCompDict, prevents entities being added multiple times:
            _hitBoxCompDict.Clear();

            // CALL Clear() on _transformCompDict, prevents entities being added multiple times:
            _transformCompDict.Clear();

            // CALL Clear() on _textureCompDict, prevents entities being added multiple times:
            _textureCompDict.Clear();

            // FOREACH UID in _roEntityCount:
            foreach (int pInt in _roEntityDict.Keys)
            {
                // DECLARE & INITIALISE a IReadOnlyDictionary<string, IComponent>, name it '_tempCompDict', give value of _roEntityDict[pInt]'s Component Dictionary:
                IReadOnlyDictionary<string, IComponent> _tempCompDict = (_roEntityDict[pInt] as IRtnROIComponentDictionary).ReturnComponentDictionary();

                // IF pComponent implements IPosition:
                if (_tempCompDict.ContainsKey("HitBoxComponent"))
                {
                    // FOREACH IComponent in currently selected entity's component dictionary:
                    foreach (IComponent pComponent in _tempCompDict.Values)
                    {
                        // IF pComponent implements IContainHitBox:
                        if (pComponent is IContainHitBox)
                        {
                            // ADD currently selected entity's IContainHitBox Component to _hitBoxCompDict:
                            _hitBoxCompDict.Add(_roEntityDict[pInt].UID, pComponent as IContainHitBox);
                        }

                        // IF pComponent implements IPosition:
                        if (pComponent is IPosition)
                        {
                            // ADD currently selected entity's IPosition Component to _transformCompDict:
                            _transformCompDict.Add(_roEntityDict[pInt].UID, pComponent as IPosition);
                        }

                        // IF pComponent implements ITexture:
                        if (pComponent is ITexture)
                        {
                            // ADD currently selected entity's ITexture Component to _textureCompDict:
                            _textureCompDict.Add(_roEntityDict[pInt].UID, pComponent as ITexture);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called to check when two or more entities with HitBox Components have collided
        /// </summary>
        /// <param name="pEntityOne"> ID of 1st Entity </param>
        /// <param name="pEntityTwo"> ID of 2nd Entity </param>
        /// <CITATION> (Price, 2021) </CITATION>
        private void CollideResponse(int pEntityOne, int pEntityTwo)
        {
            // IF First Entity Collides with Second Entity:
            if (_hitBoxCompDict[pEntityOne].HitBox.Intersects(_hitBoxCompDict[pEntityTwo].HitBox))
            {
                // CALL RespondToCollision() on _collisioResponder, passing the first and second collidable entities as parameters:
                _collisionResponder.RespondToCollision(_roEntityDict[pEntityOne], _roEntityDict[pEntityTwo]);
            }
        }

        #endregion
    }
}
