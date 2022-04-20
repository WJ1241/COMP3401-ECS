using System;

namespace COMP3401ECS_Engine.Exceptions
{
    /// <summary>
    /// Exception class used for testing whether a specified class exists in the Program
    /// Author: William Smith
    /// Date: 28/12/21
    /// </summary>
    public class ClassDoesNotExistException : Exception
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of ClassDoesNotExistException, calls base 'Exception' constructor to pass 'pMessage' value
        /// </summary>
        /// <param name="pMessage"> string value used to display error message to user </param>
        public ClassDoesNotExistException(string pMessage) : base(pMessage)
        {

        }

        #endregion
    }
}
