using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Delegates;
using COMP3401_Project.ECSPackage.Delegates.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to movement of any Pong entity
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class PongMovementBoundResponder : IInitialiseCreateDel, IInitialiseDeleteDel, IMovementBoundResponder
    {
        #region FIELD VARIABLES

        // DECLARE a Point, name it '_minXYBounds':
        private Point _minXYBounds;

        // DECLARE a Point, name it '_maxXYBounds':
        private Point _maxXYBounds;

        // DECLARE a CreateDelegate, name it '_create':
        private CreateDelegate _create;

        // DECLARE a DeleteDelegate, name it '_terminate':
        private DeleteDelegate _terminate;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PongMovementBoundResponder
        /// </summary>
        public PongMovementBoundResponder()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISECREATEDEL

        /// <summary>
        /// Initialises an object with a 'CreateDelegate' method
        /// </summary>
        /// <param name="pCreateDelegate"> Create Method </param>
        public void Initialise(CreateDelegate pCreateDelegate)
        {
            // INITIALISE _create with value of pCreateDelegate:
            _create = pCreateDelegate;
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEDELETEDEL

        /// <summary>
        /// Initialises an object with a 'DeleteDelegate' method
        /// </summary>
        /// <param name="pDeleteDelegate"> Delete Method </param>
        public void Initialise(DeleteDelegate pDeleteDelegate)
        {
            // INITIALISE _terminate with value of pDeleteDelegate:
            _terminate = pDeleteDelegate;
        }

        #endregion


        #region IMPLEMENTATION OF IMOVEMENTBOUNDRESPONDER

        /// <summary>
        /// Method which is given an entity that needs action from reaching game bounds
        /// </summary>
        public void RespondToBound(IEntity pEntity)
        {
            // IF pEntity DOES HAVE an active instance:
            if (pEntity != null)
            {
                // DECLARE & INITIALISE an IPosition, name it '_tempTfComp', give instance of pEntity's TransformComponent:
                IPosition _tempTfComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

                // DECLARE & INITIALISE an IPosition, name it '_tempVelComp', give instance of pEntity's VelocityComponent:
                IVelocity _tempVelComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

                // DECLARE & INITIALISE a Texture2D, name it '_tempTexture', give value of pEntity's Texture Property:
                Texture2D _tempTexture = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture;

                // DECLARE & INITIALISE an ILayer, name it '_tempLayer', give value of pEntity's Layer Property:
                int _tempLayer = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer;

                #region LAYER 4 (BALL)

                // IF pEntity is on Layer 4 (Non-Player Controlled Moveable Entity):
                if (_tempLayer == 4)
                {
                    // DECLARE & INITIALISE a Vector2, name it '_tempVel', used for top and bottom bounds and reversing Y velocity:
                    Vector2 _tempVel = _tempVelComp.Velocity;

                    #region Y AXIS

                    // IF pEntity is at the top or bottom of the screen:
                    if (_tempTfComp.Position.Y <= _minXYBounds.Y || _tempTfComp.Position.Y + _tempTexture.Height >= _maxXYBounds.Y)
                    {
                        // MULTIPLY _tempVel.Y by '-1':
                        _tempVel.Y *= -1;

                        // RE-ASSIGN value of _tempVel to pEntity's Velocity Component:
                        _tempVelComp.Velocity = _tempVel;
                    }

                    #endregion


                    #region X AXIS

                    // IF pEntity has exited left side of the screen:
                    if (_tempTfComp.Position.X <= _minXYBounds.X || _tempTfComp.Position.X >= _maxXYBounds.X - _tempTexture.Width)
                    {
                        // TRY checking if pEntity can be terminated:
                        try
                        {
                            // CALL _terminate, passing pInt as a parameter:
                            _terminate(pEntity.UID);
                        }
                        // CATCH InvalidValueException from termination:
                        catch (InvalidValueException e)
                        {
                            // WRITE exception message to console:
                            Console.WriteLine(e.Message);
                        }

                        // CALL _create():
                        _create();
                    }

                    #endregion
                }

                #endregion


                #region LAYER 5 (PADDLE)

                // IF pEntity is on Layer 5 (Player Controlled Moveable Entity):
                if (_tempLayer == 5)
                {
                    // DECLARE & INITIALISE a Vector2, name it '_tempPos', used for top and bottom bounds and stopping movement:
                    Vector2 _tempPos = _tempTfComp.Position;

                    // DECLARE & INITIALISE a Vector2, name it '_tempOrigin', used for keeping entity on screen:
                    Vector2 _tempOrigin = (_tempTfComp as IRotation).Origin;

                    #region Y AXIS

                    // IF pEntity is at the top of the screen:
                    if (_tempPos.Y - _tempOrigin.Y <= _minXYBounds.Y)
                    {
                        // SET _tempPos.Y to the top of the Y axis:
                        _tempPos.Y = _minXYBounds.Y + _tempOrigin.Y;
                    }

                    // ELSE IF pEntity is at the bottom of the screen:
                    else if (_tempPos.Y + _tempOrigin.Y >= _maxXYBounds.Y)
                    {
                        // SET _tempPos.Y to the bottom of the Y axis:
                        _tempPos.Y = _maxXYBounds.Y - _tempOrigin.Y;
                    }

                    // RE-ASSIGN value of _tempPos to pEntity's TransformComponent:
                    _tempTfComp.Position = _tempPos;

                    #endregion
                }

                #endregion
            }
            // IF pEntity DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException:
                throw new NullInstanceException("ERROR: pEntity does not have an active instance!");
            }
        }

        /// <summary>
        /// Property which allows only write access to a Point specifying minimum positional game bounds
        /// </summary>
        public Point MinXYBound
        {
            set
            {
                // SET value of _minXYBounds to incoming value:
                _minXYBounds = value;
            }
        }

        /// <summary>
        /// Property which allows only write access to a Point specifying maximum positional game bounds
        /// </summary>
        public Point MaxXYBound
        { 
            set
            {
                // SET value of _maxXYBounds to incoming value:
                _maxXYBounds = value;
            }
        }

        #endregion
    }
}