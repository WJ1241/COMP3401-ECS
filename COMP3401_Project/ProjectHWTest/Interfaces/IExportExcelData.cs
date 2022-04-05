using System.Collections;
using ClosedXML.Excel;

namespace COMP3401_Project.ProjectHWTest.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to export data to an MS Excel spreadsheet
    /// Author: William Smith
    /// Date: 04/04/22
    /// </summary>
    public interface IExportExcelData
    {
        #region METHODS

        /// <summary>
        /// Exports any chosen data to MS Excel
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pTestType"> Name of Test Type e.g. Short Test (Creation/Termination), Long Test (CPU, RAM, FPS) </param> 
        /// <param name="pValueList"> List of floats </param>
        void ExportToExcel(string pTestName, string pTestType, IEnumerable pValueList);

        /// <summary>
        /// Method which initialises caller with an IXLWorkbook instance
        /// </summary>
        /// <param name="pExcelWorkbook"> Workbook instance </param>
        void Initialise(IXLWorkbook pExcelWorkbook);

        #endregion
    }
}
