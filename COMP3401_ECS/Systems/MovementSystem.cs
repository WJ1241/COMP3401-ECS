using System.Collections.Generic;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Exceptions;
using COMP3401ECS_Engine.Systems.Interfaces;

namespace COMP3401ECS_Engine.Systems
{
    /// <summary>
    /// System which uses Transform and Velocity Components to draw entity on screen
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class MovementSystem : System, IInitialiseParam<IMovementBoundResponder>
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<int, IPosition>, name it '_transformCompDict':
        private IDictionary<int, IPosition> _transformCompDict;

        // DECLARE an IDictionary<int, IVelocity>, name it '_velocityCompDict':
        private IDictionary<int, IVelocity> _velocityCompDict;

        // DECLARE an IMovementBoundResponder, name it '_mmBoundResponder':
        private IMovementBoundResponder _mmBoundResponder;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of MovementSystem
        /// </summary>
        public MovementSystem()
        {
            // INSTANTIATE _transformCompDict as a new Dictionary<int, IPosition>():
            _transformCompDict = new Dictionary<int, IPosition>();

            // INSTANTIATE _velocityCompDict as a new Dictionary<int, IVelocity>():
            _velocityCompDict = new Dictionary<int, IVelocity>();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIMOVEMENTBOUNDRESPONDER

        /// <summary>
        /// Initialises an object with an IMovementBoundResponder object
        /// </summary>
        /// <param name="pMmBoundResponder"> IMovementBoundResponder object </param>
        public void Initialise(IMovementBoundResponder pMmBoundResponder) 
        {
            // IF pMmBoundResponder DOES HAVE an active instance:
            if (pMmBoundResponder != null)
            {
                // INITIALISE _mmBoundResponder with reference to pMmBoundResponder:
                _mmBoundResponder = pMmBoundResponder;
            }
            // IF pMmBoundResponder DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pMmBoundResponder does not have active instance!");
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

            // FOREACH Moveable entity:
            foreach (int pInt in _velocityCompDict.Keys)
            {
                // CHANGE Position of TransformComponent, using VelocityComponent's velocity property:
                _transformCompDict[pInt].Position += _velocityCompDict[pInt].Velocity;

                // CALL RespondToBound(), passing _roEntityDict[pInt] as a parameter, constantly being called so position is always known:
                _mmBoundResponder.RespondToBound(_roEntityDict[pInt]);
            }
        }

        #endregion


        #region INHERITED FROM SYSTEM

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        protected override void AddToCompDictionaries()
        {
            // CALL Clear() on _transformCompDict, prevents components being added multiple times:
            _transformCompDict.Clear();

            // CALL Clear() on _velocityCompDict, prevents components being added multiple times:
            _velocityCompDict.Clear();

            // FOREACH UID in _roEntityCount:
            foreach (int pInt in _roEntityDict.Keys)
            {
                // DECLARE & INITIALISE a IReadOnlyDictionary<string, IComponent>, name it 'tempCompDict', give value of _roEntityDict[pInt]'s Component Dictionary:
                IReadOnlyDictionary<string, IComponent> tempCompDict = (_roEntityDict[pInt] as IRtnROIComponentDictionary).ReturnComponentDictionary();

                if (tempCompDict.ContainsKey("VelocityComponent"))
                {
                    // FOREACH IComponent in currently selected entity's component dictionary:
                    foreach (IComponent pComponent in tempCompDict.Values)
                    {
                        // IF pComponent implements IPosition:
                        if (pComponent is IPosition)
                        {
                            // ADD currently selected entity's IPosition Component to _transformCompDict:
                            _transformCompDict.Add(_roEntityDict[pInt].UID, pComponent as IPosition);
                        }

                        // IF pComponent requires movement:
                        if (pComponent is IVelocity)
                        {
                            // ADD currently selected entity's ITexture Component to _velocityCompDict:
                            _velocityCompDict.Add(_roEntityDict[pInt].UID, pComponent as IVelocity);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
