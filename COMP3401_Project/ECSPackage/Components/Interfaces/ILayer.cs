

namespace COMP3401_Project.ECSPackage.Components.Interfaces
{
    /// <summary>
    /// Interface which allows entities to be placed on different 'layers' to aid collision separation
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface ILayer
    {
        #region PROPERTIES

        /// <summary>
        /// Property which allows read and write access of entity layer
        /// Layer 1: Background
        /// Layer 2: Non-Player Controlled Static Entity
        /// Layer 3: Non-Player Controlled Moveable Entity
        /// Layer 4: Player Controlled Moveable Entity
        /// Layer 5: GUI
        /// </summary>
        int Layer { get; set; }

        #endregion
    }
}
