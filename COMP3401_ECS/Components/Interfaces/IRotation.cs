using Microsoft.Xna.Framework;

namespace COMP3401ECS_Engine.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have rotation angle values
    /// Author: William Smith
    /// Date: 26/01/22
    /// </summary>
    public interface IRotation
    {
        #region PROPERTIES

        /// <summary>
        /// Property which allows read and write access to a Vector2 containing drawing position
        /// </summary>
        Vector2 Origin { get; set; }

        /// <summary>
        /// Property which allows read and write access to a rotation angle value
        /// </summary>
        float RotationAngle { get; set; }

        #endregion
    }
}
