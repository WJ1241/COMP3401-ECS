using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a reference to an IUpdatable object
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface IInitialiseIUpdatable
    {
        #region METHODS

        /// <summary>
        /// Method which initialises an object with an IUpdatable object
        /// </summary>
        /// <param name="pUpdatable"> IUpdatable object </param>
        void Initialise(IUpdatable pUpdatable);

        #endregion
    }
}
