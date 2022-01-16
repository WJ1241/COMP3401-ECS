using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a reference to an ISpawnEntity object
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface IInitialiseISpawnEntity
    {
        #region METHODS

        /// <summary>
        /// Method which initialises an object with an ISpawnEntity object
        /// </summary>
        /// <param name="pSpawnEntityObj"> ISpawnEntity object </param>
        void Initialise(ISpawnEntity pSpawnEntityObj);

        #endregion
    }
}
