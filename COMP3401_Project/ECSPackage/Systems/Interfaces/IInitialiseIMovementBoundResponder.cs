using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with an IMovementBoundResponder object
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IInitialiseIMovementBoundResponder
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with an IMovementBoundResponder object
        /// </summary>
        /// <param name="pMmBoundResponder"> IMovementBoundResponder object </param>
        void Initialise(IMovementBoundResponder pMmBoundResponder);

        #endregion
    }
}
