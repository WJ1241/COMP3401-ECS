using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Components.Interfaces;

namespace COMP3401_Project.ECSPackage.Components
{
    /// <summary>
    /// Class used for storing data relative to an entity's visual status on screen
    /// Author: William Smith
    /// Date: 26/01/22
    /// </summary>
    public class TextureComponent : IComponent, ITexture
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<string, Texture2D>, name it '_textureDict':
        private IDictionary<string, Texture2D> _textureDict;

        // DECLARE a Texture2D, name it '_texture':
        private Texture2D _texture;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of TextureComponent
        /// </summary>
        public TextureComponent()
        {
            // INSTANTIATE _textureDict as a new Dictionary<string, Texture2D>():
            _textureDict = new Dictionary<string, Texture2D>();
        }

        #endregion


        #region IMPLEMENTATION OF ITEXTURE

        /// <summary>
        /// Allows callers to get reference to an instance of IDictionary<string, Texture2D>
        /// </summary>
        /// <returns> Reference to an IDictionary<string, Texture2D> </returns>
        public IDictionary<string, Texture2D> ReturnTextureDict()
        {
            // RETURN instance of _textureDict:
            return _textureDict;
        }

        /// <summary>
        /// Property which gives caller read and write access to a Texture object
        /// </summary>
        public Texture2D Texture
        {
            get
            {
                // RETURN value of _texture:
                return _texture;
            }
            set
            {
                // SET value of _texture to incoming value:
                _texture = value;
            }
        }

        #endregion
    }
}
