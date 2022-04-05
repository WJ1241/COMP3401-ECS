using System;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Delegates;
using COMP3401_Project.ECSPackage.Delegates.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to movement of any Pong entity
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public class PongMovementBoundResponder : IInitialiseCreateDel, IInitialiseDeleteDel, IMovementBoundResponder
    {
        #region FIELD VARIABLES

        // DECLARE a CreateDelegate, name it '_create':
        private CreateDelegate _create;

        // DECLARE a DeleteDelegate, name it '_terminate':
        private DeleteDelegate _terminate;

        // DECLARE a Point, name it '_minXYBounds':
        private Point _minXYBounds;

        // DECLARE a Point, name it '_maxXYBounds':
        private Point _maxXYBounds;

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
            // DECLARE & INITIALISE an IPosition, name it 'tempTfComp', give instance of pEntity's TransformComponent:
            IPosition tempTfComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition;

            // DECLARE & INITIALISE an IPosition, name it 'tempVelComp', give instance of pEntity's VelocityComponent:
            IVelocity tempVelComp = (pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity;

            // DECLARE & INITIALISE a Point, name it 'tempTexSize', give value of pEntity's TexSize Property:
            Point tempTexSize = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).TexSize;

            // DECLARE & INITIALISE an ILayer, name it 'tempLayer', give value of pEntity's Layer Property:
            int tempLayer = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer;

            #region LAYER 3 (BALL)

            // IF pEntity is on Layer 3 (Non-Player Controlled Moveable Entity):
            if (tempLayer == 3)
            {
                // DECLARE & INITIALISE a Vector2, name it 'tempVel', used for top and bottom bounds and reversing Y velocity:
                Vector2 tempVel = tempVelComp.Velocity;

                #region Y AXIS

                // IF pEntity is at the top or bottom of the screen:
                if (tempTfComp.Position.Y <= _minXYBounds.Y || tempTfComp.Position.Y + tempTexSize.Y >= _maxXYBounds.Y)
                {
                    // MULTIPLY tempVel.Y by '-1':
                    tempVel.Y *= -1;

                    // RE-ASSIGN value of tempVel to pEntity's Velocity Component:
                    tempVelComp.Velocity = tempVel;
                }

                #endregion


                #region X AXIS

                // IF pEntity has exited left side of the screen:
                if (tempTfComp.Position.X <= _minXYBounds.X || tempTfComp.Position.X >= _maxXYBounds.X - tempTexSize.X)
                {
                    // MULTIPLY tempVel.X by '-1':
                    tempVel.X *= -1;

                    // RE-ASSIGN value of tempVel to pEntity's Velocity Component:
                    tempVelComp.Velocity = tempVel;


                    if (tempTfComp.Position.X <= _minXYBounds.X)
                    {
                        // SET
                        tempTfComp.Position = new Vector2(_minXYBounds.X, tempTfComp.Position.Y);
                    }


                    if (tempTfComp.Position.X >= _maxXYBounds.X - tempTexSize.X)
                    {
                        // SET
                        tempTfComp.Position = new Vector2(_maxXYBounds.X - tempTexSize.X, tempTfComp.Position.Y);
                    }



                    /*

                    // CALL _terminate, passing pInt as a parameter:
                    _terminate(pEntity.UID);

                    // CALL _create():
                    _create();

                    */
                }

                #endregion
            }

            #endregion


            #region LAYER 4 (PADDLE)

            // IF pEntity is on Layer 4 (Player Controlled Moveable Entity):
            if (tempLayer == 4)
            {
                // DECLARE & INITIALISE a Vector2, name it 'tempPos', used for top and bottom bounds and stopping movement:
                Vector2 tempPos = tempTfComp.Position;

                // DECLARE & INITIALISE a Vector2, name it 'tempOrigin', used for keeping entity on screen:
                Vector2 tempOrigin = (tempTfComp as IRotation).Origin;

                #region Y AXIS

                // IF pEntity is at the top of the screen:
                if (tempPos.Y - tempOrigin.Y <= _minXYBounds.Y)
                {
                    // SET tempPos.Y to the top of the Y axis:
                    tempPos.Y = _minXYBounds.Y + tempOrigin.Y;
                }

                // ELSE IF pEntity is at the bottom of the screen:
                else if (tempPos.Y + tempOrigin.Y >= _maxXYBounds.Y)
                {
                    // SET tempPos.Y to the bottom of the Y axis:
                    tempPos.Y = _maxXYBounds.Y - tempOrigin.Y;
                }

                // RE-ASSIGN value of tempPos to pEntity's TransformComponent:
                tempTfComp.Position = tempPos;

                #endregion
            }

            #endregion
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