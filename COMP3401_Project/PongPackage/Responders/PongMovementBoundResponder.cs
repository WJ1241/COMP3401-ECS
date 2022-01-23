using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    /// Date: 19/01/22
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
            // DECLARE & INITIALISE a Texture2D, name it '_tempTexture', give value of pEntity's Texture Property:
            Texture2D _tempTexture = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TextureComponent"] as ITexture).Texture;

            #region LAYER 3

            // IF pEntity is on Layer 3 (Non-Player Controlled Moveable Entity):
            if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 3)
            {
                // DECLARE & INITIALISE a Vector2, name it '_tempVel', used for top and bottom bounds and reversing Y velocity:
                Vector2 _tempVel = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity;

                #region Y AXIS

                // IF pEntity is at the top or bottom of the screen:
                if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.Y <= _minXYBounds.Y
                 || ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.Y + _tempTexture.Height >= _maxXYBounds.Y)
                {
                    // MULTIPLY _tempVel.Y by '-1':
                    _tempVel.Y *= -1;

                    // RE-ASSIGN value of _tempVel to pEntity's Velocity Component:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity = _tempVel;
                }

                #endregion


                #region X AXIS

                // IF pEntity has exited left side of the screen:
                if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.X <= _minXYBounds.X
                 || ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.X >= _maxXYBounds.X - _tempTexture.Width)
                {
                    // CALL _terminate, passing pInt as a parameter:
                    _terminate(pEntity.UID);

                    // CALL _create():
                    _create();
                }

                #endregion
            }

            #endregion


            #region LAYER 4

            // IF pEntity is on Layer 4 (Player Controlled Moveable Entity):
            if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 4)
            {
                // DECLARE & INITIALISE a Vector2, name it '_tempPos', used for top and bottom bounds and stopping movement:
                Vector2 _tempPos = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position;

                #region Y AXIS

                // IF pEntity is at the top of the screen:
                if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.Y <= _minXYBounds.Y)
                {
                    // SET pEntity's Position property to current X Position, and Y Position at the top of the Y axis:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position = new Vector2(_tempPos.X, _minXYBounds.Y);
                }

                // ELSE IF pEntity is at the bottom of the screen:
                else if (((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position.Y +_tempTexture.Height >= _maxXYBounds.Y)
                {
                    // SET pEntity's Position property to current X Position, and Y Position at the bottom of the Y axis:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["TransformComponent"] as IPosition).Position = new Vector2(_tempPos.X, _maxXYBounds.Y - _tempTexture.Height);
                }

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
