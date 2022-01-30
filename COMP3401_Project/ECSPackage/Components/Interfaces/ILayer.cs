using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Layer 1: GUI
        /// Layer 2: Background
        /// Layer 3: Non-Player Controlled Static Entity
        /// Layer 4: Non-Player Controlled Moveable Entity
        /// Layer 5: Player Controlled Moveable Entity
        /// </summary>
        int Layer { get; set; }

        #endregion
    }
}
