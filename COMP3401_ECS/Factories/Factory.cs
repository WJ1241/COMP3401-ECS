using System;
using COMP3401ECS_Engine.Exceptions;
using COMP3401ECS_Engine.Factories.Interfaces;

namespace COMP3401ECS_Engine.Factories
{
    /// <summary>
    /// Class which creates an object and returns it as its most abstract form needed
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    /// <typeparam name="A"> 'A'bstract Class type </typeparam>
    public class Factory<A> : IFactory<A>
    {
        #region IMPLEMENTATION OF IFACTORY<A>

        /// <summary>
        /// Creates an 'C'oncrete object, and returned using its 'A'bstract type
        /// </summary>
        /// <typeparam name="C"> 'C'oncrete object </typeparam>
        /// <returns> Instance of C typed as A </returns>
        public A Create<C>() where C : A, new()
        {
            // DECLARE an A, name it 'tempObj':
            A tempObj;

            // TRY checking if class implements A:
            try
            {
                // INSTANTIATE tempObj as A new C():
                tempObj = new C();
            }
            // CATCH Exception from creation of object:
            catch (Exception)
            {
                // THROW new ClassDoesNotExistException, with corresponding message:
                throw new ClassDoesNotExistException("ERROR: Class passed through parameter of method does not exist or implement Type in place of 'A'!");
            }

            // RETURN instance of tempObj:
            return tempObj;
        }

        #endregion
    }
}
