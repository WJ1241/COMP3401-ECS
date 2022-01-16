using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using COMP3401_Project.ECSPackage.Components;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Delegates.Interfaces;
using COMP3401_Project.ECSPackage.Entities;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Factories;
using COMP3401_Project.ECSPackage.Factories.Interfaces;
using COMP3401_Project.ECSPackage.Services.Interfaces;
using COMP3401_Project.ECSPackage.Systems;
using COMP3401_Project.ECSPackage.Systems.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Managers;
using COMP3401_Project.ECSPackage.Systems.Managers.Interfaces;

namespace COMP3401_Project
{
    /// <summary>
    /// Main Class of ECS System
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public class Kernel : Game
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<string, IService>, name it '_serviceDict':
        private IDictionary<string, IService> _serviceDict;

        // DECLARE an IDictionary<int, IEntity>, name it '_entityDict':
        private IDictionary<int, IEntity> _entityDict;

        // DECLARE a GraphicsDeviceManager, name it '_graphics':
        private GraphicsDeviceManager _graphics;

        // DECLARE a SpriteBatch, name it '_spriteBatch':
        private SpriteBatch _spriteBatch;

        // DECLARE a Vector2, name it '_screenSize':
        private Vector2 _screenSize;

        #endregion


        #region CONSTRUCTOR

        public Kernel()
        {
            // INSTANTIATE _graphics as new GraphicsDeviceManager, passing Kernel as a parameter:
            _graphics = new GraphicsDeviceManager(this);

            // SET RootDirectory of Content as "Content":
            Content.RootDirectory = "Content";

            // SET screen width to 1600:
            _graphics.PreferredBackBufferWidth = 1600;

            // SET screen height to 900;
            _graphics.PreferredBackBufferHeight = 900;

            // SET IsMouseVisible to true to allow mouse testing:
            IsMouseVisible = true;

            // SET AllowUserResizing to true to allow User to configure window to suit their resolution:
            Window.AllowUserResizing = true;
        }

        #endregion


        #region PROTECTED METHODS

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region INITIAL SETUP

            // SET value of _screenSize.X to Viewport.Width:
            _screenSize.X = GraphicsDevice.Viewport.Width;

            // SET value of _screenSize.Y to Viewport.Height:
            _screenSize.Y = GraphicsDevice.Viewport.Height;

            #endregion


            #region DICTIONARY/LIST INSTANTIATIONS

            // INSTANTIATE _serviceDict as a new Dictionary<string, IService>():
            _serviceDict = new Dictionary<string, IService>();

            // ADD a new Factory<IService>() to _serviceDict:
            _serviceDict.Add("ServiceFactory", new Factory<IService>());

            // ADD a new Factory<IEntity>() to _serviceDict:
            _serviceDict.Add("EntityFactory", new Factory<IEntity>());

            // ADD a new Factory<IComponent>() to _serviceDict:
            _serviceDict.Add("ComponentFactory", new Factory<IComponent>());

            // ADD a new Factory<IUpdatable>() to _serviceDict:
            _serviceDict.Add("UpdatableFactory", new Factory<IUpdatable>());

            #endregion


            #region MANAGER INSTANTIATIONS

            #region ENTITY MANAGER

            // INSTANTIATE _serviceDict["EntityManager"] as a new EntityManager():
            _serviceDict.Add("EntityManager", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<EntityManager>());

            // STORE reference to master entity list locally for easier reuse:
            _entityDict = (_serviceDict["EntityManager"] as IRtnEntityDictionary).ReturnEntityDict();

            #endregion


            #region SCENE MANAGER

            // INSTANTIATE _serviceDict["SceneManager"] as a new SceneManager():
            _serviceDict.Add("SceneManager", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<SceneManager>());

            // INITIALISE _serviceDict["SceneManager"] with a new SceneGraph():
            (_serviceDict["SceneManager"] as IInitialiseISpawnEntity).Initialise((_serviceDict["ServiceFactory"] as IFactory<IService>).Create<SceneGraph>() as ISpawnEntity);

            // INITIALISE _serviceDict["EntityManager"] with reference to _serviceDict["SceneManager"]:
            (_serviceDict["EntityManager"] as IInitialiseISceneManager).Initialise(_serviceDict["SceneManager"] as ISceneManager);

            #endregion

            #endregion


            #region INITIALISING _SYSTEMLIST

            #region DRAW SYSTEM

            // DECLARE & INSTANTIATE a new DrawSystem(), name it '_tempUpdatable':
            IUpdatable _tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<DrawSystem>();

            // INITIALISE _serviceDict["SceneManager"] with _tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(_tempUpdatable);

            #endregion


            #region MOVEMENT SYSTEM

            // INSTANTIATE _tempUpdatable as new MovementSystem():
            _tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<MovementSystem>();

            // INITIALISE _tempUpdatable with reference to _entityDict.Terminate() method:
            (_tempUpdatable as IInitialiseDeleteDel).Initialise((_serviceDict["EntityManager"] as IEntityManager).Terminate);

            // INITIALISE _serviceDict["SceneManager"] with _tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(_tempUpdatable);

            #endregion

            #endregion


            #region ENTITY CREATION

            // FOR LOOP for creating entities, stops before 'i' reaches 2:
            for (int i = 0; i < 2; i++)
            {
                // DECLARE & INSTANTIATE an IEntity as a new Entity, name it '_tempEntity':
                IEntity _tempEntity = (_serviceDict["EntityFactory"] as IFactory<IEntity>).Create<Entity>();

                // ASSIGN UID to _tempEntity:
                _tempEntity.UID = i;

                // ADD _tempEntity as a value, and _tempEntity.UID as a key to _entityDict:
                _entityDict.Add(_tempEntity.UID, _tempEntity);
            }

            #endregion

            // INITIALISE base class:
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // INSTANTIATE _spriteBatch as new SpriteBatch:
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region INITIALISING ENTITIES WITH COMPONENTS

            #region ENTITY 0

            // ADD a new TransformComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new HitBoxComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<HitBoxComponent>());

            // ADD a new VelocityComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>());

            #endregion


            #region ENTITY 1

            // ADD a new TransformComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new HitBoxComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<HitBoxComponent>());

            // ADD a new VelocityComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>());

            #endregion

            #endregion


            #region SETTING ENTITY COMPONENT VALUES

            #region ENTITY 0

            // LOAD "ServerLogo" as the texture of _entityDict[0]'s HitBoxComponent:
            ((_entityDict[0] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture = Content.Load<Texture2D>("Server Logo");

            // SET Speed of _entityDict[0]'s VelocityComponent:
            ((_entityDict[0] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Speed = 5;

            // SET Direction of _entityDict[0]'s VelocityComponent:
            ((_entityDict[0] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(1);

            #endregion


            #region ENTITY 1

            // LOAD "ServerLogo" as the texture of _entityDict[1]'s HitBoxComponent:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture = Content.Load<Texture2D>("Server Logo");

            // SET Speed of _entityDict[1]'s VelocityComponent:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Speed = 2;

            // SET Direction of _entityDict[1]'s VelocityComponent:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(-1);

            #endregion

            #endregion


            #region SPAWNING ENTITIES

            // SPAWN _entityDict[0] with Position at the centre of screen:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[0], _screenSize / 2);

            // SPAWN _entityDict[1] with Position at the top left corner of screen:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[1], _screenSize / _screenSize);

            #endregion

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime pGameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                // CALL Exit():
                Exit();
            }

            // CALL Update() on _serviceDict["SceneManager"], passing pGameTime as a parameter:
            (_serviceDict["SceneManager"] as IUpdatable).Update(pGameTime);

            // CALL Update() on base class, passing pGameTime as a parameter:
            base.Update(pGameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime pGameTime)
        {
            // SET colour of background:
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // CALL Draw() on _sceneManager, passing _spriteBatch as a parameter:
            (_serviceDict["SceneManager"] as IDraw).Draw(_spriteBatch);

            // CALL Draw() on base class, passing pGameTime as a parameter:
            base.Draw(pGameTime);
        }

        #endregion
    }
}
