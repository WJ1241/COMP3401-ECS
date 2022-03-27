

namespace COMP3401_Project.ECSPackage.Delegates.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a 'Delete' delegate
    /// Author: William Smith
    /// Date: 21/12/21
    /// </summary>
    public interface IInitialiseDeleteDel
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with a 'DeleteDelegate' method
        /// </summary>
        /// <param name="pDeleteDelegate"> Delete Method </param>
        void Initialise(DeleteDelegate pDeleteDelegate);

        #endregion
    }
}
