using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;

namespace COMP3401ECS_Engine.Components
{
    /// <summary>
    /// Class used for storing data relative to an entity's movement status on screen
    /// Author: William Smith
    /// Date: 02/11/21
    /// </summary>
    public class VelocityComponent : IComponent, IVelocity
    {
        #region FIELD VARIABLES

        // DECLARE an float, name it '_speed':
        private float _speed;

        // DECLARE a Vector2, name it '_direction':
        private Vector2 _direction;

        // DECLARE a Vector2, name it '_velocity':
        private Vector2 _velocity;

        #endregion


        #region IMPLEMENTATION OF IVELOCITY

        /// <summary>
        /// Property which gives caller read and write access to speed value
        /// </summary>
        public float Speed
        {
            get
            {
                // RETURN value of _speed:
                return _speed;
            }
            set
            {
                // SET value of _speed to incoming value:
                _speed = value;
            }
        }

        /// <summary>
        /// Property which gives caller read and write access to direction value
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                // RETURN value of _direction:
                return _direction;
            }
            set
            {
                // SET value of _direction to incoming value:
                _direction = value;
            }
        }

        /// <summary>
        /// Property which gives caller read and write access to velocity value
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                // RETURN value of _velocity:
                return _velocity;
            }
            set
            {
                // SET value of of _velocity to incoming value:
                _velocity = value;
            } 
        }

        #endregion
    }
}
