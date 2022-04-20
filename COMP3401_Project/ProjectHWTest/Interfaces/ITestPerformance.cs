using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace COMP3401ECS.ProjectHWTest.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to test the performance of a device's hardware
    /// Author: William Smith
    /// Date: 04/04/22
    /// </summary>
    public interface ITestPerformance
    {
        #region METHODS

        /// <summary>
        /// Method which initialises a caller with a PerformanceCounter instance
        /// </summary>
        /// <param name="pHWName"> Name of Hardware Device </param>
        /// <param name="pHWStats"> PerformanceCounter instance </param>
        void Initialise(string pHWName, PerformanceCounter pHWStats);

        /// <summary>
        /// Method which initialises a caller with a string and an IList<float> instance
        /// </summary>
        /// <param name="pTestName"> Name of Test </param>
        /// <param name="pFloatList"> List of float values </param>
        void Initialise(string pTestName, IList<float> pFloatList);

        /// <summary>
        /// Method which initialises a caller with an IList<long> instance
        /// </summary>
        /// <param name="pLongList"> List of long values </param>
        void Initialise(IList<long> pLongList);

        /// <summary>
        /// Tests any specified resources in quick succession
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// <param name="pTime"> Time taken for quick test </param>
        /// <param name="pTestFinish"> Used for signalling that test results can be exported </param>
        void QuickTimedTest(string pTestName, string pTestType, long pTime, bool pTestFinish);

        /// <summary>
        /// Tests any specified resources in a long timeframe
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        void LongTimedTest(GameTime pGameTime);

        /// <summary>
        /// Tests FPS count in an application
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        void TestFPS(GameTime pGameTime);

        /// <summary>
        /// Used to time tasks which are completed over a long time e.g. CPU/RAM/FPS
        /// </summary>
        void UpdateLongTimedTest();

        /// <summary>
        /// Finalises any testing and closes application
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// </summary>
        void FinishTesting(string pTestType);

        #endregion
    }
}
