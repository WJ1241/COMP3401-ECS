using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3401_Project.ECSPackage.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have a Texture object
    /// Author: William Smith
    /// Date: 24/10/21
    /// </summary>
    public interface ITexture
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller read and write access to a Texture object
        /// </summary>
        Texture2D Texture { get; set; }

        #endregion
    }
}
