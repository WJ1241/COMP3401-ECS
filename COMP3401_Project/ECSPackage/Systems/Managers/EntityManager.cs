using System;
using System.Collections.Generic;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Managers.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers
{
    /// <summary>
    /// Class which contains the master list of entities in the game level
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class EntityManager : IEntityManager, IInitialiseISceneManager, IRtnEntityDictionary
    {
        #region FIELD VARIABLES

        // DECLARE a IDictionary<int, IEntity>, name it '_entityDict':
        private IDictionary<int, IEntity> _entityDict;

        // DECLARE an ISceneManager, name it '_sceneManager':
        private ISceneManager _sceneManager;

        // DECLARE an int, name it '_uIDCount':
        private int _uIDCount;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of EntityManagerSystem
        /// </summary>
        public EntityManager()
        {
            // INITIALISE _uIDCount with a value of 0:
            _uIDCount = 0;

            // INSTANTIATE _entityDict as a new Dictionary<int, IEntity>():
            _entityDict = new Dictionary<int, IEntity>();
        }

        #endregion


        #region IMPLEMENTATION OF IENTITYMANAGER

        /// <summary>
        /// Adds an Entity to a collection object
        /// </summary>
        /// <param name="pEntity"> Entity to be added to collection object </param>
        public void AddEntity(IEntity pEntity)
        {
            // IF pEntity DOES HAVE an active instance:
            if (pEntity != null)
            {
                // SET pEntity.UID value to current _uIDCount value:
                pEntity.UID = _uIDCount;

                // ADD pEntity as well as current _uIDCount for identification
                _entityDict.Add(pEntity.UID, pEntity);

                // INCREMENT _uIDCount by 1:
                _uIDCount++;
            }

            // IF pEntity DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: Entity to be added to _entityDict is currently null, requires instantiation!");
            }
        }

        /// <summary>
        /// Terminates Entity using its ID value
        /// </summary>
        /// <param name="pID"> ID value of Entity in scene </param>
        public void Terminate(int pID)
        {
            #region SCENEMANAGER

            // TRY checking if _sceneManager contains an entity ID'd with value of pID:
            try
            {
                // CALL Remove() on _sceneManager:
                _sceneManager.Remove(pID);
            }

            // CATCH InvalidValueException from entity removal:
            catch (InvalidValueException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }

            #endregion


            #region ENTITYMANAGER

            // IF _entityDict DOES contain an entity ID'd with value of pID:
            if (_entityDict.ContainsKey(pID))
            {
                // CALL Remove() on _entityDict, passing pID as a parameter:
                _entityDict.Remove(pID);
            }
            // IF _entityDict DOES NOT contain an entity ID'd with value of pID:
            else
            {
                // THROW new InvalidValueException, with corresponding message:
                throw new InvalidValueException("ERROR: _entityDict does not contain an entity with an ID value of " + pID + "!");
            }

            #endregion
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEISCENEMANAGER

        /// <summary>
        /// Method which initialises an object with an ISceneManager object
        /// </summary>
        /// <param name="pSceneManager"> ISceneManager object </param>
        public void Initialise(ISceneManager pSceneManager)
        {
            // IF pSceneManager DOES HAVE an active instance:
            if (pSceneManager != null)
            {
                // INITIALISE _sceneManager with reference to pSceneManager:
                _sceneManager = pSceneManager;
            }
            // IF pSceneManager DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pSceneManager cannot be stored in _sceneManager as it is currently null, requires instantiation!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IRTNENTITYDICTIONARY

        /// <summary>
        /// Returns a reference to an Entity Dictionary
        /// </summary>
        public IDictionary<int, IEntity> ReturnEntityDict()
        {
            // IF _entityDict DOES HAVE an active instance:
            if (_entityDict != null)
            {
                // RETURN reference to _entityDict:
                return _entityDict;
            }
            // IF _entityDict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _entityDict cannot be returned as it is currently null, requires instantiation!");
            }
        }

        #endregion
    }
}
