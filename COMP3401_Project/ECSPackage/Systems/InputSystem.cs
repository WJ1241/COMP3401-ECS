using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses Player Components to allow an entity to be controlled by a User/Player
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class InputSystem : System, IInitialiseIInputResponder
    {
        #region FIELD VARIABLES

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
            // IF pInputResponder DOES HAVE an active instance:
            if (pInputResponder != null)
            {
                // INITIALISE _inputResponder with reference to pInputResponder:
                _inputResponder = pInputResponder;
            }
            // IF pInputResponder DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pInputResponder does not have active instance!");
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

            // IF _playerEntityDict DOES HAVE an active instance:
            if (_playerEntityDict != null)
            {
                // FOREACH IEntity object in _playerEntityDict.Values:
                foreach (IEntity pEntity in _playerEntityDict.Values)
                {
                    // TRY checking if RespondToInput() throws a NullInstanceException:
                    try
                    {
                        // CALL RespondToInput on _inputResponder, passing pEntity as a parameter, constantly being called so player input is always detected:
                        _inputResponder.RespondToInput(pEntity);
                    }
                    // CATCH 
                    catch (NullInstanceException e)
                    {
                        // WRITE exception message to console:
                        Console.WriteLine(e.Message);
                    }
                }
            }
            // IF _playerEntityDict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _playerEntityDict does not have an active instance!");
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        protected override void AddToCompDictionaries()
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
