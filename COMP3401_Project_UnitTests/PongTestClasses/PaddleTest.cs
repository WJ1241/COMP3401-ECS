using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Entities;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Components;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;
using COMP3401_Project.PongPackage.Responders;
using COMP3401_Project.PongPackage.Responders.Interfaces;

namespace COMP3401_Project_UnitTests.PongTestClasses
{
    /// <summary>
    /// Test Class for an Entity that acts as a Paddle in game
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    [TestClass]
    public class PaddleTest
    {
        #region FIELD VARIABLES

        // DECLARE TWO Point variables, name them, '_minXYBounds' & '_maxXYBounds':
        private Point _minXYBounds, _maxXYBounds;

        #endregion


        #region PADDLE CREATION (MAKES EASIER TO READ TESTS)

        /// <summary>
        /// Method which creates a Paddle entity for Test Methods
        /// </summary>
        public IEntity CreatePaddle()
        {
            #region MONOGAME SETUP

            // SET value of _minXYBounds to (0,0):
            _minXYBounds = new Point(0);

            // SET value of _maxXYBounds to max Window Size of 1600x800:
            _maxXYBounds = new Point(1600, 800);

            #endregion


            #region CREATION

            // DECLARE & INSTANTIATE an IEntity as a new Entity, name it '_tempEntity':
            IEntity _tempEntity = new Entity();

            // ASSIGN UID to _tempEntity:
            _tempEntity.UID = 1;

            #endregion


            #region COMPONENT INITIALISATION

            // ADD a new TransformComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new TransformComponent());

            // ADD a new TextureComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new TextureComponent());

            // ADD a new HitBoxComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new HitBoxComponent());

            // ADD a new VelocityComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new VelocityComponent());

            // ADD a new LayerComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new LayerComponent());

            // ADD a new PlayerComponent to _tempEntity's ComponentList:
            _tempEntity.AddComponent(new PlayerComponent());

            #endregion


            #region SETTING COMPONENT VALUES

            // DECLARE & INITIALISE an ITexture, name it '_tempTexComp', give instance of _tempEntity's TextureComponent:
            ITexture _tempTexComp = (_tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // SET & MOCK a texture size of 20x60 to _tempEntity's TextureComponent:
            _tempTexComp.TexSize = new Point(20, 60);

            // SET Layer of _tempEntity's LayerComponent to '4':
            ((_tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer = 4;

            // DECLARE & INITIALISE an IVelocity, name it '_tempVelComp', give value of _tempEntity's VelocityComponent:
            IVelocity _tempVelComp = (_tempEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // SET Speed of _tempEntity's VelocityComponent with value of '10':
            _tempVelComp.Speed = 10;

            #endregion


            #region RETURNING ENTITY TO CALLER

            // RETURN instance of _tempEntity:
            return _tempEntity;

            #endregion
        }

        #endregion


        #region UP MOVEMENT

        /// <summary>
        /// Tests if Player One's Direction heads upwards when 'W' is Pressed
        /// </summary>
        [TestMethod]
        public void PlayerOneMoveUp()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IVelocity, name it '_paddleVelComp', give value of _paddle's VelocityComponent:
            IVelocity _paddleVelComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // DECLARE & INITIALISE an IPlayer, name it '_paddlePlayComp', give value of _paddle's PlayerComponent:
            IPlayer _paddlePlayComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer;

            // SET PlayerID of _paddle's PlayerComponent to PlayerIndex.One:
            _paddlePlayComp.PlayerID = PlayerIndex.One;

            #endregion


            #region PONGINPUTRESPONDER

            // DECLARE & INSTANTIATE an IInputResponder as a new PaddleInputResponder, name it '_inputResponder':
            IInputResponder _inputResponder = new PaddleInputResponder();

            #endregion

            #endregion


            #region ACT

            // SET input to 'W' Key:
            (_inputResponder as ITestKBInput).SetKeyPressed = "W";

            // CALL RespondToInput on _inputResponder, passing _paddle as a parameter:
            _inputResponder.RespondToInput(_paddle);

            #endregion
            

            #region ASSERT

            // ASSERT that Paddle is moving upwards:
            Assert.AreEqual(_paddleVelComp.Speed * -1, _paddleVelComp.Velocity.Y);

            #endregion
        }

        /// <summary>
        /// Tests if Player Two's Direction heads upwards when Up Arrow is Pressed
        /// </summary>
        [TestMethod]
        public void PlayerTwoMoveUp()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IVelocity, name it '_paddleVelComp', give value of _paddle's VelocityComponent:
            IVelocity _paddleVelComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // DECLARE & INITIALISE an IPlayer, name it '_paddlePlayComp', give value of _paddle's PlayerComponent:
            IPlayer _paddlePlayComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer;

            // SET PlayerID of _paddle's PlayerComponent to PlayerIndex.Two:
            _paddlePlayComp.PlayerID = PlayerIndex.Two;

            #endregion


            #region PONGINPUTRESPONDER

            // DECLARE & INSTANTIATE an IInputResponder as a new PaddleInputResponder, name it '_inputResponder':
            IInputResponder _inputResponder = new PaddleInputResponder();

            #endregion

            #endregion


            #region ACT

            // SET input to Up Arrow Key:
            (_inputResponder as ITestKBInput).SetKeyPressed = "Up";

            // CALL RespondToInput on _inputResponder, passing _paddle as a parameter:
            _inputResponder.RespondToInput(_paddle);

            #endregion


            #region ASSERT

            // ASSERT that Paddle is moving upwards:
            Assert.AreEqual(_paddleVelComp.Speed * -1, _paddleVelComp.Velocity.Y);

            #endregion
        }

        #endregion


        #region DOWN MOVEMENT

        /// <summary>
        /// Tests if Player One's Direction heads downwards when 'S' is Pressed
        /// </summary>
        [TestMethod]
        public void PlayerOneMoveDown()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IVelocity, name it '_paddleVelComp', give value of _paddle's VelocityComponent:
            IVelocity _paddleVelComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // DECLARE & INITIALISE an IPlayer, name it '_paddlePlayComp', give value of _paddle's PlayerComponent:
            IPlayer _paddlePlayComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer;

            // SET PlayerID of _paddle's PlayerComponent to PlayerIndex.One:
            _paddlePlayComp.PlayerID = PlayerIndex.One;

            #endregion


            #region PONGINPUTRESPONDER

            // DECLARE & INSTANTIATE an IInputResponder as a new PaddleInputResponder, name it '_inputResponder':
            IInputResponder _inputResponder = new PaddleInputResponder();

            #endregion

            #endregion


            #region ACT

            // SET input to 'S' Key:
            (_inputResponder as ITestKBInput).SetKeyPressed = "S";

            // CALL RespondToInput on _inputResponder, passing _paddle as a parameter:
            _inputResponder.RespondToInput(_paddle);

            #endregion


            #region ASSERT

            // ASSERT that Paddle is moving downwards:
            Assert.AreEqual(_paddleVelComp.Speed * 1, _paddleVelComp.Velocity.Y);

            #endregion
        }

        /// <summary>
        /// Tests if Player Two's Direction heads downwards when Down Arrow is Pressed
        /// </summary>
        [TestMethod]
        public void PlayerTwoMoveDown()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IVelocity, name it '_paddleVelComp', give value of _paddle's VelocityComponent:
            IVelocity _paddleVelComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // DECLARE & INITIALISE an IPlayer, name it '_paddlePlayComp', give value of _paddle's PlayerComponent:
            IPlayer _paddlePlayComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer;

            // SET PlayerID of _paddle's PlayerComponent to PlayerIndex.Two:
            _paddlePlayComp.PlayerID = PlayerIndex.Two;

            #endregion


            #region PONGINPUTRESPONDER

            // DECLARE & INSTANTIATE an IInputResponder as a new PaddleInputResponder, name it '_inputResponder':
            IInputResponder _inputResponder = new PaddleInputResponder();

            #endregion

            #endregion


            #region ACT

            // SET input to Down Arrow Key:
            (_inputResponder as ITestKBInput).SetKeyPressed = "Down";

            // CALL RespondToInput on _inputResponder, passing _paddle as a parameter:
            _inputResponder.RespondToInput(_paddle);

            #endregion


            #region ASSERT

            // ASSERT that Paddle is moving downwards:
            Assert.AreEqual(_paddleVelComp.Speed * 1, _paddleVelComp.Velocity.Y);

            #endregion
        }

        #endregion


        #region TOP BOUND 

        /// <summary>
        /// Tests if Paddle Reverts Position to top of Y axis when in contact with top of screen
        /// </summary>
        [TestMethod]
        public void ContactWithTopOfScreen()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IPosition, name it '_paddleTfComp', give value of _paddle's TransformComponent:
            IPosition _paddleTfComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an IRotation, name it '_paddleRotComp', give value of _paddleTfComp:
            IRotation _paddleRotComp = _paddleTfComp as IRotation;

            // DECLARE & INITIALISE an ITexture, name it '_paddleTexComp', give value of _paddle's TextureComponent:
            ITexture _paddleTexComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // SET Origin Property of _paddleRotComp to middle of _paddleTexComp.TexSize:
            _paddleRotComp.Origin = new Vector2(_paddleTexComp.TexSize.X / 2, _paddleTexComp.TexSize.Y / 2);

            // SPAWN _paddle above the top left of screen, Y axis value of _minXYBounds.Y + _paddleRotComp.Origin.Y - 1, so it is already off screen before running code:
            _paddleTfComp.Position = new Vector2(_minXYBounds.X + _paddleRotComp.Origin.X, _minXYBounds.Y + _paddleRotComp.Origin.Y - 1);

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

            // CALL _RespondToBound on _mmBoundResponder, passing _paddle as a parameter:
            _mmBoundResponder.RespondToBound(_paddle);

            #endregion


            #region ASSERT

            // ASSERT that Paddle has reverted Y axis Position back to top of screen:
            Assert.AreEqual(_minXYBounds.Y, _paddleTfComp.Position.Y - _paddleRotComp.Origin.Y);

            #endregion
        }

        #endregion


        #region BOTTOM BOUND

        /// <summary>
        /// Tests if Paddle Reverts Position to bottom of Y axis when in contact with bottom of screen
        /// </summary>
        [TestMethod]
        public void ContactWithBottomOfScreen()
        {
            #region ARRANGE

            #region PADDLE

            // DECLARE & INSTANTIATE an IEntity using CreatePaddle(), name it '_paddle':
            IEntity _paddle = CreatePaddle();

            // DECLARE & INITIALISE an IPosition, name it '_paddleTfComp', give value of _paddle's TransformComponent:
            IPosition _paddleTfComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an IRotation, name it '_paddleRotComp', give value of _paddleTfComp:
            IRotation _paddleRotComp = _paddleTfComp as IRotation;

            // DECLARE & INITIALISE an ITexture, name it '_paddleTexComp', give value of _paddle's TextureComponent:
            ITexture _paddleTexComp = (_paddle as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // SET Origin Property of _paddleRotComp to middle of _paddleTexComp.TexSize:
            _paddleRotComp.Origin = new Vector2(_paddleTexComp.TexSize.X / 2, _paddleTexComp.TexSize.Y / 2);

            // SPAWN _paddle below the bottom left of screen, Y axis value of _maxXYBounds.Y - _paddleRotComp.Origin.Y + 1, so it is already off screen before running code:
            _paddleTfComp.Position = new Vector2(_minXYBounds.X + _paddleRotComp.Origin.X, _maxXYBounds.Y - _paddleRotComp.Origin.Y + 1);

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

            // CALL _RespondToBound on _mmBoundResponder, passing _paddle as a parameter:
            _mmBoundResponder.RespondToBound(_paddle);

            #endregion


            #region ASSERT

            // ASSERT that Paddle has reverted Y axis Position back to bottom of screen:
            Assert.AreEqual(_maxXYBounds.Y, _paddleTfComp.Position.Y + _paddleRotComp.Origin.Y);

            #endregion
        }

        #endregion
    }
}
