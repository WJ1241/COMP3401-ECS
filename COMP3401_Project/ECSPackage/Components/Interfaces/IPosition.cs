using Microsoft.Xna.Framework;

namespace COMP3401_Project.ECSPackage.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have a Vector2 object for positional values
    /// Author: William Smith
    /// Date: 24/10/21
    /// </summary>
    public interface IPosition
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller read and write access to positional values
        /// </summary>
        Vector2 Position { get; set; }

        #endregion
    }
}
