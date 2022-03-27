

namespace COMP3401_Project.ECSPackage.Delegates.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with a 'Create' delegate
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IInitialiseCreateDel
    {
        #region METHODS

        /// <summary>
        /// Initialises an object with a 'CreateDelegate' method
        /// </summary>
        /// <param name="pCreateDelegate"> Create Method </param>
        void Initialise(CreateDelegate pCreateDelegate);

        #endregion
    }
}
