using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses HitBox Components to test whether entities have collided with other entities
    /// Author: William Smith & Marc Price
    /// Date: 09/02/22
    /// </summary>
    /// <REFERENCE> Price, M. (2021) ‘Session 16 - Collision Management’, Games Design & Engineering: Sessions. Available at: https://worcesterbb.blackboard.com. (Accessed: 14 January 2022).</REFERENCE>
    public class CollisionSystem : System, IInitialiseICollisionResponder
    {
        #region FIELD VARIABLES

        // DECLARE an IList<IEntity>, name it '_hitBoxEntList', prevents misaddressing issue in Collision FOR loop:
        private IList<IEntity> _hitBoxEntList;

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
            // INSTANTIATE _hitBoxEntList as a new List<IEntity>():
            _hitBoxEntList = new List<IEntity>();

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
            // IF pCollisionResponder DOES HAVE an active instance:
            if (pCollisionResponder != null)
            {
                // INITIALISE _collisionResponder with instance of pCollisionResponder:
                _collisionResponder = pCollisionResponder;
            }
            // IF pCollisionResponder DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pCollisionResponder does not have active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public override void Update(GameTime pGameTime)
        {
            // CALL AddToCompDictionaries() iteratively so references are not kept:
            AddToCompDictionaries();

            // FOREACH Collidable entity:
            foreach (int pInt in _hitBoxCompDict.Keys)
            {
                // SET HitBox property of HitBoxComponent to a new Rectangle created using TransformComponent's Position property and TextureComponent's Texture property, takes away Origin due to actual Position being different to draw Position:
                _hitBoxCompDict[pInt].HitBox = new Rectangle((int)_transformCompDict[pInt].Position.X - (int)(_transformCompDict[pInt] as IRotation).Origin.X, 
                                                             (int)_transformCompDict[pInt].Position.Y - (int)(_transformCompDict[pInt] as IRotation).Origin.Y,
                                                             _textureCompDict[pInt].TexSize.X,
                                                             _textureCompDict[pInt].TexSize.Y);
            }

            // DECLARE an IList<IContainHitBox>, give value of _hitBoxCompDict.Values as a List:
            IList<IContainHitBox> tempHitBoxList = _hitBoxCompDict.Values.ToList();

            // FOR LOOP, Iterates over count of tempHitBoxList - 1 so that first entity cannot collide with itself:
            for (int i = 0; i < (tempHitBoxList.Count - 1); i++)
            {
                // FOR LOOP, Iterates over count of tempHitBoxList + 1 so that second entity cannot collide with itself:
                for (int j = i + 1; j < tempHitBoxList.Count; j++)
                { 
                    // IF First Entity (i) Collides with Second Entity (j):
                    if (tempHitBoxList[i].HitBox.Intersects(tempHitBoxList[j].HitBox))
                    {
                        // CALL RespondToCollision() on _collisionResponder, passing the first (i) and second (j) collidable entities as parameters:
                        _collisionResponder.RespondToCollision(_hitBoxEntList[i], _hitBoxEntList[j]);
                    }
                }
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        protected override void AddToCompDictionaries()
        {
            // CALL Clear() on _hitBoxCompDict, prevents entities being added multiple times and misaddressing old entities:
            _hitBoxEntList.Clear();

            // CALL Clear() on _hitBoxCompDict, prevents entities being added multiple times:
            _hitBoxCompDict.Clear();

            // CALL Clear() on _transformCompDict, prevents entities being added multiple times:
            _transformCompDict.Clear();

            // CALL Clear() on _textureCompDict, prevents entities being added multiple times:
            _textureCompDict.Clear();

            // FOREACH UID in _roEntityCount:
            foreach (int pInt in _roEntityDict.Keys)
            {
                // DECLARE & INITIALISE a IReadOnlyDictionary<string, IComponent>, name it 'tempCompDict', give value of _roEntityDict[pInt]'s Component Dictionary:
                IReadOnlyDictionary<string, IComponent> tempCompDict = (_roEntityDict[pInt] as IRtnROIComponentDictionary).ReturnComponentDictionary();

                // IF tempCompDict contains a HitBoxComponent:
                if (tempCompDict.ContainsKey("HitBoxComponent"))
                {
                    // ADD _roEntityDict[pInt] to _hitBoxEntList:
                    _hitBoxEntList.Add(_roEntityDict[pInt]);

                    // FOREACH IComponent in currently selected entity's component dictionary:
                    foreach (IComponent pComponent in tempCompDict.Values)
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

        #endregion
    }
}
