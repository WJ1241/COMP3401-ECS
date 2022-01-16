using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Services.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Managers.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers
{
    /// <summary>
    /// Class which stores and manages individual levels
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public class SceneManager : IService, IDraw, ISceneManager, IInitialiseISpawnEntity, IInitialiseIUpdatable, ISpawnEntity, IUpdatable
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
            // CALL Draw() on _sceneGraph, passing pSpriteBatch as a parameter:
            (_sceneGraph as IDraw).Draw(pSpriteBatch);
        }

        #endregion


        #region IMPLEMENTATION OF ISCENEMANAGER

        /// <summary>
        /// Removes an Entity from the scene using an ID
        /// </summary>
        /// <param name="pID"> Identification for chosen entity to remove from scene </param>
        public void Remove(int pID)
        {
            // CALL Remove() on _entityDict, passing pID as a parameter:
            _sceneEntityDict.Remove(pID);

            // PRINT to console to inform user of new entity:
            Console.WriteLine("Entity " + pID + " has been removed from Level!");
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEISPAWNENTITY

        /// <summary>
        /// Method which initialises an object with a ISpawnEntity Object
        /// </summary>
        /// <param name="pSpawnEntityObj"> ISpawnEntity object </param>
        public void Initialise(ISpawnEntity pSpawnEntityObj)
        {
            // ASSIGN _sceneGraph with reference to pSpawnEntityObj:
            _sceneGraph = pSpawnEntityObj;

            // INITIALISE _sceneGraph with reference to _sceneEntityDict:
            (_sceneGraph as IInitialiseIROIEntityDictionary).Initialise(_sceneEntityDict as IReadOnlyDictionary<int, IEntity>);
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIUPDATABLE

        /// <summary>
        /// Method which initialises an object with an IUpdatable object
        /// </summary>
        /// <param name="pUpdatable"> IUpdatable object </param>
        public void Initialise(IUpdatable pUpdatable)
        {
            // INITIALISE _sceneGraph with a reference to an IUpdatable object:
            (_sceneGraph as IInitialiseIUpdatable).Initialise(pUpdatable);
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
            // ADD pEntity as a value and pEntity.UID as a key to _sceneEntityDict:
            _sceneEntityDict.Add(pEntity.UID, pEntity);

            // CALL Spawn() on _sceneGraph, passing pEntity and pPosition as parameters:
            _sceneGraph.Spawn(pEntity, pPosition);
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public void Update(GameTime pGameTime)
        {
            // CALL Update() on _sceneGraph, passing pGameTime as a parameter:
            (_sceneGraph as IUpdatable).Update(pGameTime);
        }

        #endregion
    }
}
