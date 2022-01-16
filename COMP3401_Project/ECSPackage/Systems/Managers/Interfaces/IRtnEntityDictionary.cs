using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Entities.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have return an IDictionary<int, IEntity> reference
    /// Author: William Smith
    /// Date: 21/12/21
    /// </summary>
    public interface IRtnEntityDictionary
    {
        #region METHODS

        /// <summary>
        /// Returns a reference to an Entity Dictionary
        /// </summary>
        IDictionary<int, IEntity> ReturnEntityDict();

        #endregion
    }
}
