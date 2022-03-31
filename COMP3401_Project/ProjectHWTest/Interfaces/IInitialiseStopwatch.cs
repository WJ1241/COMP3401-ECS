using System.Diagnostics;

namespace COMP3401_Project.ProjectHWTest.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a Stopwatch instance
    /// Author: William Smith
    /// Date: 31/03/22
    /// </summary>
    public interface IInitialiseStopwatch
    {
        #region METHODS

        /// <summary>
        /// Method which initialises caller with a Stopwatch instance
        /// </summary>
        /// <param name="pTimer"> Stopwatch instance </param>
        void Initialise(Stopwatch pTimer);

        #endregion
    }
}
