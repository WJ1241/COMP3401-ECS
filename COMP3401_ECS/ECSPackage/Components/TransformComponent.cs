using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;

namespace COMP3401ECS_Engine.Components
{
    /// <summary>
    /// Class used for storing data relative to an entity's position on screen
    /// Author: William Smith
    /// Date: 26/01/22
    /// </summary>
    public class TransformComponent : IComponent, IPosition, IRotation
    {
        #region FIELD VARIABLES

        // DECLARE a Vector2, name it '_position':
        private Vector2 _position;

        // DECLARE a Vector2, name it '_originPoint':
        private Vector2 _originPoint;

        // DECLARE a float, name it '_rotAngle', give default value of 0, can change if needed:
        private float _rotAngle = 0;

        #endregion


        #region IMPLEMENTATION OF IPOSITION

        /// <summary>
        /// Property which gives caller read and write access to positional values
        /// </summary>
        public Vector2 Position
        {
            get 
            {
                // RETURN value of _position:
                return _position;
            }
            set
            {
                // SET value of _position to incoming value:
                _position = value;
            }
        }

        #endregion


        #region IMPLEMENTATION OF IROTATION

        /// <summary>
        /// Property which allows read and write access to a Vector2 containing drawing position
        /// </summary>
        public Vector2 Origin
        {
            get
            {
                // RETURN value of _originPoint:
                return _originPoint;
            }
            set
            {
                // SET value of _originPoint to incoming value:
                _originPoint = value;
            }
        }

        /// <summary>
        /// Property which allows read and write access to a rotation angle value
        /// </summary>
        public float RotationAngle
        {
            get
            {
                // RETURN value of _rotAngle:
                return _rotAngle;
            }
            set
            {
                // SET value of _rotAngle to incoming value:
                _rotAngle = value;
            }
        }

        #endregion

    }
}
