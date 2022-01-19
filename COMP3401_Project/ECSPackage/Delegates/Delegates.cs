using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP3401_Project.ECSPackage.Delegates
{
    //----------------------------------------//

    /// <summary>
    /// C# File to store Delegate Methods
    /// Author: William Smith
    /// Date: 19/01/21
    /// </summary>

    //----------------------------------------//

    /// <summary>
    /// Delegate used for Creation
    /// </summary>
    public delegate void CreateDelegate();

    /// <summary>
    /// Delegate used for Deletion with an integer parameter
    /// </summary>
    /// <param name="pInt">any integer to be used for deletion process (Integer, Object Ref)</param>
    public delegate void DeleteDelegate(int pInt);
}
