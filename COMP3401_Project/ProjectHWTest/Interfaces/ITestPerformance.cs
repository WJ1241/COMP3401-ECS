using System.Diagnostics;

namespace COMP3401_Project.ProjectHWTest.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to test the performance of a device's hardware
    /// Author: William Smith
    /// Date: 31/03/22
    /// </summary>
    public interface ITestPerformance
    {
        #region METHODS

        /// <summary>
        /// Method which initialises a caller with a PerformanceCounter instance
        /// </summary>
        /// <param name="pHWStats"> PerformanceCounter instance </param>
        void Initialise(PerformanceCounter pHWStats);

        /// <summary>
        /// Tests how long it takes to create entities
        /// </summary>
        void TestCreation();

        /// <summary>
        /// Tests how long it takes to terminate entities
        /// </summary>
        void TestTermination();

        /// <summary>
        /// Tests CPU Usage in an application
        /// </summary>
        void TestCPUUsage();

        /// <summary>
        /// Tests RAM Usage in an application
        /// </summary>
        void TestRAMUsage();

        /// <summary>
        /// Tests GPU Usage in an application
        /// </summary>
        void TestGPUUsage();

        /// <summary>
        /// Tests FPS count in an application
        /// </summary>
        void TestFPS();

        /// <summary>
        /// Finalises any testing and closes application
        /// </summary>
        void FinishTesting();

        #endregion
    }
}
