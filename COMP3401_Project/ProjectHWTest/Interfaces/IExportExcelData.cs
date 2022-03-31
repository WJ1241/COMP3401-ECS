using System.Collections.Generic;
using ClosedXML.Excel;

namespace COMP3401_Project.ProjectHWTest.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to export data to an MS Excel spreadsheet
    /// Author: William Smith
    /// Date: 31/03/22
    /// </summary>
    public interface IExportExcelData
    {
        #region METHODS

        /// <summary>
        /// Exports any chosen data to MS Excel
        /// </summary>
        /// <param name="pTestName"> Name of Test to export </param>
        /// <param name="pValueList"> List of floats </param>
        void ExportToExcel(string pTestName, IList<float> pValueList);

        /// <summary>
        /// Method which initialises caller with an XLWorkbook instance
        /// </summary>
        /// <param name="pExcelWorkbook"> Workbook instance </param>
        void Initialise(XLWorkbook pExcelWorkbook);

        #endregion
    }
}
