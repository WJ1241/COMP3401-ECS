using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Managers.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers
{
    /// <summary>
    /// Class which stores and manages individual levels
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class SceneManager : IDraw, ISceneManager, IInitialiseISpawnEntity, IInitialiseIUpdatable, ISpawnEntity, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<int, IEntity>, name it '_sceneEntityDict':
        private IDictionary<int, IEntity> _sceneEntityDict;

        // DECLARE an ISpawnEntity, name it '_sceneGraph':
        private ISpawnEntity _sceneGraph;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of SceneManager
        /// </summary>
        public SceneManager()
        {
            // INSTANTIATE _sceneEntityDict as a new Dictionary<int, IEntity>():
            _sceneEntityDict = new Dictionary<int, IEntity>();
        }

        #endregion


        #region IMPLEMENTATION OF IDRAW

        /// <summary>
        /// When called, draws entity's texture on screen
        /// </summary>
        /// <param name="pSpriteBatch"> Needed to draw entity's texture on screen </param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            // TRY checking if Draw() throws either a NullInstanceException or a NullReferenceException:
            try
            {
                // IF _sceneGraph DOES HAVE an active instance:
                if (_sceneGraph != null)
                {
                    // CALL Draw() on _sceneGraph, passing pSpriteBatch as a parameter:
                    (_sceneGraph as IDraw).Draw(pSpriteBatch);
                }
                // IF _sceneGraph DOES NOT HAVE an active instance:
                else
                {
                    // THROW new NullInstanceException, with corresponding message:
                    throw new NullInstanceException("ERROR: _sceneGraph does not have an active instance!");
                }
            }
            // CATCH NullInstanceException from Draw():
            catch (NullInstanceException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }
            // CATCH NullReferenceException from Draw():
            catch (NullReferenceException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }
        }

        #endregion


        #region IMPLEMENTATION OF ISCENEMANAGER

        /// <summary>
        /// Removes an Entity from the scene using an ID
        /// </summary>
        /// <param name="pID"> Identification for chosen entity to remove from scene </param>
        public void Remove(int pID)
        {
            // IF _sceneEntityDict DOES contain an entity ID'd with value of pID:
            if (_sceneEntityDict.ContainsKey(pID))
            {
                // CALL Remove() on _entityDict, passing pID as a parameter:
                _sceneEntityDict.Remove(pID);

                // PRINT to console to inform user of new entity:
                Console.WriteLine("Entity " + pID + " has been removed from Level!");
            }
            // IF _sceneEntityDict DOES NOT contain an entity ID'd with value of pID:
            else
            {
                // THROW new InvalidValueException, with corresponding message:
                throw new InvalidValueException("ERROR: _sceneEntityDict does not contain an entity with an ID value of " + pID + "!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEISPAWNENTITY

        /// <summary>
        /// Method which initialises an object with a ISpawnEntity Object
        /// </summary>
        /// <param name="pSpawnEntityObj"> ISpawnEntity object </param>
        public void Initialise(ISpawnEntity pSpawnEntityObj)
        {
            // IF pSpawnEntityObj DOES HAVE an active instance:
            if (pSpawnEntityObj != null)
            {
                // ASSIGN _sceneGraph with reference to pSpawnEntityObj:
                _sceneGraph = pSpawnEntityObj;

                // TRY checking if Initialise() throws a NullInstanceException:
                try
                {
                    // INITIALISE _sceneGraph with reference to _sceneEntityDict:
                    (_sceneGraph as IInitialiseIROIEntityDictionary).Initialise(_sceneEntityDict as IReadOnlyDictionary<int, IEntity>);
                }
                // CATCH NullInstanceException from Initialise():
                catch (NullInstanceException e)
                {
                    // WRITE exception message to console:
                    Console.WriteLine(e.Message);
                }
            }
            // IF pSpawnEntityObj DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pSpawnEntityObj does not have an active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIUPDATABLE

        /// <summary>
        /// Method which initialises an object with an IUpdatable object
        /// </summary>
        /// <param name="pUpdatable"> IUpdatable object </param>
        public void Initialise(IUpdatable pUpdatable)
        {
            // TRY checking if Initialise() throws a NullInstanceException:
            try
            {
                // IF pUpdatable DOES HAVE an active instance:
                if (pUpdatable != null)
                {
                    // INITIALISE _sceneGraph with a reference to an IUpdatable object:
                    (_sceneGraph as IInitialiseIUpdatable).Initialise(pUpdatable);
                }
                // IF pUpdatable DOES NOT HAVE an active instance:
                else
                {
                    // THROW new NullInstanceException, with corresponding message:
                    throw new NullInstanceException("ERROR: pUpdatable does not have an active instance");
                }
            }
            // CATCH NullInstanceException from Initialise():
            catch (NullInstanceException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }
        }

        #endregion


        #region IMPLEMENTATION OF ISPAWNENTITY

        /// <summary>
        /// Spawns Entity on screen with a given position
        /// </summary>
        /// <param name="pEntity"> Entity to be spawned on screen </param>
        /// <param name="pPosition"> Position for Entity to be placed </param>
        public void Spawn(IEntity pEntity, Vector2 pPosition)
        {
            // IF _sceneEntityDict DOES HAVE an active instance:
            if (_sceneEntityDict != null)
            {
                // ADD pEntity as a value and pEntity.UID as a key to _sceneEntityDict:
                _sceneEntityDict.Add(pEntity.UID, pEntity);
            }
            // IF _sceneEntityDict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _sceneEntityDict does not contain an active instance!");
            }

            // TRY checking if _sceneGraph.Spawn() throws a NullInstanceException:
            try
            {
                // IF _sceneGraph DOES HAVE an active instance:
                if (_sceneGraph != null)
                {
                    // CALL Spawn() on _sceneGraph, passing pEntity and pPosition as parameters:
                    _sceneGraph.Spawn(pEntity, pPosition);
                }
                // IF _sceneGraph DOES NOT HAVE an active instance:
                else
                {
                    // THROW new NullInstanceException, with corresponding message:
                    throw new NullInstanceException("ERROR: _sceneGraph does not contain an active instance!");
                }
            }
            // CATCH NullInstanceException from entity spawn:
            catch (NullInstanceException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public void Update(GameTime pGameTime)
        {
            // TRY checking if _sceneGraph.Update() throws a NullReferenceException:
            try
            {
                // IF _sceneGraph DOES HAVE an active instance:
                if (_sceneGraph != null)
                {
                    // CALL Update() on _sceneGraph, passing pGameTime as a parameter:
                    (_sceneGraph as IUpdatable).Update(pGameTime);
                }
                // IF _sceneGraph DOES NOT HAVE an active instance:
                else
                {
                    // THROW new NullInstanceException, with corresponding message:
                    throw new NullInstanceException("ERROR: _sceneGraph does not contain an active instance!");
                }
            }
            // CATCH NullReferenceException from scene graph update:
            catch (NullReferenceException e)
            {
                // WRITE exception message to console:
                Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
