﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to user input to control Paddle on screen
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class PaddleInputResponder : IInputResponder
    {
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
            // IF pEntity DOES HAVE a current instance:
            if (pEntity != null)
            {
                // DECLARE & INITIALISE a KeyboardState, name it '_kBState', give value of current Keyboard state:
                KeyboardState _kBState = Keyboard.GetState();

                // DECLARE & INITIALISE an IRotation, name it '_tempTfComp', give instance of pEntity's TransformComponent:
                IRotation _tempTfComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IRotation;

                // DECLARE & INITIALISE an ITexture, name it '_tempTexComp', give instance of pEntity's TextureComponent:
                ITexture _tempTexComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture;

                // DECLARE & INITIALISE a PlayerIndex, name it '_currentPlayerNum', give value of current entity's PlayerID Property:
                PlayerIndex _currentPlayerNum = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer).PlayerID;

                // INSTANTIATE new Vector2, set as 0 to stop movement:
                ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0);

                // IF Player 1:
                if (_currentPlayerNum == PlayerIndex.One)
                {
                    // SET Texture of pEntity's TextureComponent to "Paddle1_DFLT, works for idling":
                    _tempTexComp.Texture = _tempTexComp.ReturnTextureDict()["Paddle1_DFLT"];

                    // IF W OR S Key is down:
                    if (_kBState.IsKeyDown(Keys.W) || _kBState.IsKeyDown(Keys.S))
                    {
                        // SET Texture of pEntity's TextureComponent to "Paddle1_INPT":
                        _tempTexComp.Texture = _tempTexComp.ReturnTextureDict()["Paddle1_INPT"];

                        // IF W Key is down:
                        if (_kBState.IsKeyDown(Keys.W))
                        {
                            // SET value of pEntity's TransformComponent's RotationAngle Property to '0', measured in radians:
                            _tempTfComp.RotationAngle = 0;

                            // SET direction of pEntity to '-1' to move upwards:
                             ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                        }

                        // ELSE IF S Key is down:
                        else if (_kBState.IsKeyDown(Keys.S))
                        {
                            // SET value of pEntity's TransformComponent's RotationAngle Property to PI, measured in radians:
                            _tempTfComp.RotationAngle = (float)Math.PI;

                            // SET direction of pEntity to '1' to move downwards:
                            ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                        }
                    }
                }

                // ELSE IF Player 2:
                else if (_currentPlayerNum == PlayerIndex.Two)
                {
                    // SET Texture of pEntity's TextureComponent to "Paddle2_DFLT, works for idling":
                    _tempTexComp.Texture = _tempTexComp.ReturnTextureDict()["Paddle2_DFLT"];

                    // IF Up OR Down Key is down:
                    if (_kBState.IsKeyDown(Keys.Up) || _kBState.IsKeyDown(Keys.Down))
                    {
                        // SET Texture of pEntity's TextureComponent to "Paddle2_INPT":
                        _tempTexComp.Texture = _tempTexComp.ReturnTextureDict()["Paddle2_INPT"];

                        // IF Up Arrow Key is down:
                        if (_kBState.IsKeyDown(Keys.Up))
                        {
                            // SET value of pEntity's TransformComponent's RotationAngle Property to '0', measured in radians:
                            _tempTfComp.RotationAngle = 0;

                            // SET direction of pEntity to '-1' to move upwards:
                            ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                        }

                        // ELSE IF Down Arrow Key is down:
                        else if (_kBState.IsKeyDown(Keys.Down))
                        {
                            // SET value of pEntity's TransformComponent's RotationAngle Property to PI, measured in radians:
                            _tempTfComp.RotationAngle = (float)Math.PI;

                            // SET direction of pEntity to '1' to move downwards:
                            ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                        }
                    }
                }

                // DECLARE & INITIALISE an IVelocity, give value of pEntity's Velocity Component, name it'_tempVelComp':
                IVelocity _tempVelComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

                // ASSIGN value of _tempVelComp.Speed multiplied by _tempVelComp.Direction to _tempVelComp.Velocity:
                _tempVelComp.Velocity = _tempVelComp.Speed * _tempVelComp.Direction;
            }
            // IF pEntity DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pEntity requiring user input does not have an active instance!");
            }
        }

        #endregion
    }
}
