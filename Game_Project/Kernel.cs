using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using COMP3401_Project.ECSPackage.Components;
using COMP3401_Project.ECSPackage.Entities;
using COMP3401_Project.ECSPackage.Factories;
using COMP3401_Project.ECSPackage.Services;
using COMP3401_Project.ECSPackage.Systems;

namespace COMP3401_Project
{
    /// <summary>
    /// Main Class of ECS System
    /// Author: William Smith
    /// Date: 01/11/21
    /// </summary>
    public class Kernel : Game
    {
        // DECLARE a GraphicsDeviceManager, name it '_graphics':
        private GraphicsDeviceManager _graphics;

        // DECLARE a SpriteBatch, name it '_spriteBatch':
        private SpriteBatch _spriteBatch;

        // DECLARE a Vector2, name it '_screenSize':
        private Vector2 _screenSize;

        // DECLARE an IDictionary<string, IService>, name it '_serviceDict':
        private IDictionary<string, IService> _serviceDict;

        // DECLARE an IDictionary<int, IEntity>, name it '_entityDict':
        private IDictionary<int, IEntity> _entityDict;

        // DECLARE an IList<IUpdatable>, name it '_systemList':
        private IList<IUpdatable> _systemList;


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

            // ADD a new Factory<IEntity>() to _serviceDict:
            _serviceDict.Add("EntityFactory", new Factory<IEntity>());

            // ADD a new Factory<IComponent>() to _serviceDict:
            _serviceDict.Add("ComponentFactory", new Factory<IComponent>());


            //_serviceDict.Add("SystemFactory", new Factory<ISystem>());

            // INSTANTIATE _entityDict as new Dictionary<int, IEntity>:
            _entityDict = new Dictionary<int, IEntity>();

            // INSTANTIATE _systemList as new List<IUpdatable>():
            _systemList = new List<IUpdatable>();

            #endregion


            #region INITIALISING _SYSTEMLIST 

            #region DRAW SYSTEM

            // DECLARE & INSTANTIATE an IUpdatable as a new DrawSystem(), name it '_tempUpdatable':
            IUpdatable _tempUpdatable = new DrawSystem();

            // INITIALISE _tempUpdatable with reference to _entityDict:
            (_tempUpdatable as IInitialiseIROIEntityDictionary).Initialise(_entityDict as IReadOnlyDictionary<int, IEntity>);

            // ADD _tempUpdatable to _systemList():
            _systemList.Add(_tempUpdatable);

            #endregion


            #region MOVEMENT SYSTEM

            // INSTANTIATE _tempUpdatable as new MovementSystem():
            _tempUpdatable = new MovementSystem();

            // INITIALISE _tempUpdatable with reference to _entityDict:
            (_tempUpdatable as IInitialiseIROIEntityDictionary).Initialise(_entityDict as IReadOnlyDictionary<int, IEntity>);

            // ADD _tempUpdatable to _systemList():
            _systemList.Add(_tempUpdatable);

            #endregion

            #endregion


            #region ENTITY CREATION

            // DECLARE & INSTANTIATE an IEntity as a new Entity, name it '_tempEntity':
            IEntity _tempEntity = (_serviceDict["EntityFactory"] as IFactory<IEntity>).Create<Entity>();

            // ASSIGN UID to _tempEntity:
            _tempEntity.UID = 1;

            // ADD _tempEntity as a value, and _tempEntity.UID as a key to _entityDict:
            _entityDict.Add(_tempEntity.UID, _tempEntity);

            // INSTANTIATE a new Entity:
            _tempEntity = (_serviceDict["EntityFactory"] as IFactory<IEntity>).Create<Entity>();

            // ASSIGN UID to _tempEntity:
            _tempEntity.UID = 2;

            // ADD _tempEntity as a value, and _tempEntity.UID as a key to _entityDict:
            _entityDict.Add(_tempEntity.UID, _tempEntity);

            #endregion


            #region COMPONENT LIST INITIALISATION

            // FOREACH IEntity instance in _entityDict:
            foreach (IEntity pEntity in _entityDict.Values)
            {
                // DECLARE & INSTANTIATE an IList<IComponent> as a new List<IComponent>(), name it '_componentList':
                IList<IComponent> _componentList = new List<IComponent>();

                // INITIALISE pEntity with _componentList:
                (pEntity as IInitialiseIComponentList).Initialise(_componentList);
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

            // DECLARE an IComponent, name it '_tempTransformComp', used for all entities which need a TransformComponent:
            IComponent _tempTransformComp;

            // DECLARE an IComponent, name it '_tempTextureComp', used for all entities which need a TextureComponent:
            IComponent _tempTextureComp;

            // DECLARE an IComponent, name it '_tempVelocityComp', used for all entities which need a VelocityComponent:
            IComponent _tempVelocityComp;

            #region IMAGE 1

            // INSTANTIATE _tempTransformComp as new TransformComponent():
            _tempTransformComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>();

            // SET Position of _tempTransformComp to centre of screen:
            (_tempTransformComp as IPosition).Position = _screenSize / 2;

            // INSTANTIATE _tempTransformComp as new TextureComponent():
            _tempTextureComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>();

            // LOAD "ServerLogo" as the texture of _tempTextureComp:
            (_tempTextureComp as ITexture).Texture = Content.Load<Texture2D>("Server Logo");

            // INSTANTIATE _tempVelocityComp as new VelocityComponent():
            _tempVelocityComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>();

            // SET Speed of _tempVelocityComp:
            (_tempVelocityComp as IVelocity).Speed = 5;

            // SET Direction of _tempVelocityComp:
            (_tempVelocityComp as IVelocity).Direction = new Vector2(1);

            // ADD _tempTransformComp to _entityDict[1]'s ComponentList:
            (_entityDict[1] as IContainList).ComponentList.Add(_tempTransformComp);

            // ADD _tempTextureComp to _entityDict[1]'s ComponentList:
            (_entityDict[1] as IContainList).ComponentList.Add(_tempTextureComp);

            // ADD _tempVelocityComp to _entityDict[1]'s ComponentList:
            (_entityDict[1] as IContainList).ComponentList.Add(_tempVelocityComp);

            #endregion


            #region IMAGE 2

            // INSTANTIATE _tempTransformComp as new TransformComponent():
            _tempTransformComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TransformComponent>();

            // SET Position of _tempTransformComp to centre of screen:
            (_tempTransformComp as IPosition).Position = _screenSize / _screenSize;

            // INSTANTIATE _tempTransformComp as new TextureComponent():
            _tempTextureComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<TextureComponent>();

            // LOAD "ServerLogo" as the texture of _tempTextureComp:
            (_tempTextureComp as ITexture).Texture = Content.Load<Texture2D>("Server Logo");

            // INSTANTIATE _tempVelocityComp as new VelocityComponent():
            _tempVelocityComp = (_serviceDict["ComponentFactory"] as IFactory<IComponent>).Create<VelocityComponent>();

            // SET Speed of _tempVelocityComp:
            (_tempVelocityComp as IVelocity).Speed = 2;

            // SET Direction of _tempVelocityComp:
            (_tempVelocityComp as IVelocity).Direction = new Vector2(-1);

            // ADD _tempTransformComp to _entityDict[1]'s ComponentList:
            (_entityDict[2] as IContainList).ComponentList.Add(_tempTransformComp);

            // ADD _tempTextureComp to _entityDict[1]'s ComponentList:
            (_entityDict[2] as IContainList).ComponentList.Add(_tempTextureComp);

            // ADD _tempVelocityComp to _entityDict[2]'s ComponentList:
            (_entityDict[2] as IContainList).ComponentList.Add(_tempVelocityComp);

            #endregion

            #endregion



            // TODO: use this.Content to load your game content here
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
                Exit();

            
            // FOREACH IUpdatable instance in _systemList:
            foreach (IUpdatable pUpdatable in _systemList)
            {
                // CALL Update() on pUpdatable, passing pGameTime as a parameter:
                pUpdatable.Update(pGameTime);
            }

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

            // FOREACH IUpdatable instance in _systemList:
            foreach (IUpdatable pUpdatable in _systemList)
            {
                // IF pUpdatable implements IDraw:
                if (pUpdatable is IDraw)
                {
                    // CALL Draw() on pUpdatable, passing _spriteBatch as a parameter:
                    (pUpdatable as IDraw).Draw(_spriteBatch);
                }
            }

            // CALL Draw() on base class, passing pGameTime as a parameter:
            base.Draw(pGameTime);
        }
    }
}
