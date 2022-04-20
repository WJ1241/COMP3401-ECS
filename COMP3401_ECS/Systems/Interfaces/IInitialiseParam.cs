

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Initialises an implementation with ONE reference/value
    /// Author: William Smith
    /// Date: 20/04/22
    /// </summary>
    /// <typeparam name="T"> Generic 'T', any type </typeparam>
    public interface IInitialiseParam<T>
    {
        #region METHODS

        /// <summary>
        /// Initialises an implementation with a reference to 'T'
        /// </summary>
        /// <param name="pT"> Reference/Value of type 'T' </param>
        void Initialise(T pT);

        #endregion
    }
}
