using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Factories.Interfaces;

namespace COMP3401_Project.ECSPackage.Factories
{
    /// <summary>
    /// Class which creates an object and returns it as its most abstract form needed
    /// Author: William Smith
    /// Date: 21/12/21
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
            // DECLARE & INSTANTIATE an A as a C, name it _tempObj:
            A _tempObj = new C();

            // RETURN instance of _tempObj:
            return _tempObj;
        }

        #endregion
    }
}
