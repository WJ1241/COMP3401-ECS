using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with an IInputResponder object
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IInitialiseIInputResponder
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with an IInputResponder object
        /// </summary>
        /// <param name="pInputResponder"> IInputResponder object </param>
        void Initialise(IInputResponder pInputResponder);

        #endregion
    }
}
