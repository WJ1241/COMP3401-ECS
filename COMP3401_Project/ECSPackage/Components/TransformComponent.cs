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
    /// Class used for storing data relative to an entity's position on screen
    /// Author: William Smith
    /// Date: 24/10/21
    /// </summary>
    public class TransformComponent : IComponent, IPosition
    {
        #region FIELD VARIABLES

        // DECLARE a Vector2, name it '_position':
        private Vector2 _position;

        #endregion


        #region IMPLEMENTATION OF IPOSITION

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
    }
}
