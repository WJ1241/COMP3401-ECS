using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with an ICollisionResponder object
    /// Author: William Smith
    /// Date: 14/01/22
    /// </summary>
    public interface IInitialiseICollisionResponder
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with an ICollisionResponder object
        /// </summary>
        /// <param name="pCollisionResponder"> ICollisionResponder object </param>
        void Initialise(ICollisionResponder pCollisionResponder);

        #endregion
    }
}
