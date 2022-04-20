using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;

namespace COMP3401ECS_Engine.Components
{
    /// <summary>
    /// Class which contains a Rectangle object to act as a HitBox
    /// Author: William Smith
    /// Date: 09/01/22
    /// </summary>
    public class HitBoxComponent : IComponent, IContainHitBox
    {
        #region FIELD VARIABLES

        // DECLARE a Rectangle, name it '_hitBox':
        private Rectangle _hitBox;

        #endregion


        #region IMPLEMENTATION OF ICONTAINHITBOX

        /// <summary>
        /// Used to Return a rectangle object to caller of property
        /// </summary>
        public Rectangle HitBox
        {
            get
            {
                // RETURN value of _hitBox:
                return _hitBox;
            }
            set
            {
                // SET value of _hitBox to incoming value:
                _hitBox = value;
            }
        }

        #endregion
    }
}
