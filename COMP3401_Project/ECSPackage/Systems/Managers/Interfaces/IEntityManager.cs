using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Entities.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations manage entities in game level
    /// Author: William Smith
    /// Date: 09/01/22
    /// </summary>
    public interface IEntityManager
    {
        #region METHODS

        /// <summary>
        /// Adds an Entity to a collection object
        /// </summary>
        /// <param name="pEntity"> Entity to be added to collection object </param>
        void AddEntity(IEntity pEntity);

        /// <summary>
        /// Terminates Entity using its ID value
        /// </summary>
        /// <param name="pID"> ID value of Entity in scene </param>
        void Terminate(int pID);

        #endregion
    }
}
