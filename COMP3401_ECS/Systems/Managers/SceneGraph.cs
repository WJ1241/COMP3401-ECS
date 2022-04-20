using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Exceptions;
using COMP3401ECS_Engine.Services.Interfaces;
using COMP3401ECS_Engine.Systems.Interfaces;
using COMP3401ECS_Engine.Systems.Managers.Interfaces;

namespace COMP3401ECS_Engine.Systems.Managers
{
    /// <summary>
    /// Class which contains entities relative to the level required to be loaded
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public class SceneGraph : IService, IInitialiseParam<IReadOnlyDictionary<int, IEntity>>, IInitialiseParam<IUpdatable>, IDraw, ISpawnEntity, IUpdatable
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


        #region IMPLEMENTATION OF IINITIALISEPARAM<IREADONLYDICTIONARY<INT, IENTITY>

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


        #region IMPLEMENTATION OF IINITIALISEPARAM<IUPDATABLE>

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
                (pUpdatable as IInitialiseParam<IReadOnlyDictionary<int, IEntity>>).Initialise(_sceneEntityDict);

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
            // CALL Draw() on DrawSystem, passing pSpriteBatch as a parameter:
            (_systemDict["DrawSystem"] as IDraw).Draw(pSpriteBatch);
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
            // FOREACH IUpdatable object in _systemDict.Values:
            foreach (IUpdatable pUpdatable in _systemDict.Values)
            {
                // CALL Update() on pUpdatable, passing pGameTime as a parameter:
                pUpdatable.Update(pGameTime);
            }
        }

        #endregion
    }
}
