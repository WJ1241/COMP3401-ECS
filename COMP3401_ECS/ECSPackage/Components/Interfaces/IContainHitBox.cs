
using Microsoft.Xna.Framework;

namespace COMP3401ECS_Engine.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have a Rectangle HitBox around their image/texture/sprite
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface IContainHitBox
    {
        #region PROPERTIES

        /// <summary>
        /// Used to Return a rectangle object to caller of property
        /// </summary>
        Rectangle HitBox { get; set; }

        #endregion
    }
}
