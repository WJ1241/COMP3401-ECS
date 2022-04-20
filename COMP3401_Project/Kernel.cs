using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ClosedXML.Excel;
using COMP3401ECS_Engine.Components;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Delegates.Interfaces;
using COMP3401ECS_Engine.Entities;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Factories;
using COMP3401ECS_Engine.Factories.Interfaces;
using COMP3401ECS_Engine.Services.Interfaces;
using COMP3401ECS_Engine.Systems;
using COMP3401ECS_Engine.Systems.Interfaces;
using COMP3401ECS_Engine.Systems.Managers;
using COMP3401ECS_Engine.Systems.Managers.Interfaces;
using COMP3401_Project.PongPackage.Forms;
using COMP3401_Project.PongPackage.Responders;
using COMP3401_Project_ProjectHWTest;
using COMP3401_Project.ProjectHWTest.Interfaces;

namespace COMP3401_Project
{
    /// <summary>
    /// Main Class of ECS System
    /// Author: William Smith
    /// Date: 04/04/22
    /// </summary>
    public class Kernel : Game
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<string, IService>, name it '_serviceDict':
        private IDictionary<string, IService> _serviceDict;

        // DECLARE an IDictionary<int, IEntity>, name it '_entityDict':
        private IDictionary<int, IEntity> _entityDict;

        // DECLARE a Form, name it '_entityCreator':
        private Form _entityCreator;

        // DECLARE a GraphicsDeviceManager, name it '_graphics':
        private GraphicsDeviceManager _graphics;

        // DECLARE a SpriteBatch, name it '_spriteBatch':
        private SpriteBatch _spriteBatch;

        // DECLARE a Random, name it '_rand':
        private Random _rand;

        // DECLARE a Vector2, name it '_screenSize':
        private Vector2 _screenSize;

        // DECLARE an int, name it '_pongEntityID':
        private int _pongEntityID;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of Kernel
        /// </summary>
        public Kernel()
        {
            // INSTANTIATE _graphics as new GraphicsDeviceManager, passing Kernel as a parameter:
            _graphics = new GraphicsDeviceManager(this);

            // INSTANTIATE _entityCreator as a new CreationScreen():
            _entityCreator = new CreationScreen();

            // INSTANTIATE _rand as a new Random():
            _rand = new Random();

            // SET screen width to 1600:
            _graphics.PreferredBackBufferWidth = 1600;

            // SET screen height to 900;
            _graphics.PreferredBackBufferHeight = 900;
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

            // SET RootDirectory of Content as "Content":
            Content.RootDirectory = "Content";

            // SET value of _screenSize.X to Viewport.Width:
            _screenSize.X = GraphicsDevice.Viewport.Width;

            // SET value of _screenSize.Y to Viewport.Height:
            _screenSize.Y = GraphicsDevice.Viewport.Height;

            // INITIALISE _entityCreator with reference to CreateMultipleEntities():
            (_entityCreator as IInitialiseCreateMultiDel).Initialise(CreateMultipleEntities);

            // INITIALISE _entityCreator with reference to DeleteMultipleEntities():
            (_entityCreator as IInitialiseDeleteDel).Initialise(DeleteMultipleEntities);

            // SHOW _entityCreator:
            _entityCreator.Show();

            // SET IsMouseVisible to true to allow mouse testing:
            IsMouseVisible = true;

            // SET AllowUserResizing to true to allow User to configure window to suit their resolution:
            Window.AllowUserResizing = true;

            // ASSIGN _pongEntityID with a value of 0:
            _pongEntityID = 0;

            #endregion


            #region DICTIONARY/LIST INSTANTIATIONS

            // INSTANTIATE _serviceDict as a new Dictionary<string, IService>():
            _serviceDict = new Dictionary<string, IService>();

            #endregion


            #region FACTORY INSTANTIATIONS

            // ADD a new Factory<IService>() to _serviceDict:
            _serviceDict.Add("ServiceFactory", new Factory<IService>());

            // ADD a new Factory<IDisposable>() to _serviceDict:
            _serviceDict.Add("DisposableFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IDisposable>>());

            // ADD a new Factory<IEnumerable>() to _serviceDict:
            _serviceDict.Add("EnumerableFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IEnumerable>>());

            // ADD a new Factory<IEntity>() to _serviceDict:
            _serviceDict.Add("EntityFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IEntity>>());

            // ADD a new Factory<IComponent>() to _serviceDict:
            _serviceDict.Add("ComponentFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IComponent>>());

            // ADD a new Factory<IUpdatable>() to _serviceDict:
            _serviceDict.Add("UpdatableFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IUpdatable>>());

            // ADD a new Factory<IResponder>() to _serviceDict:
            _serviceDict.Add("ResponderFactory", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<Factory<IResponder>>());

            #endregion


            #region MANAGER INSTANTIATIONS & INITIALISATIONS

            #region ENTITY MANAGER

            // ADD a new EntityManager() to _serviceDict:
            _serviceDict.Add("EntityManager", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<EntityManager>());

            // STORE reference to master entity list locally for easier reuse:
            _entityDict = (_serviceDict["EntityManager"] as IRtnEntityDictionary).ReturnEntityDict();

            #endregion


            #region SCENE MANAGER

            // ADD a new SceneManager() to _serviceDict:
            _serviceDict.Add("SceneManager", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<SceneManager>());

            // INITIALISE _serviceDict["SceneManager"] with a new SceneGraph():
            (_serviceDict["SceneManager"] as IInitialiseISpawnEntity).Initialise((_serviceDict["ServiceFactory"] as IFactory<IService>).Create<SceneGraph>() as ISpawnEntity);

            // INITIALISE _serviceDict["EntityManager"] with reference to _serviceDict["SceneManager"]:
            (_serviceDict["EntityManager"] as IInitialiseISceneManager).Initialise(_serviceDict["SceneManager"] as ISceneManager);

            #endregion

            #endregion


            #region PERFORMANCE MEASURE INSTANTIATION & INITIALISATIONS

            // ADD a new PerformanceMeasure() to _serviceDict:
            _serviceDict.Add("PerformanceMeasure", (_serviceDict["ServiceFactory"] as IFactory<IService>).Create<PerformanceMeasure>());

            // INITIALISE _serviceDict["PerformanceMeasure"] with a new Stopwatch():
            (_serviceDict["PerformanceMeasure"] as IInitialiseStopwatch).Initialise(new Stopwatch());

            // INITIALISE _serviceDict["PerformanceMeasure"] with a new XLWorkbook():
            (_serviceDict["PerformanceMeasure"] as IExportExcelData).Initialise((_serviceDict["DisposableFactory"] as IFactory<IDisposable>).Create<XLWorkbook>() as XLWorkbook);

            // INITIALISE _serviceDict["PerformanceMeasure"] with a new List<long>():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise((_serviceDict["EnumerableFactory"] as IFactory<IEnumerable>).Create<List<long>>() as IList<long>);

            // INITIALISE _serviceDict["PerformanceMeasure"] with "CPU" and a new PerformanceCounter():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise("CPU", new PerformanceCounter("Process", "% Processor Time", "COMP3401_Project"));

            // INITIALISE _serviceDict["PerformanceMeasure"] with "CPU" and a new List<float>():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise("CPU", (_serviceDict["EnumerableFactory"] as IFactory<IEnumerable>).Create<List<float>>() as IList<float>);

            // INITIALISE _serviceDict["PerformanceMeasure"] with "RAM" and a new PerformanceCounter():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise("RAM", new PerformanceCounter("Process", "Working Set", "COMP3401_Project"));

            // INITIALISE _serviceDict["PerformanceMeasure"] with "RAM" and a new List<float>():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise("RAM", (_serviceDict["EnumerableFactory"] as IFactory<IEnumerable>).Create<List<float>>() as IList<float>);

            // INITIALISE _serviceDict["PerformanceMeasure"] with "FPS" and a new List<float>():
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).Initialise("FPS", (_serviceDict["EnumerableFactory"] as IFactory<IEnumerable>).Create<List<float>>() as IList<float>);

            #endregion


            #region INITIALISING SYSTEMS TO SCENEMANAGER

            #region DRAW SYSTEM

            // DECLARE & INSTANTIATE a new DrawSystem(), name it 'tempUpdatable':
            IUpdatable tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<DrawSystem>();

            // INITIALISE _serviceDict["SceneManager"] with _tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(tempUpdatable);

            #endregion


            #region MOVEMENT SYSTEM

            // INSTANTIATE tempUpdatable as new MovementSystem():
            tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<MovementSystem>();

            // INITIALISE _serviceDict["SceneManager"] with tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(tempUpdatable);

            #region IMOVEMENTBOUNDRESPONDER INITIALISATION

            // DECLARE & INSTANTIATE an IMovementBoundResponder as a new PongMovementBoundResponder, name it '_mmBoundResponder':
            IMovementBoundResponder _mmBoundResponder = (_serviceDict["ResponderFactory"] as IFactory<IResponder>).Create<PongMovementBoundResponder>() as IMovementBoundResponder;

            // INITIALISE _mmBoundResponder with reference to CreateBall() method:
            (_mmBoundResponder as IInitialiseCreateDel).Initialise(CreateBall);

            // INITIALISE _mmBoundResponder with reference to _entityDict.Terminate() method:
            (_mmBoundResponder as IInitialiseDeleteDel).Initialise((_serviceDict["EntityManager"] as IEntityManager).Terminate);

            // INITIALISE _mmBoundResponder.MaxXYBound with a new Point set at (0,0):
            _mmBoundResponder.MinXYBound = new Point(0);

            // INITIALISE _mmBoundResponder.MaxXYBound with a new Point set at (_screenSize.X,_screenSize.Y):
            _mmBoundResponder.MaxXYBound = new Point((int)_screenSize.X, (int)_screenSize.Y);

            #endregion

            // INITIALISE tempUpdatable with _mmBoundResponder:
            (tempUpdatable as IInitialiseIMovementBoundResponder).Initialise(_mmBoundResponder);


            #endregion


            #region INPUT SYSTEM

            // INSTANTIATE tempUpdatable as new InputSystem():
            tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<InputSystem>();

            // INITIALISE tempUpdatable with a new PaddleInputResponder():
            (tempUpdatable as IInitialiseIInputResponder).Initialise((_serviceDict["ResponderFactory"] as IFactory<IResponder>).Create<PaddleInputResponder>() as IInputResponder);

            // INITIALISE _serviceDict["SceneManager"] with tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(tempUpdatable);

            #endregion


            #region COLLISION SYSTEM

            // INSTANTIATE tempUpdatable as new CollisionSystem():
            tempUpdatable = (_serviceDict["UpdatableFactory"] as IFactory<IUpdatable>).Create<CollisionSystem>();

            // INITIALISE tempUpdatable with a new PongEntityCollisionResponder():
            (tempUpdatable as IInitialiseICollisionResponder).Initialise((_serviceDict["ResponderFactory"] as IFactory<IResponder>).Create<PongEntityCollisionResponder>() as ICollisionResponder);

            // INITIALISE _serviceDict["SceneManager"] with tempUpdatable:
            (_serviceDict["SceneManager"] as IInitialiseIUpdatable).Initialise(tempUpdatable);

            #endregion


            #endregion


            #region INITIAL ENTITY CREATION (BG & 2 PADDLES)

            // FOR LOOP for creating entities, stops before 'i' reaches 3:
            for (int i = 0; i < 3; i++)
            {
                // ASSIGN _pongEntityID with value of 'i':
                _pongEntityID = i;

                // DECLARE & INSTANTIATE an IEntity as a new Entity, name it 'tempEntity':
                IEntity tempEntity = (_serviceDict["EntityFactory"] as IFactory<IEntity>).Create<Entity>();

                // ASSIGN UID to tempEntity:
                tempEntity.UID = _pongEntityID;

                // ADD tempEntity to _serviceDict['EntityManager']:
                (_serviceDict["EntityManager"] as IEntityManager).AddEntity(tempEntity);
            }

            #endregion


            #region INITIALISING ENTITIES WITH COMPONENTS

            #region BACKGROUND

            // ADD a new TransformComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new LayerComponent to _entityDict[0]'s ComponentList:
            _entityDict[0].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<LayerComponent>());

            #endregion

            /*
            #region PADDLE 1

            // ADD a new TransformComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new HitBoxComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<HitBoxComponent>());

            // ADD a new VelocityComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>());

            // ADD a new PlayerComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<PlayerComponent>());

            // ADD a new LayerComponent to _entityDict[1]'s ComponentList:
            _entityDict[1].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<LayerComponent>());

            #endregion


            #region PADDLE 2

            // ADD a new TransformComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new HitBoxComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<HitBoxComponent>());

            // ADD a new VelocityComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>());

            // ADD a new PlayerComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<PlayerComponent>());

            // ADD a new LayerComponent to _entityDict[2]'s ComponentList:
            _entityDict[2].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<LayerComponent>());

            #endregion
            */

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

            #region SETTING ENTITY COMPONENT VALUES

            #region BACKGROUND

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _entityDict[0]'s TextureComponent:
            ITexture tempTexComp = (_entityDict[0] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // ADD "Background" texture to _entityDict[0]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Background", Content.Load<Texture2D>("Background"));

            // LOAD "Background" as the texture of _entityDict[0]'s TextureComponent:
            tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Background"];

            // SET Layer of _entityDict[0]'s LayerComponent to '1':
            ((_entityDict[0] as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 1;

            #endregion

            /*
            #region PADDLE 1

            // INITIALISE tempTexComp with instance of _entityDict[0]'s TextureComponent:
            tempTexComp = (_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // ADD "Paddle1_DFLT" texture to _entityDict[1]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Paddle1_DFLT", Content.Load<Texture2D>("Paddle1_DFLT"));

            // ADD "Paddle1_INPT" texture to _entityDict[1]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Paddle1_INPT", Content.Load<Texture2D>("Paddle1_INPT"));

            // LOAD "Paddle1_DFLT" as the texture of _entityDict[1]'s TextureComponent:
            tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle1_DFLT"];

            // SET Speed of _entityDict[1]'s VelocityComponent:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Speed = 10;

            // SET PlayerID of _entityDict[1]'s PlayerComponent to PlayerIndex.One:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer).PlayerID = PlayerIndex.One;

            // SET Layer of _entityDict[1]'s LayerComponent to '4':
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 4;

            #endregion


            #region PADDLE 2

            // INITIALISE tempTexComp with instance of _entityDict[2]'s TextureComponent:
            tempTexComp = (_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // ADD "Paddle2_DFLT" texture to _entityDict[2]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Paddle2_DFLT", Content.Load<Texture2D>("Paddle2_DFLT"));

            // ADD "Paddle2_INPT" texture to _entityDict[2]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Paddle2_INPT", Content.Load<Texture2D>("Paddle2_INPT"));

            // LOAD "Paddle2_DFLT" as the texture of _entityDict[2]'s TextureComponent:
            tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle2_DFLT"];

            // SET Speed of _entityDict[2]'s VelocityComponent:
            ((_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Speed = 10;

            // SET PlayerID of _entityDict[2]'s PlayerComponent to PlayerIndex.Two:
            ((_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer).PlayerID = PlayerIndex.Two;

            // SET Layer of _entityDict[2]'s LayerComponent to '4':
            ((_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 4;

            #endregion
            */

            #endregion


            #region SPAWNING ENTITIES

            #region BACKGROUND

            // SPAWN _entityDict[0] with Position at origin:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[0], new Vector2(0));

            #endregion

            /*

            #region PADDLE 1

            // DECLARE & INITIALISE a Texture2D, name it 'tempTexture', get Texture from _entityDict[1] to be spawned on screen:
            Texture2D tempTexture = ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture;

            // SET Origin of _entityDict[1]'s TransformComponent, centre of Texture:
            ((_entityDict[1] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IRotation).Origin = new Vector2(tempTexture.Width / 2, tempTexture.Height / 2);

            // SPAWN _entityDict[1] with Position at left side of screen with some space:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[1], new Vector2(tempTexture.Width, _screenSize.Y / 2));

            #endregion


            #region PADDLE 2

            // INITIALISE tempTexture with the Texture from _entityDict[2]:
            tempTexture = ((_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture;

            // SET Origin of _entityDict[2]'s TransformComponent, centre of Texture:
            ((_entityDict[2] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IRotation).Origin = new Vector2(tempTexture.Width / 2, tempTexture.Height / 2);

            // SPAWN _entityDict[2] with Position at right side of screen with some space:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[2], new Vector2(_screenSize.X - tempTexture.Width, _screenSize.Y / 2));

            #endregion


            #region BALL

            // CALL CreateBall(), for all creation, initialisation and spawn logic of a Ball entity after constant non-replaceable entities e.g. Paddles are created:
            CreateBall();

            #endregion

            */

            // CALL CreateMultipleEntities(), passing 100 as a parameter:
            //CreateMultipleEntities(100);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                // CALL Exit():
                Exit();
            }

            // CALL Update() on _serviceDict["SceneManager"], passing pGameTime as a parameter:
            (_serviceDict["SceneManager"] as IUpdatable).Update(pGameTime);

            // CALL LongTimedTest() on _serviceDict["PerformanceMeasure"], passing pGameTime as a parameter:
            //(_serviceDict["PerformanceMeasure"] as ITestPerformance).LongTimedTest(pGameTime);

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

            // CALL TestFPS() on _serviceDict["PerformanceMeasure"], passing pGameTime as a parameter:
            // CALLED IN DRAW() AS UPDATE() DOES NOT CHANGE FPS
            //(_serviceDict["PerformanceMeasure"] as ITestPerformance).TestFPS(pGameTime);

            // CALL Draw() on base class, passing pGameTime as a parameter:
            base.Draw(pGameTime);
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which recreates a Ball entity to continue Pong after Player scores
        /// </summary>
        private void CreateBall()
        {
            #region CREATION

            // INCREMENT _pongEntityID by 1:
            _pongEntityID++;

            // DECLARE & INSTANTIATE an IEntity as a new Entity, name it 'tempEntity':
            IEntity tempEntity = (_serviceDict["EntityFactory"] as IFactory<IEntity>).Create<Entity>();

            // ASSIGN UID to tempEntity:
            tempEntity.UID = _pongEntityID;

            // ADD tempEntity as a value, and tempEntity.UID as a key to _entityDict:
            _entityDict.Add(tempEntity.UID, tempEntity);

            #endregion


            #region COMPONENT INITIALISATION

            // ADD a new TransformComponent to _entityDict[_pongEntityID]'s ComponentList:
            _entityDict[_pongEntityID].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>());

            // ADD a new TextureComponent to _entityDict[_pongEntityID]'s ComponentList:
            _entityDict[_pongEntityID].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>());

            // ADD a new HitBoxComponent to _entityDict[_pongEntityID]'s ComponentList:
            _entityDict[_pongEntityID].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<HitBoxComponent>());

            // ADD a new VelocityComponent to _entityDict[_pongEntityID]'s ComponentList:
            _entityDict[_pongEntityID].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>());

            // ADD a new LayerComponent to _entityDict[_pongEntityID]'s ComponentList:
            _entityDict[_pongEntityID].AddComponent((_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<LayerComponent>());

            #endregion


            #region SETTING COMPONENT VALUES

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _entityDict[_pongEntityID]'s TextureComponent:
            ITexture tempTexComp = (_entityDict[_pongEntityID] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // ADD "Football" texture to _entityDict[_pongEntityID]'s TextureComponent's Dictionary:
            tempTexComp.ReturnTextureDict().Add("Football", Content.Load<Texture2D>("Football"));

            // LOAD "Football" as the texture of _entityDict[_pongEntityID]'s TextureComponent:
            tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Football"];

            // SET Layer of _entityDict[_pongEntityID]'s LayerComponent to '3':
            ((_entityDict[_pongEntityID] as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 3;

            #region VELOCITY COMPONENT

            // DECLARE & INSTANTIATE a new Vector2, name it '_randDir', with X & Y values of '1':
            Vector2 _randDir = new Vector2(1);

            // IF _rand.Next() lands on '2'
            if (_rand.Next(1, 3) == 2)
            {
                // MULTIPLY _randDir.X by '-1':
                _randDir.X *= -1;
            }

            // IF _rand.Next() lands on '2'
            if (_rand.Next(1, 3) == 2)
            {
                // MULTIPLY _randDir.Y by '-1':
                _randDir.Y *= -1;
            }

            // DECLARE & INITIALISE an IVelocity, name it 'tempVelComp', give value of _entityDict[_pongEntityID]'s VelocityComponent:
            IVelocity tempVelComp = (_entityDict[_pongEntityID] as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SET Speed of _entityDict[_pongEntityID]'s VelocityComponent with value of '5':
            tempVelComp.Speed = 5;

            // SET Direction of _entityDict[_pongEntityID]'s VelocityComponent with value of _randDir:
            tempVelComp.Direction = _randDir;

            // SET Velocity of _entityDict[_pongEntityID]'s VelocityComponent with it's Speed Property multiplied by it's Direction Property:
            tempVelComp.Velocity = tempVelComp.Speed * tempVelComp.Direction;

            #endregion

            #endregion


            #region SPAWN

            // DECLARE & INITIALISE a Texture2D, name it 'tempTexture', get Texture from entity to be spawned on screen:
            Texture2D tempTexture = ((_entityDict[_pongEntityID] as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture;

            // SPAWN _entityDict[_pongEntityID] with Position at the centre of screen:
            (_serviceDict["SceneManager"] as ISpawnEntity).Spawn(_entityDict[_pongEntityID], new Vector2((_screenSize.X / 2) - (tempTexture.Width / 2), (_screenSize.Y / 2) - (tempTexture.Height / 2)));

            #endregion
        }

        /// <summary>
        /// Method which creates as many Ball entities as specified via an integer parameter
        /// </summary>
        /// <param name="pInt"> Number of entities to be created </param>
        private void CreateMultipleEntities(int pInt)
        {
            // DECLARE & INSTANTIATE a Stopwatch, name it 'tempStopwatch':
            //Stopwatch tempStopwatch = new Stopwatch();

            // FORLOOP, iterate for as many times specified by pInt:
            for (int i = 0; i < pInt; i++)
            {
                /*
                // RESET tempStopwatch():
                tempStopwatch.Reset();

                // START tempStopwatch():
                tempStopwatch.Start();
                */

                // CALL CreateBall():
                CreateBall();

                /*
                // STOP tempStopwatch:
                tempStopwatch.Stop();

                // CALL QuickTimedTest() on _serviceDict["PerformanceMeasure"], passing TWO strings, tempStopWatch's elapsed ms, and FALSE as parameters:
                (_serviceDict["PerformanceMeasure"] as ITestPerformance).QuickTimedTest("CreationTest", "ShortTest", tempStopwatch.ElapsedMilliseconds, false);
                */
            }

            /*
            // CALL QuickTimedTest() on _serviceDict["PerformanceMeasure"], passing TWO strings, 0 as null cannot be used, and TRUE as parameters:
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).QuickTimedTest("CreationTest", "ShortTest", 0, true);

            // CALL DeleteMultipleEntities(), passing 100 as a parameter:
            DeleteMultipleEntities(100);

            // CALL UpdateLongTimedTest() on _serviceDict["PerformanceMeasure"]:
            (_serviceDict["PerformanceMeasure"] as ITestPerformance).UpdateLongTimedTest();
            */
        }

        /// <summary>
        /// Method which deletes as many Ball entities as specified via an integer parameter
        /// </summary>
        /// <param name="pInt"> Number of entities to be deleted </param>
        private void DeleteMultipleEntities(int pInt)
        {
            // DECLARE & INSTANTIATE a Stopwatch, name it 'tempStopwatch':
            //Stopwatch tempStopwatch = new Stopwatch();

            // IF pInt DOES NOT exceed the number of entities in _entityDict:
            if (pInt <= _entityDict.Count)
            {
                // DECLARE & INITIALISE an int with the value of _entityDict.Count - 1 due to using index 0, name it 'tempEntCount':
                int tempEntCount = _entityDict.Count - 1;

                // FORLOOP, iterate for as many times specified by pInt:
                for (int i = tempEntCount; i > tempEntCount - pInt; i--)
                {
                    /*
                    // RESET tempStopwatch():
                    tempStopwatch.Reset();

                    // START tempStopwatch():
                    tempStopwatch.Start();
                    */

                    // REMOVE entity stored at address 'i' from "EntityManager":
                    (_serviceDict["EntityManager"] as IEntityManager).Terminate(i);

                    /*
                    // STOP tempStopwatch:
                    tempStopwatch.Stop();

                    // CALL QuickTimedTest() on _serviceDict["PerformanceMeasure"], passing TWO strings, tempStopWatch's elapsed ms, and FALSE as parameters:
                    (_serviceDict["PerformanceMeasure"] as ITestPerformance).QuickTimedTest("TerminationTest", "ShortTest", tempStopwatch.ElapsedMilliseconds, false);
                    */

                    // DECREMENT _pongEntityID by '1':
                    _pongEntityID--;
                }

                // CALL QuickTimedTest() on _serviceDict["PerformanceMeasure"], passing TWO strings, 0 as null cannot be used, and TRUE as parameters:
                //(_serviceDict["PerformanceMeasure"] as ITestPerformance).QuickTimedTest("TerminationTest", "ShortTest", 0, true);

                // CALL Collect on Garbage Collector to ensure memory collection after termination:
                GC.Collect();
            }
            // IF pInt DOES exceed the number of entities in _entityDict:
            else
            {
                // WRITE to console explaining that user cannot delete more than the current entity count:
                Console.WriteLine("ERROR: You cannot delete more entities than the number currently stored in the Entity Dictionary!");
            }
        }

        #endregion
    }
}
