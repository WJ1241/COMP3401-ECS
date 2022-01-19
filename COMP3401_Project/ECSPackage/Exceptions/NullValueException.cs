using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Exceptions
{
    /// <summary>
    /// Exception class used for testing if an addressed object is missing a value in a stored variable
    /// Author: William Smith
    /// Date: 24/10/21
    /// </summary>
    public class NullValueException : Exception
    {
        /// <summary>
        /// Constructor for objects of NullValueException, calls base 'Exception' constructor to pass 'pMessage' value
        /// </summary>
        /// <param name="pMessage">string value used to display error message to user</param>
        public NullValueException(string pMessage) : base(pMessage)
        {

        }
    }
}
