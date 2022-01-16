using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Delegates.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialise with a 'Delete' delegate
    /// Author: William Smith
    /// Date: 21/12/21
    /// </summary>
    public interface IInitialiseDeleteDel
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with a 'DeleteDelegate' method
        /// </summary>
        /// <param name="pDeleteDel"> Delete Method </param>
        void Initialise(DeleteDelegate pDeleteDel);

        #endregion
    }
}
