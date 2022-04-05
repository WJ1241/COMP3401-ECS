using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using ClosedXML.Excel;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Services.Interfaces;
using COMP3401_Project.ProjectHWTest.Interfaces;

namespace COMP3401_Project_ProjectHWTest
{
    /// <summary>
    /// Class which gets current usages from the user's system and sends results to an Excel Spreadsheet
    /// Author: William Smith
    /// Date: 04/04/22
    /// </summary>
    /// <REFERENCE> Cerutti, T. (2016) Exporting the values in List to excel. Available at: https://stackoverflow.com/questions/2206279/exporting-the-values-in-list-to-excel. (Accessed: 31 March 2022). </REFERENCE>
    /// <REFERENCE> 'shytikov' (2018) c# calculate CPU usage for a specific application. Available at: https://stackoverflow.com/questions/1277556/c-sharp-calculate-cpu-usage-for-a-specific-application. (Accessed: 4 April 2022). </REFERENCE>
    /// <REFERENCE> Whitaker, R.B. (No Date) Calculating The Frame Rate. Available at: http://rbwhitaker.wikidot.com/calculating-the-frame-rate. (Accessed: 31 March 2022). </REFERENCE>
    public class PerformanceMeasure : IExportExcelData, IInitialiseStopwatch, IService, ITestPerformance
    {
        #region J0B LIST

        /*
                    Thursday and Friday Jobs
            - Get RAM Usage Done
            - Get CPU Usage Done
            - Get FPS Counter Done

            Tests to take place

		            Timed Tests
            - Time taken to create set amount of entities
            - Time taken to delete set amount of entities

		            Resource Tests

	            50 entities
            - FPS Average over 5 minutes with 50 entities
            - CPU usage Average over 5 minutes with 50 entities
            - RAM usage Average over 5 minutes with 50 entities

	            500 entities
            - FPS Average over 5 minutes with 500 entities
            - CPU usage Average over 5 minutes with 500 entities
            - RAM usage Average over 5 minutes with 500 entities
         
        */

        #endregion


        #region FIELD VARIABLES

        // DECLARE an IDictionary<string, PerformanceCounter>, name it '_hwCounters':
        private IDictionary<string, PerformanceCounter> _hwCounters;

        // DECLARE an IDictionary<string, IList<float>>, name it '_floatListDict':
        private IDictionary<string, IList<float>> _floatListDict;

        // DECLARE an IList<long>, name it '_quickTimeList':
        private IList<long> _quickTimeList;

        // DECLARE an IXLWorkbook, name it '_excelWorkbook':
        private IXLWorkbook _excelWorkbook;

        // DECLARE a Stopwatch, name it '_timer':
        private Stopwatch _timer;

        // DECLARE a bool, name it '_testStarted':
        private bool _testStarted;

        // DECLARE a string, name it '_currentTest':
        private string _currentTest;

        // DECLARE a long, name it '_prevTimeInterval':
        private long _prevTimeInterval;

        // DECLARE a long, name it '_totalTime':
        private long _totalTime;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PerformanceMeasure
        /// </summary>
        public PerformanceMeasure()
        {
            // INSTANTIATE _hwCounters as a new Dictionary<string, PerformanceCounter>():
            _hwCounters = new Dictionary<string, PerformanceCounter>();

            // INSTANTIATE _floatListDict as a new Dictionary<string, IList<float>>()
            _floatListDict = new Dictionary<string, IList<float>>();

            // SET _testStarted to false:
            _testStarted = false;

            // INITIALISE _currentTest with a blank string to avoid null errors:
            _currentTest = "";

            // INITIALISE _prevTimeInterval with a value of '0':
            _prevTimeInterval = 0;

            // INITIALISE _totalTime with a value of '0':
            _totalTime = 0;
        }

        #endregion


        #region IMPLEMENTATION OF IEXPORTEXCELDATA

        /// <summary>
        /// Exports any chosen data to MS Excel
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// <param name="pValueList"> List of value </param>
        /// <CITATION> (Cerutti, 2016) </CITATION>
        public void ExportToExcel(string pTestName, string pTestType, IEnumerable pValueList)
        {
            // IF _excelWorkbook DOES NOT contain a Worksheet with the same value as pTestName:
            if (!_excelWorkbook.Worksheets.Contains(pTestName))
            {
                // ADD Worksheet named using pTestName to _excelWorkbook:
                _excelWorkbook.AddWorksheet(pTestName);

                // DECLARE & INITIALISE an IXLWorksheet with the result of _excelWorkbook.Worksheet():
                IXLWorksheet excelWorksheet = _excelWorkbook.Worksheet(pTestName);

                // DECLARE & INITIALISE an int with a value of '1', name it 'row':
                // USED FOR EXCEL SPREADSHEET ROW
                int row = 1;

                // IF pValueList stores float values:
                if (pValueList is IList<float>)
                {
                    // FOREACH float in pValueList:
                    foreach (float pValue in pValueList)
                    {
                        // ADD pValue to Cell ("A" + row):
                        excelWorksheet.Cell("A" + row).Value = pValue;

                        // INCREMENT row by '1':
                        row++;
                    }
                }
                // IF pValueList stores long values:
                else if (pValueList is IList<long>)
                {
                    // FOREACH long in pValueList:
                    foreach (long pValue in pValueList)
                    {
                        // ADD pValue to Cell ("A" + row):
                        excelWorksheet.Cell("A" + row).Value = pValue;

                        // INCREMENT row by '1':
                        row++;
                    }
                }
            }

            // WRITE Excel Workbook save to console:
            Console.WriteLine(pTestName + " has been saved to the Workbook!");

            // IF Creation and Termination HAVE BEEN time tested:
            if (_excelWorkbook.Worksheets.Contains("CreationTest") && _excelWorkbook.Worksheets.Contains("TerminationTest"))
            {
                // CALL FinishTesting(), passing pTestType as a parameter:
                FinishTesting(pTestType);
            }

            // IF CPU and RAM HAVE BEEN tested:
            if (_excelWorkbook.Worksheets.Contains("CPUTest") && _excelWorkbook.Worksheets.Contains("RAMTest"))
            {
                // CALL FinishTesting(), passing pTestType as a parameter:
                FinishTesting(pTestType);
            }

            // IF FPS HAS BEEN tested:
            if (_excelWorkbook.Worksheets.Contains("FPSTest"))
            {
                // CALL FinishTesting(), passing pTestType as a parameter:
                FinishTesting(pTestType);
            }
        }

        /// <summary>
        /// Method which initialises caller with an IXLWorkbook instance
        /// </summary>
        /// <param name="pExcelWorkbook"> Workbook instance </param>
        public void Initialise(IXLWorkbook pExcelWorkbook)
        {
            // IF pExcelWorkbook DOES HAVE an active instance:
            if (pExcelWorkbook != null)
            {
                // INITIALISE _excelWorkbook with reference to pExcelWorkbook:
                _excelWorkbook = pExcelWorkbook;
            }
            // IF pTimer DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pExcelWorkbook does not have an active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISESTOPWATCH

        /// <summary>
        /// Method which initialises caller with a Stopwatch instance
        /// </summary>
        /// <param name="pTimer"> Stopwatch instance </param>
        public void Initialise(Stopwatch pTimer)
        {
            // IF pTimer DOES HAVE an active instance:
            if (pTimer != null)
            {
                // INITIALISE _timer with reference to pTimer:
                _timer = pTimer;
            }
            // IF pTimer DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pTimer does not have an active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF ITESTPERFORMANCE

        /// <summary>
        /// Method which initialises a caller with a PerformanceCounter instance
        /// </summary>
        /// <param name="pHWName"> Name of Hardware Device </param>
        /// <param name="pHWStats"> PerformanceCounter instance </param>
        public void Initialise(string pHWName, PerformanceCounter pHWStats)
        {
            // IF pHWStats DOES HAVE an active instance:
            if (pHWStats != null)
            {
                // ADD pHWName as a key and pHWStats as a value to _hwCounters:
                _hwCounters.Add(pHWName, pHWStats);
            }
            // IF pHWStats DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pHWStats does not have an active instance!");
            }
        }

        /// <summary>
        /// Method which initialises a caller with a string and an IList<float> instance
        /// </summary>
        /// <param name="pTestName"> Name of Test </param>
        /// <param name="pFloatList"> List of float values </param>
        public void Initialise(string pTestName, IList<float> pFloatList)
        {
            // IF pFloatList DOES HAVE an active instance:
            if (pFloatList != null)
            {
                // ADD pTestName as a key and pFloatList as a value to _floatListDict:
                _floatListDict.Add(pTestName, pFloatList);
            }
            // IF pFloatList DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pFloatList does not have an active instance!");
            }
        }

        /// <summary>
        /// Method which initialises a caller with an IList<long> instance
        /// </summary>
        /// <param name="pLongList"> List of long values </param>
        public void Initialise(IList<long> pLongList)
        {
            // IF pLongList DOES HAVE an active instance:
            if (pLongList != null)
            {
                // INITIALISE _quickTimeList with reference to pLongList:
                _quickTimeList = pLongList;
            }
            // IF pLongList DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pLongList does not have an active instance!");
            }
        }

        /// <summary>
        /// Tests any specified resources in quick succession
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// <param name="pTime"> Time taken for quick test </param>
        /// <param name="pTestFinish"> Used for signalling that test results can be exported </param>
        public void QuickTimedTest(string pTestName, string pTestType, long pTime, bool pTestFinish)
        {
            // IF test has changed:
            if (!_currentTest.Equals(pTestName))
            {
                // CLEAR _quickTimeList:
                _quickTimeList.Clear();

                // INITIALISE _currentTest with value of pTestName:
                _currentTest = pTestName;
            }

            // IF pTestFinish is FALSE:
            if (!pTestFinish)
            {
                // ADD value of pTime to _quickTimeList:
                _quickTimeList.Add(pTime);

                // INCREMENT _totalTime by the value of pTime:
                _totalTime += pTime;
            }
            // IF pTestFinish is TRUE:
            else if (pTestFinish)
            {
                // ADD value of _totalTime to _quickTimeList:
                _quickTimeList.Add(_totalTime);

                // CALL ExportToExcel(), passing pTestName, pTestType and _quickTimeList as parameters:
                ExportToExcel(pTestName, pTestType, _quickTimeList);
            }
        }

        /// <summary>
        /// Tests any specified resources in a long timeframe
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        /// <CITATION> ('shytikov', 2018) </CITATION>
        public void LongTimedTest(GameTime pGameTime)
        {
            // IF _testStarted is TRUE:
            if (_testStarted)
            {
                // IF calling ResultIntervalUpdate() returns TRUE:
                if (ResultIntervalUpdate())
                {
                    // ADD CPU Usage % value to _floatListDict["CPU"]:
                    // USES ENVIRONMENT.PROCESSORCOUNT DUE TO MULTIPLE CORES ON PROCESSOR
                    _floatListDict["CPU"].Add(_hwCounters["CPU"].NextValue() / Environment.ProcessorCount);

                    // ADD RAM Usage (Bytes) value to _floatListDict["RAM"]:
                    _floatListDict["RAM"].Add(_hwCounters["RAM"].NextValue());
                }

                // IF _timer has exceeded FIVE minutes:
                if (_timer.Elapsed.TotalMinutes >= 5)
                {
                    // CALL UpdateLongTimedTest():
                    UpdateLongTimedTest();
                }
            }
            // IF _testStarted is FALSE and FIVE minutes have passed:
            else if (!_testStarted && _timer.Elapsed.TotalMinutes >= 5)
            {
                // CALL ExportToExcel(), passing TWO strings and _floatListDict["CPU"] as parameters:
                ExportToExcel("CPUTest", "HWTest", _floatListDict["CPU"]);

                // CALL ExportToExcel(), passing TWO strings and _floatListDict["RAM"] as parameters:
                ExportToExcel("RAMTest", "HWTest", _floatListDict["RAM"]);
            }
        }

        /// <summary>
        /// Tests FPS count in an application
        /// </summary>
        /// <param name="pGameTime">Provides a snapshot of timing values.</param>
        /// <CITATION> (Whitaker, No Date) </CITATION>
        public void TestFPS(GameTime pGameTime)
        {
            // IF _testStarted is TRUE:
            if (_testStarted)
            {
                // IF calling ResultIntervalUpdate() returns TRUE:
                if (ResultIntervalUpdate())
                {
                    // ADD FPS value to _floatListDict["FPS"]:
                    _floatListDict["FPS"].Add(1 / (float)pGameTime.ElapsedGameTime.TotalSeconds);
                }

                // IF _timer has exceeded FIVE minutes:
                if (_timer.Elapsed.TotalMinutes >= 5)
                {
                    // CALL UpdateLongTimedTest():
                    UpdateLongTimedTest();
                }
            }
            // IF _testStarted is FALSE and FIVE minutes have passed:
            else if (!_testStarted && _timer.Elapsed.TotalMinutes >= 5)
            {
                // CALL ExportToExcel(), passing TWO strings and _floatListDict["FPS"] as parameters:
                ExportToExcel("FPSTest", "FPSTest", _floatListDict["FPS"]);
            }
        }

        /// <summary>
        /// Used to time tasks which are completed over a long time e.g. CPU/RAM/FPS
        /// </summary>
        public void UpdateLongTimedTest()
        {
            // IF _testStarted is FALSE:
            if (!_testStarted)
            {
                // RESET _timer:
                _timer.Reset();

                // INTIIALISE _prevTimeInterval with value of '0':
                _prevTimeInterval = 0;

                // START _timer:
                _timer.Start();

                // SET _testStarted to true:
                _testStarted = true;
            }
            // IF _timer has been started
            else if (_testStarted)
            {
                // STOP _timer:
                _timer.Stop();

                // SET _testStarted to false:
                _testStarted = false;
            }
        }

        /// <summary>
        /// Finalises any testing and closes application
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// </summary>
        public void FinishTesting(string pTestType)
        {
            // SAVE _excelWork using Date and Time as well as the Test Type:
            _excelWorkbook.SaveAs("..\\..\\..\\..\\..\\..\\Tests\\ECS\\" + pTestType + "\\" + DateTime.Now.ToString("dd_MM_yy--HH_mm_ss") + ".xlsx");

            // CALL Exit() on entire Application:
            Application.Exit();
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Updates Result Interval and returns if Interval has been met
        /// </summary>
        /// <param> boolean value </param>
        private bool ResultIntervalUpdate()
        {
            // DECLARE & INITIALISE a bool, set it to false, name it 'intervalMet':
            bool intervalMet = false;

            // IF interval of 500ms has been met:
            if (_timer.ElapsedMilliseconds - _prevTimeInterval >= 500)
            {
                // SET intervalMet to true:
                intervalMet = true;

                // INITIALISE _prevTimeInterval with value of _timer.ElapsedMilliseconds:
                _prevTimeInterval = _timer.ElapsedMilliseconds;
            }

            // RETURN value of intervalMet:
            return intervalMet;
        }

        #endregion
    }
}
