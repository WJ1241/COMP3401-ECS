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
    /// System which uses Player Components to allow an entity to be controlled by a User/Player
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public class InputSystem : IInitialiseIInputResponder, IInitialiseIROIEntityDictionary, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        private IReadOnlyDictionary<int, IEntity> _roEntityDict;

        // DECLARE an IDictionary<int, IEntity>, name it '_playerEntityDict':
        private IDictionary<int, IEntity> _playerEntityDict;

        // DECLARE an IInputResponder, name it '_inputResponder':
        private IInputResponder _inputResponder;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of InputSystem
        /// </summary>
        public InputSystem()
        {
            // INSTANTIATE _playerEntityDict as a new Dictionary<int, IEntity>():
            _playerEntityDict = new Dictionary<int, IEntity>();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIINPUTRESPONDER

        /// <summary>
        /// Initialises an object with an IInputResponder object
        /// </summary>
        /// <param name="pInputResponder"> IInputResponder object </param>
        public void Initialise(IInputResponder pInputResponder) 
        {
            // INITIALISE _inputResponder with reference to pInputResponder:
            _inputResponder = pInputResponder;
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

            // FOREACH IEntity object in _playerEntityDict.Values:
            foreach (IEntity pEntity in _playerEntityDict.Values)
            {
                // CALL RespondToInput on _inputResponder, passing pEntity as a parameter, constantly being called so player input is always detected:
                _inputResponder.RespondToInput(pEntity);
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        private void AddToCompDictionaries()
        {
            // CALL Clear() on _playerEntityDict, prevents entities being added multiple times:
            _playerEntityDict.Clear();

            // FOREACH UID in _roEntityCount:
            foreach (int pInt in _roEntityDict.Keys)
            {
                // DECLARE & INITIALISE a IReadOnlyDictionary<string, IComponent>, name it '_tempCompDict', give value of _roEntityDict[pInt]'s Component Dictionary:
                IReadOnlyDictionary<string, IComponent> _tempCompDict = (_roEntityDict[pInt] as IRtnROIComponentDictionary).ReturnComponentDictionary();

                // FOREACH IComponent in _tempCompDict.Values:
                foreach (IComponent pComponent in _tempCompDict.Values)
                {
                    // IF pComponent implements IPlayer:
                    if (pComponent is IPlayer)
                    {
                        // ADD Player Entity to _playerEntityDict as a value, and their UID as a key:
                        _playerEntityDict.Add(_roEntityDict[pInt].UID, _roEntityDict[pInt]);
                    }
                }

                /*
                // IF _tempCompDict contains a PlayerComponent:
                if (_tempCompDict.ContainsKey("PlayerComponent"))
                {
                    // ADD Player Entity to _playerEntityDict as a value, and their UID as a key:
                    _playerEntityDict.Add(_roEntityDict[pInt].UID, _roEntityDict[pInt]);
                }
                */

            }
        }

        #endregion
    }
}
