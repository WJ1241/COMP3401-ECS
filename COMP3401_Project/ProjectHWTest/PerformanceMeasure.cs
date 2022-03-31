using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using ClosedXML.Excel;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Services.Interfaces;
using COMP3401_Project.ProjectHWTest.Interfaces;

namespace COMP3401_Project_ProjectHWTest
{
    /// <summary>
    /// Class which gets current usages from the user's system and sends results to an Excel Spreadsheet
    /// Author: William Smith
    /// Date: 31/03/22
    /// </summary>
    /// <REFERENCE> Cerutti, T. (2016) Exporting the values in List to excel. Available at: https://stackoverflow.com/questions/2206279/exporting-the-values-in-list-to-excel. (Accessed: 31 March 2022). </REFERENCE>
    public class PerformanceMeasure : IExportExcelData, IInitialiseStopwatch, IService, ITestPerformance
    {
        #region J0B LIST

        /*
                    Thursday and Friday Jobs
            - Get GPU Usage Done
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
            - GPU usage Average over 5 minutes with 50 entities
            - RAM usage Average over 5 minutes with 50 entities

	            500 entities
            - FPS Average over 5 minutes with 500 entities
            - CPU usage Average over 5 minutes with 500 entities
            - GPU usage Average over 5 minutes with 500 entities
            - RAM usage Average over 5 minutes with 500 entities
         
        */

        #endregion


        #region FIELD VARIABLES

        // DECLARE a XLWorkbook, name it '_excelWorkbook':
        private XLWorkbook _excelWorkbook;

        // DECLARE a PerformanceCounter, name it '_hwStats':
        private PerformanceCounter _hwStats;

        // DECLARE a Stopwatch, name it '_timer':
        private Stopwatch _timer;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PerformanceMeasure
        /// </summary>
        public PerformanceMeasure()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IEXPORTEXCELDATA

        /// <summary>
        /// Exports any chosen data to MS Excel
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pValueList"> List of floats </param>
        /// <CITATION> (Cerutti, 2016) </CITATION>
        public void ExportToExcel(string pTestName, IList<float> pValueList)
        {
            // ADD Worksheet named using pTestName to _excelWorkbook:
            _excelWorkbook.AddWorksheet(pTestName);

            // DECLARE & INITIALISE an IXLWorksheet with the result of _excelWorkbook.Worksheet():
            IXLWorksheet excelWorksheet = _excelWorkbook.Worksheet(pTestName);

            // DECLARE & INITIALISE an int with a value of '1', name it 'row':
            // USED FOR EXCEL SPREADSHEET ROW
            int row = 1;

            // FOREACH float in pValueList:
            foreach (float pValue in pValueList)
            {
                // ADD pValue to Cell ("A" + row):
                excelWorksheet.Cell("A" + row).Value = pValue;

                // INCREMENT row by '1':
                row++;
            }

            // SAVE _excelWork using Date and Time as well as the Test Name:
            _excelWorkbook.SaveAs("..\\..\\..\\..\\..\\..\\Tests\\" + pTestName + "\\" + DateTime.Now.ToString("dd_MM_yy--HH_mm_ss") + ".xlsx");

            // WRITE Excel Workbook save to console:
            Console.WriteLine(pTestName + " has been saved to the Workbook!");

            // IF Creation and Termination HAVE BEEN time tested:
            if (_excelWorkbook.Worksheets.Contains("CreationTest") && _excelWorkbook.Worksheets.Contains("TerminationTest"))
            {
                // CALL FinishTesting():
                FinishTesting();
            }

            // IF CPU, GPU, RAM and FPS HAVE BEEN tested:
            if (_excelWorkbook.Worksheets.Contains("CPUTest") && _excelWorkbook.Worksheets.Contains("GPUTest") &&
                _excelWorkbook.Worksheets.Contains("RAMTest") && _excelWorkbook.Worksheets.Contains("FPSTest"))
            {
                // CALL FinishTesting():
                FinishTesting();
            }
        }

        /// <summary>
        /// Method which initialises caller with a Workbook instance
        /// </summary>
        /// <param name="pExcelWorkbook"> Workbook instance </param>
        public void Initialise(XLWorkbook pExcelWorkbook)
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
        /// <param name="pHWStats"> PerformanceCounter instance </param>
        public void Initialise(PerformanceCounter pHWStats)
        {
            // IF pHWStats DOES HAVE an active instance:
            if (pHWStats != null)
            {
                // INITIALISE _hwStats with reference to pHWStats:
                _hwStats = pHWStats;
            }
            // IF pHWStats DOES NOT HAVE an active instance:
            else
            {
                // THROW a new NullInstanceException(), with corresponding message:
                throw new NullInstanceException("ERROR: pHWStats does not have an active instance!");
            }
        }

        /// <summary>
        /// Tests how long it takes to create entities
        /// </summary>
        public void TestCreation()
        {

        }

        /// <summary>
        /// Tests how long it takes to terminate entities
        /// </summary>
        public void TestTermination()
        {

        }

        /// <summary>
        /// Tests CPU Usage in an application
        /// </summary>
        public void TestCPUUsage()
        {

        }

        /// <summary>
        /// Tests RAM Usage in an application
        /// </summary>
        public void TestRAMUsage()
        {

        }

        /// <summary>
        /// Tests GPU Usage in an application
        /// </summary>
        public void TestGPUUsage()
        {

        }

        /// <summary>
        /// Tests FPS count in an application
        /// </summary>
        public void TestFPS()
        {

        }

        /// <summary>
        /// Finalises any testing and closes application
        /// </summary>
        public void FinishTesting()
        {
            // CALL Exit() on entire Application:
            Application.Exit();
        }

        #endregion
    }
}
