using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Services.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Managers.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers
{
    /// <summary>
    /// Class which contains entities relative to the level required to be loaded
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class SceneGraph : IService, IInitialiseIROIEntityDictionary, IInitialiseIUpdatable, IDraw, ISpawnEntity, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<string, IEntity>, name it '_sceneEntityDict':
        private IReadOnlyDictionary<int, IEntity> _sceneEntityDict;

        // DECLARE an IDictionary<string, IUpdatable>, name it '_systemDict':
        private IDictionary<string, IUpdatable> _systemDict;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of SceneGraph
        /// </summary>
        public SceneGraph()
        {
            // INSTANTIATE _systemDict as a new Dictionary<string, IUpdatable>():
            _systemDict = new Dictionary<string, IUpdatable>();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIROIENTITYDICTIONARY

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        public void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict)
        {
            // IF pIRODict DOES HAVE an active instance:
            if (pIRODict != null)
            {
                // INITIALISE _sceneEntityDict with reference to pIRODict:
                _sceneEntityDict = pIRODict;
            }
            // IF pIRODict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pIRODict does not have an active instance!");
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
            // IF pUpdatable DOES HAVE an active instance:
            if (pUpdatable != null)
            {
                // INITIALISE pUpdatable with reference to _sceneEntityDict:
                (pUpdatable as IInitialiseIROIEntityDictionary).Initialise(_sceneEntityDict);

                // ADD pUpdatable as a value, and its name as a string to _systemDict:
                _systemDict.Add(pUpdatable.GetType().Name, pUpdatable);
            }
            // IF pUpdatable DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pUpdatable does not have an active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IDRAW

        /// <summary>
        /// When called, draws entity's texture on screen
        /// </summary>
        /// <param name="pSpriteBatch"> Needed to draw entity's texture on screen </param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            // IF _systemDict DOES contain a key named "DrawSystem" && DOES HAVE an active instance:
            if (_systemDict.ContainsKey("DrawSystem") && _systemDict["DrawSystem"] != null)
            {
                // TRY checking if Draw() throws a NullInstanceException:
                try
                {
                    // CALL Draw() on DrawSystem, passing pSpriteBatch as a parameter:
                    (_systemDict["DrawSystem"] as IDraw).Draw(pSpriteBatch);
                }
                // CATCH NullInstanceException from Draw():
                catch (NullInstanceException e)
                {
                    // WRITE exception message to console:
                    Console.WriteLine(e.Message);
                }
            }
            // IF _systemDict DOES NOT contain a key named "DrawSystem":
            else if (!_systemDict.ContainsKey("DrawSystem"))
            {
                // THROW new NullReferenceException, with corresponding message:
                throw new NullReferenceException("ERROR: No object stored with 'DrawSystem' as a key in _systemDict!");
            }
            // IF _systemDict["DrawSystem"] DOES NOT HAVE an active instance:
            else if (_systemDict["DrawSystem"] == null)
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _systemDict['DrawSystem'] does not have an active instance!");
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
            // IF pEntity DOES HAVE an active instance:
            if (pEntity != null)
            {
                // SET position of pEntity with the value of pPosition:
                ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position = pPosition;

                // PRINT to console to inform user of new entity:
                Console.WriteLine("Entity " + pEntity.UID + " has Spawned in Level!");
            }
            // IF pEntity DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pEntity " + pEntity.UID + " does not have an active instance!");
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
            // IF _systemDict DOES contain an object:
            if (_systemDict.Count != 0)
            {
                // FOREACH IUpdatable object in _systemDict.Values:
                foreach (IUpdatable pUpdatable in _systemDict.Values)
                {
                    // TRY checking if Update() throws a NullInstanceException:
                    try
                    {
                        // CALL Update() on pUpdatable, passing pGameTime as a parameter:
                        pUpdatable.Update(pGameTime);
                    }
                    // CATCH NullInstanceException from Update():
                    catch (NullInstanceException e)
                    {
                        // WRITE exception to console:
                        Console.WriteLine(e.Message);
                    }
                }
            }
            // IF _systemDict DOES NOT contain an object:
            else
            {
                // THROW new NullReferenceException, with corresponding message:
                throw new NullReferenceException("WARNING: There are no systems to be updated in game loop!");
            }
        }

        #endregion

    }
}
