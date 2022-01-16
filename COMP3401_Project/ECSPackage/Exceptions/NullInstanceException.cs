using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Exceptions
{
    /// <summary>
    /// Exception class used for testing if an addressed object is without an instance.
    /// Author: William Smith
    /// Date: 24/10/21
    /// </summary>
    public class NullInstanceException : Exception
    {
        /// <summary>
        /// Constructor for objects of NullInstanceException, calls base 'Exception' constructor to pass 'pMessage' value
        /// </summary>
        /// <param name="pMessage">string value used to display error message to user</param>
        public NullInstanceException(string pMessage) : base(pMessage)
        {

        }
    }
}
