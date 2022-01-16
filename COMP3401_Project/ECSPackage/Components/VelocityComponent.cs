using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;

namespace COMP3401_Project.ECSPackage.Components
{
    /// <summary>
    /// Class used for storing data relative to an entity's movement status on screen
    /// Author: William Smith
    /// Date: 02/11/21
    /// </summary>
    public class VelocityComponent : IComponent, IVelocity
    {
        #region FIELD VARIABLES

        // DECLARE an int, name it '_speed':
        private int _speed;

        // DECLARE a Vector2, name it '_direction':
        private Vector2 _direction;

        #endregion


        #region IMPLEMENTATION OF IVELOCITY

        /// <summary>
        /// Property which gives caller read and write access to speed value
        /// </summary>
        public int Speed
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

        #endregion
    }
}
