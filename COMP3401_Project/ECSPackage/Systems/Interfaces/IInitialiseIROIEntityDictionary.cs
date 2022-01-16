using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Entities.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with an IReadOnlyDictionary
    /// Author: William Smith
    /// Date: 01/11/21
    /// </summary>
    public interface IInitialiseIROIEntityDictionary
    {
        #region METHODS

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict);

        #endregion
    }
}
