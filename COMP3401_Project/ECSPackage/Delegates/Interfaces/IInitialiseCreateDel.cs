using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Delegates.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialise with a 'Create' delegate
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IInitialiseCreateDel
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with a 'CreateDelegate' method
        /// </summary>
        /// <param name="pCreateDel"> Create Method </param>
        void Initialise(CreateDelegate pCreateDelegate);

        #endregion
    }
}
