using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3401ECS_Engine.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have a Texture2D object
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public interface ITexture
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller read and write access to a Texture object
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// Property which gives caller read and write access to Texture Size
        /// </summary>
        Point TexSize { get; set; }

        #endregion


        #region METHODS

        /// <summary>
        /// Allows callers to get reference to an instance of IDictionary<string, Texture2D>
        /// </summary>
        /// <returns> Reference to an IDictionary<string, Texture2D> </returns>
        IDictionary<string, Texture2D> ReturnTextureDict();

        #endregion
    }
}
