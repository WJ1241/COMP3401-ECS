using COMP3401ECS_Engine.Services.Interfaces;

namespace COMP3401ECS_Engine.Factories.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to create objects of any type
    /// Author: William Smith
    /// Date: 21/12/21
    /// </summary>
    /// <typeparam name="A"> 'A'bstract Class type </typeparam>
    public interface IFactory<A> : IService
    {
        #region METHODS

        /// <summary>
        /// Creates an 'C'oncrete object, and returned using its 'A'bstract type
        /// </summary>
        /// <typeparam name="C"> 'C'oncrete object </typeparam>
        /// <returns> Instance of C typed as A </returns>
        A Create<C>() where C : A, new();

        #endregion
    }
}
