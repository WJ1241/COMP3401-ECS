using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.PongPackage.Responders.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have their input commands tested
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public interface ITestKBInput
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller write access to what key is pressed
        /// </summary>
        string SetKeyPressed { set; }

        #endregion
    }
}
