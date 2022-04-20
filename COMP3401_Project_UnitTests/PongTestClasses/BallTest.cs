using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Delegates.Interfaces;
using COMP3401ECS_Engine.Entities;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Systems.Interfaces;
using COMP3401ECS_Engine.Systems.Managers;
using COMP3401ECS_Engine.Systems.Managers.Interfaces;
using COMP3401_Project.PongPackage.Responders;


namespace COMP3401_Project_UnitTests.PongTestClasses
{
    /// <summary>
    /// Test Class for an Entity that acts as a Ball in game
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    [TestClass]
    public class BallTest
    {
        #region FIELD VARIABLES

        // DECLARE TWO Point variables, name them, '_minXYBounds' & '_maxXYBounds':
        private Point _minXYBounds, _maxXYBounds;

        #endregion


        #region BALL CREATION (MAKES EASIER TO READ TESTS)

        /// <summary>
        /// Method which creates a Ball entity for Test Methods
        /// </summary>
        public IEntity CreateBall()
        {
            #region MONOGAME SETUP

            // SET value of _minXYBounds to (0,0):
            _minXYBounds = new Point(0);

            // SET value of _maxXYBounds to max Window Size of 1600x800:
            _maxXYBounds = new Point(1600, 800);

            #endregion


            #region CREATION

            // DECLARE & INSTANTIATE an IEntity as a new Entity, name it 'tempEntity':
            IEntity tempEntity = new Entity();

            // ASSIGN UID to tempEntity:
            tempEntity.UID = 1;

            #endregion


            #region COMPONENT INITIALISATION

            // ADD a new TransformComponent to tempEntity's ComponentList:
            tempEntity.AddComponent(new TransformComponent());

            // ADD a new TextureComponent to tempEntity's ComponentList:
            tempEntity.AddComponent(new TextureComponent());

            // ADD a new HitBoxComponent to tempEntity's ComponentList:
            tempEntity.AddComponent(new HitBoxComponent());

            // ADD a new VelocityComponent to tempEntity's ComponentList:
            tempEntity.AddComponent(new VelocityComponent());

            // ADD a new LayerComponent to tempEntity's ComponentList:
            tempEntity.AddComponent(new LayerComponent());

            #endregion


            #region SETTING COMPONENT VALUES

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of tempEntity's TextureComponent:
            ITexture tempTexComp = (tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // SET & MOCK a texture size of 20x20 to tempEntity's TextureComponent:
            tempTexComp.TexSize = new Point(20, 20);

            // SET Layer of tempEntity's LayerComponent to '3':
            ((tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 3;

            // DECLARE & INITIALISE an IVelocity, name it 'tempVelComp', give value of tempEntity's VelocityComponent:
            IVelocity tempVelComp = (tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SET Speed of tempEntity's VelocityComponent with value of '5':
            tempVelComp.Speed = 5;

            #endregion


            #region RETURNING ENTITY TO CALLER

            // RETURN instance of tempEntity:
            return tempEntity;

            #endregion
        }

        #endregion


        #region TOP BOUND 

        /// <summary>
        /// Tests if Ball Reverses Y Direction when in contact with top of screen
        /// </summary>
        [TestMethod]
        public void ContactWithTopOfScreen()
        {
            #region ARRANGE

            #region BALL

            // DECLARE & INSTANTIATE an IEntity using CreateBall(), name it '_ball':
            IEntity _ball = CreateBall();

            // DECLARE & INITIALISE an IPosition, give value of _ball's TransformComponent, name it '_ballTfComp':
            IPosition _ballTfComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _ball's TextureComponent:
            ITexture _ballTexComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE an IVelocity, give value of _ball's VelocityComponent, name it '_ballVelComp':
            IVelocity _ballVelComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SPAWN _ball in top middle of screen, Y axis value of _minXYBounds.Y, so it is already in contact before running code:
            _ballTfComp.Position = new Vector2((_maxXYBounds.X / 2) - _ballTexComp.TexSize.X / 2, _minXYBounds.Y);

            // SET Direction of _ball to -1, to head upwards:
            _ballVelComp.Direction = new Vector2(0, -1);

            // SET Velocity of _ball's VelocityComponent with it's Speed Property multiplied by it's Direction Property:
            _ballVelComp.Velocity = _ballVelComp.Speed * _ballVelComp.Direction;

            #endregion


            #region PONGMOVEMENTBOUNDRESPONDER

            // DECLARE & INSTANTIATE an IMovementBoundResponder as a new PongMovementBoundResponder, name it 'mmBoundResponder':
            IMovementBoundResponder _mmBoundResponder = new PongMovementBoundResponder();

            // SET value of _minXYBounds to value of _minXYBounds:
            _mmBoundResponder.MinXYBound = _minXYBounds;

            // SET value of _minXYBounds to value of _maxXYBounds:
            _mmBoundResponder.MaxXYBound = _maxXYBounds;

            #endregion

            #endregion


            #region ACT

            // CALL _RespondToBound on _mmBoundResponder, passing _ball as a parameter:
            _mmBoundResponder.RespondToBound(_ball);

            #endregion


            #region ASSERT

            // ASSERT that Ball has reversed Y axis Velocity to a positive number:
            Assert.AreEqual(_ballVelComp.Speed * 1, _ballVelComp.Velocity.Y);

            #endregion
        }

        #endregion


        #region BOTTOM BOUND

        /// <summary>
        /// Tests if Ball Reverses Y Direction when in contact with bottom of screen
        /// </summary>
        [TestMethod]
        public void ContactWithBottomOfScreen()
        {
            #region ARRANGE

            #region BALL

            // DECLARE & INSTANTIATE an IEntity using CreateBall(), name it '_ball':
            IEntity _ball = CreateBall();

            // DECLARE & INITIALISE an IPosition, give value of _ball's TransformComponent, name it '_ballTfComp':
            IPosition _ballTfComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _ball's TextureComponent:
            ITexture _ballTexComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE an IVelocity, give value of _ball's VelocityComponent, name it '_ballVelComp':
            IVelocity _ballVelComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SPAWN _ball in bottom middle of screen, Y axis value of _maxXYBounds - _ballTexComp.TexSize.Y, so it is already in contact before running code:
            _ballTfComp.Position = new Vector2((_maxXYBounds.X / 2) - _ballTexComp.TexSize.X / 2, _maxXYBounds.Y - _ballTexComp.TexSize.Y);

            // SET Direction of _ball to 1, to head downwards:
            _ballVelComp.Direction = new Vector2(0, 1);

            // SET Velocity of _ball's VelocityComponent with it's Speed Property multiplied by it's Direction Property:
            _ballVelComp.Velocity = _ballVelComp.Speed * _ballVelComp.Direction;

            #endregion


            #region PONGMOVEMENTBOUNDRESPONDER

            // DECLARE & INSTANTIATE an IMovementBoundResponder as a new PongMovementBoundResponder, name it 'mmBoundResponder':
            IMovementBoundResponder _mmBoundResponder = new PongMovementBoundResponder();

            // SET value of _minXYBounds to value of _minXYBounds:
            _mmBoundResponder.MinXYBound = _minXYBounds;

            // SET value of _minXYBounds to value of _maxXYBounds:
            _mmBoundResponder.MaxXYBound = _maxXYBounds;

            #endregion

            #endregion


            #region ACT

            // CALL _RespondToBound on _mmBoundResponder, passing _ball as a parameter:
            _mmBoundResponder.RespondToBound(_ball);

            #endregion


            #region ASSERT

            // ASSERT that Ball has reversed Y axis Velocity to a negative number:
            Assert.AreEqual(_ballVelComp.Speed * -1, _ballVelComp.Velocity.Y);

            #endregion
        }

        #endregion


        #region COLLISION

        /// <summary>
        /// Tests if Ball reverses X axis Direction when in contact with another Entity
        /// </summary>
        [TestMethod]
        public void CollisionWithSecondEntity()
        {
            #region ARRANGE

            #region BALL

            // DECLARE & INSTANTIATE an IEntity using CreateBall(), name it '_ball':
            IEntity _ball = CreateBall();

            // DECLARE & INITIALISE an IPosition, give value of _ball's TransformComponent, name it '_ballTfComp':
            IPosition _ballTfComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _ball's TextureComponent:
            ITexture _ballTexComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE an IVelocity, give value of _ball's VelocityComponent, name it '_ballVelComp':
            IVelocity _ballVelComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SPAWN _ball in bottom middle of screen, Y axis value of _maxXYBounds - _ballTexComp.TexSize.Y, so it is already in contact before running code:
            _ballTfComp.Position = new Vector2((_maxXYBounds.X / 2) - _ballTexComp.TexSize.X / 2, _maxXYBounds.Y - _ballTexComp.TexSize.Y);

            // SET Direction of _ball to -1, to head left:
            _ballVelComp.Direction = new Vector2(-1, 0);

            // SET Velocity of _ball's VelocityComponent with it's Speed Property multiplied by its Direction Property:
            _ballVelComp.Velocity = _ballVelComp.Speed * _ballVelComp.Direction;

            #endregion


            #region OTHER ENTITY

            // DECLARE & INSTANTIATE an IEntity as a new Entity(), name it '_scndEntity':
            // USED TO MAKE RESPONDTOCOLLISION() NOT BREAK AT RUNTIME
            IEntity _scndEntity = new Entity();

            // ADD new VelocityComponent to _scndEntity():
            _scndEntity.AddComponent(new VelocityComponent());


            #endregion


            #region PONGMOVEMENTBOUNDRESPONDER

            // DECLARE & INSTANTIATE an ICollisionResponder as a new PongEntityCollisionResponder, name it '_eCollisionResponder':
            ICollisionResponder _eCollisionResponder = new PongEntityCollisionResponder();

            #endregion

            #endregion


            #region ACT

            // CALL RespondToCollision() on _eCollisionResponder, passing _ball and _scndEntity as parameters:
            _eCollisionResponder.RespondToCollision(_ball, _scndEntity);

            #endregion


            #region ASSERT

            // ASSERT that Ball has reversed X axis Velocity:
            Assert.AreEqual(_ballVelComp.Speed * 1, _ballVelComp.Velocity.X);

            #endregion
        }

        #endregion


        #region TERMINATION

        /// <summary>
        /// Tests if Ball has been terminated when exiting off of the left side of the screen
        /// </summary>
        [TestMethod]
        public void LeftOfScreenTermination()
        {
            #region ARRANGE

            #region MANAGERS

            // DECLARE & INSTANTIATE an IEntityManager as a new EntityManager, name it 'entityManager':
            IEntityManager entityManager = new EntityManager();

            // DECLARE & INSTANTIATE an ISceneManager as a new SceneManager, name it 'sceneManager':
            ISceneManager sceneManager = new SceneManager();

            // INITIALISE _entityManager with _sceneManager:
            (entityManager as IInitialiseISceneManager).Initialise(sceneManager);

            // INITIALISE _sceneManager with a new SceneGraph():
            (sceneManager as IInitialiseISpawnEntity).Initialise(new SceneGraph());

            #endregion


            #region BALL

            // DECLARE & INSTANTIATE an IEntity using CreateBall(), name it '_ball':
            IEntity ball = CreateBall();

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of ball's TextureComponent:
            ITexture ballTexComp = (ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE an IVelocity, give value of ball's VelocityComponent, name it 'ballVelComp':
            IVelocity ballVelComp = (ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SET Direction of ball to -1, to head left:
            ballVelComp.Direction = new Vector2(-1, 0);

            // SET Velocity of ball's VelocityComponent with it's Speed Property multiplied by it's Direction Property:
            ballVelComp.Velocity = ballVelComp.Speed * ballVelComp.Direction;

            // ADD tempEntity to entityManager:
            entityManager.AddEntity(ball);

            // SPAWN ball in left middle of screen, X axis value of _minXYBounds.X, so it is already in contact before running code:
            (sceneManager as ISpawnEntity).Spawn(ball, new Vector2(_minXYBounds.X, (_maxXYBounds.Y / 2) - (ballTexComp.TexSize.Y / 2)));

            #endregion


            #region PONGMOVEMENTBOUNDRESPONDER

            // DECLARE & INSTANTIATE an IMovementBoundResponder as a new PongMovementBoundResponder, name it 'mmBoundResponder':
            IMovementBoundResponder mmBoundResponder = new PongMovementBoundResponder();

            // SET value of MinXYBounds to value of _minXYBounds:
            mmBoundResponder.MinXYBound = _minXYBounds;

            // SET value of MaxXYBounds to value of _maxXYBounds:
            mmBoundResponder.MaxXYBound = _maxXYBounds;

            // INITIALISE mmBoundResponder with DummyCreate():
            (mmBoundResponder as IInitialiseCreateDel).Initialise(DummyCreate);

            // INITIALISE mmBoundResponder with entityManager.Terminate():
            (mmBoundResponder as IInitialiseDeleteDel).Initialise(entityManager.Terminate);

            #endregion

            #endregion


            #region ACT

            // CALL RespondToBound on mmBoundResponder, passing ball as a parameter:
            mmBoundResponder.RespondToBound(ball);

            #endregion


            #region ASSERT

            // IF entityManager DOES NOT contain an Entity ID'd by '1':
            if (!(entityManager as IRtnEntityDictionary).ReturnEntityDict().ContainsKey(ball.UID))
            {
                // DO NOTHING, PASSES
            }
            // IF _entityManager DOES contain an Entity ID'd by '1':
            else
            {
                // ASSERT that the test has failed, with corresponding message:
                Assert.Fail("ERROR: _ball has an active instance!");
            }

            #endregion
        }

        /// <summary>
        /// Tests if Ball has been terminated when exiting off of the right side of the screen
        /// </summary>
        [TestMethod]
        public void RightOfScreenTermination()
        {
            #region ARRANGE

            #region MANAGERS

            // DECLARE & INSTANTIATE an IEntityManager as a new EntityManager, name it '_entityManager':
            IEntityManager _entityManager = new EntityManager();

            // DECLARE & INSTANTIATE an ISceneManager as a new SceneManager, name it '_sceneManager':
            ISceneManager _sceneManager = new SceneManager();

            // INITIALISE _entityManager with _sceneManager:
            (_entityManager as IInitialiseISceneManager).Initialise(_sceneManager);

            // INITIALISE _sceneManager with a new SceneGraph():
            (_sceneManager as IInitialiseISpawnEntity).Initialise(new SceneGraph());

            #endregion


            #region BALL

            // DECLARE & INSTANTIATE an IEntity using CreateBall(), name it '_ball':
            IEntity _ball = CreateBall();

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of _ball's TextureComponent:
            ITexture _ballTexComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE an IVelocity, give value of _ball's VelocityComponent, name it '_ballVelComp':
            IVelocity _ballVelComp = (_ball as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SET Direction of _ball to 1, to head right:
            _ballVelComp.Direction = new Vector2(1, 0);

            // SET Velocity of _ball's VelocityComponent with it's Speed Property multiplied by it's Direction Property:
            _ballVelComp.Velocity = _ballVelComp.Speed * _ballVelComp.Direction;

            // ADD tempEntity to _entityManager:
            _entityManager.AddEntity(_ball);

            // SPAWN _ball in left middle of screen, X axis value of _maxXYBounds.X, so it is already in contact before running code:
            (_sceneManager as ISpawnEntity).Spawn(_ball, new Vector2(_maxXYBounds.X - _ballTexComp.TexSize.X, (_maxXYBounds.Y / 2) - (_ballTexComp.TexSize.Y / 2)));

            #endregion


            #region PONGMOVEMENTBOUNDRESPONDER

            // DECLARE & INSTANTIATE an IMovementBoundResponder as a new PongMovementBoundResponder, name it 'mmBoundResponder':
            IMovementBoundResponder _mmBoundResponder = new PongMovementBoundResponder();

            // SET value of _minXYBounds to value of _minXYBounds:
            _mmBoundResponder.MinXYBound = _minXYBounds;

            // SET value of _minXYBounds to value of _maxXYBounds:
            _mmBoundResponder.MaxXYBound = _maxXYBounds;

            // INITIALISE _mmBoundResponder with DummyCreate():
            (_mmBoundResponder as IInitialiseCreateDel).Initialise(DummyCreate);

            // INITIALISE _mmBoundResponder with _entityManager.Terminate():
            (_mmBoundResponder as IInitialiseDeleteDel).Initialise(_entityManager.Terminate);

            #endregion

            #endregion


            #region ACT

            // CALL _RespondToBound on _mmBoundResponder, passing _ball as a parameter:
            _mmBoundResponder.RespondToBound(_ball);

            #endregion


            #region ASSERT

            // IF _entityManager DOES NOT contain an Entity ID'd by '1':
            if (!(_entityManager as IRtnEntityDictionary).ReturnEntityDict().ContainsKey(_ball.UID))
            {
                // DO NOTHING, PASSES
            }
            // IF _entityManager DOES contain an Entity ID'd by '1':
            else
            {
                // ASSERT that the test has failed, with corresponding message:
                Assert.Fail("ERROR: _ball has an active instance!");
            }

            #endregion
        }

        #endregion


        #region DUMMY METHODS

        /// <summary>
        /// Dummy Create Method to initialise a CreateDelegate with to prevent runtime error
        /// </summary>
        private void DummyCreate()
        {
            // DOES NOTHING, CANNOT USE CREATEBALL() IN KERNEL DUE TO ISSUES WITH CREATING GRAPHICS DEVICE
        }

        #endregion
    }
}