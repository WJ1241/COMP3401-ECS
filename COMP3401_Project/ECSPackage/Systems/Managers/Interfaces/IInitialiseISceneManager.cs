using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a reference to an ISceneManager object
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface IInitialiseISceneManager
    {
        #region METHODS

        /// <summary>
        /// Method which initialises an object with an ISceneManager object
        /// </summary>
        /// <param name="pSceneManager"> ISceneManager object </param>
        void Initialise(ISceneManager pSceneManager);

        #endregion
    }
}
