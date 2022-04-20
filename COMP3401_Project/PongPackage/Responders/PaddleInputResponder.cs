using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using COMP3401ECS.PongPackage.Responders.Interfaces;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Systems.Interfaces;

namespace COMP3401ECS.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to user input to control Paddle on screen
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public class PaddleInputResponder : IInputResponder, ITestKBInput
    {
        #region FIELD VARIABLES

        // DECLARE a string, name it '_currentKey':
        // ALLOWS EASIER TESTING, CANNOT TEST INPUT DUE TO INABILITY TO HOLD KEY
        private string _currentKey;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PaddleInputResponder
        /// </summary>
        public PaddleInputResponder()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IINPUTRESPONDER

        /// <summary>
        /// Method which is given an entity that needs updating from player input
        /// </summary>
        /// <param name="pEntity"> Entity which needs update from player input </param>
        public void RespondToInput(IEntity pEntity)
        {
            // DECLARE & INITIALISE a KeyboardState, name it '_kBState', give value of current Keyboard state:
            KeyboardState _kBState = Keyboard.GetState();

            // DECLARE & INITIALISE an IRotation, name it 'tempTfComp', give instance of pEntity's TransformComponent:
            IRotation tempTfComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IRotation;

            // DECLARE & INITIALISE an ITexture, name it 'tempTexComp', give instance of pEntity's TextureComponent:
            ITexture tempTexComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

            // DECLARE & INITIALISE a PlayerIndex, name it '_currentPlayerNum', give value of current entity's PlayerID Property:
            PlayerIndex _currentPlayerNum = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer).PlayerID;

            // INSTANTIATE new Vector2, set as 0 to stop movement:
            ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0);

            // IF Player 1:
            if (_currentPlayerNum == PlayerIndex.One)
            {
                // IF Player 2 is Pressing 'W' key:
                if (_kBState.IsKeyDown(Keys.W))
                {
                    // SET _currentKey to "Up":
                    _currentKey = "W";
                }
                // IF Player 2 is Pressing 'S' key:
                if (_kBState.IsKeyDown(Keys.S))
                {
                    // SET _currentKey to "Down":
                    _currentKey = "S";
                }


                // IF Player 1 HAS been assigned a texture named "Paddle1_DFLT":
                if (tempTexComp.ReturnTextureDict().ContainsKey("Paddle1_DFLT"))
                {
                    // SET Texture of pEntity's TextureComponent to "Paddle1_DFLT, works for idling":
                    tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle1_DFLT"];
                }

                // IF W OR S Key is down:
                if (_currentKey == "W" || _currentKey == "S")
                {
                    // IF Player 1 HAS been assigned a texture named "Paddle1_INPT":
                    if (tempTexComp.ReturnTextureDict().ContainsKey("Paddle1_INPT"))
                    {
                        // SET Texture of pEntity's TextureComponent to "Paddle1_INPT":
                        tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle1_INPT"];
                    }

                    // IF W Key is down:
                    if (_currentKey == "W")
                    {
                        // SET value of pEntity's TransformComponent's RotationAngle Property to '0', measured in radians:
                        tempTfComp.RotationAngle = 0;

                        // SET direction of pEntity to '-1' to move upwards:
                        ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                    }

                    // ELSE IF S Key is down:
                    else if (_currentKey == "S")
                    {
                        // SET value of pEntity's TransformComponent's RotationAngle Property to PI, measured in radians:
                        tempTfComp.RotationAngle = (float)Math.PI;

                        // SET direction of pEntity to '1' to move downwards:
                        ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                    }
                }
            }

            // ELSE IF Player 2:
            else if (_currentPlayerNum == PlayerIndex.Two)
            {
                // IF Player 2 is Pressing Up key:
                if (_kBState.IsKeyDown(Keys.Up))
                {
                    // SET _currentKey to "Up":
                    _currentKey = "Up";
                }
                // IF Player 2 is Pressing Down key:
                if (_kBState.IsKeyDown(Keys.Down))
                {
                    // SET _currentKey to "Down":
                    _currentKey = "Down";
                }

                // IF Player 2 HAS been assigned a texture named "Paddle2_DFLT":
                if (tempTexComp.ReturnTextureDict().ContainsKey("Paddle2_DFLT"))
                {
                    // SET Texture of pEntity's TextureComponent to "Paddle2_DFLT, works for idling":
                    tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle2_DFLT"];
                }

                // IF Up OR Down Key is down:
                if (_currentKey == "Up" || _currentKey == "Down")
                {
                    // IF Player 2 HAS been assigned a texture named "Paddle2_INPT":
                    if (tempTexComp.ReturnTextureDict().ContainsKey("Paddle2_INPT"))
                    {
                        // SET Texture of pEntity's TextureComponent to "Paddle2_INPT":
                        tempTexComp.Texture = tempTexComp.ReturnTextureDict()["Paddle2_INPT"];
                    }

                    // IF Up Arrow Key is down:
                    if (_currentKey == "Up")
                    {
                        // SET value of pEntity's TransformComponent's RotationAngle Property to '0', measured in radians:
                        tempTfComp.RotationAngle = 0;

                        // SET direction of pEntity to '-1' to move upwards:
                        ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                    }

                    // ELSE IF Down Arrow Key is down:
                    else if (_currentKey == "Down")
                    {
                        // SET value of pEntity's TransformComponent's RotationAngle Property to PI, measured in radians (180 degrees n ):
                        tempTfComp.RotationAngle = (float)Math.PI;

                        // SET direction of pEntity to '1' to move downwards:
                        ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                    }
                }
            }

            // DECLARE & INITIALISE an IVelocity, give value of pEntity's Velocity Component, name it'tempVelComp':
            IVelocity tempVelComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // ASSIGN value of tempVelComp.Speed multiplied by tempVelComp.Direction to tempVelComp.Velocity:
            tempVelComp.Velocity = tempVelComp.Speed * tempVelComp.Direction;

            // RESET _currentKey to a blank string:
            _currentKey = "";
        }

        #endregion


        #region IMPLEMENTATION OF ITESTKBINPUT

        /// <summary>
        /// Property which gives caller write access to what key is pressed
        /// </summary>
        public string SetKeyPressed
        {
            set
            {
                // SET value of _currentKey to incoming value:
                _currentKey = value;
            }
        }

        #endregion
    }
}
