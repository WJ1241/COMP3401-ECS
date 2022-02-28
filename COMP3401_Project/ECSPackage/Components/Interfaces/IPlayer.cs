using Microsoft.Xna.Framework;

namespace COMP3401_Project.ECSPackage.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be controlled by a User/Player
    /// Author: William Smith
    /// Date: 14/01/22
    /// </summary>
    public interface IPlayer
    {
        #region PROPERTIES

        /// <summary>
        /// Property which can get and set value of a Player's ID Number
        /// </summary>
        PlayerIndex PlayerID { get; set; }

        #endregion
    }
}
