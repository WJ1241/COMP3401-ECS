

namespace COMP3401_Project.ECSPackage.Delegates.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a 'Create Multiple' delegate
    /// Author: William Smith
    /// Date: 06/03/22
    /// </summary>
    public interface IInitialiseCreateMultiDel
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with a 'CreateMultipleDelegate' method
        /// </summary>
        /// <param name="pCreateMultiDel"> Create Multiple Method </param>
        void Initialise(CreateMultipleDelegate pCreateMultiDel);

        #endregion
    }
}