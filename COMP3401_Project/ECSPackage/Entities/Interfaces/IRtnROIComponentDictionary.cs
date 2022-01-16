using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Components.Interfaces;

namespace COMP3401_Project.ECSPackage.Entities.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to return a read only IReadOnlyDictionary<string, IComponent>
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface IRtnROIComponentDictionary
    {
        #region METHODS

        /// <summary>
        /// Allows callers to get reference to an instance of IReadOnlyDictionary<string, IComponent>
        /// </summary>
        /// <returns> Reference to an IReadOnlyDictionary<string, IComponent> </returns>
        IReadOnlyDictionary<string, IComponent> ReturnComponentDictionary();

        #endregion
    }
}
