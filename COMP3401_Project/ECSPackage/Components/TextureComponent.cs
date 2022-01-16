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
    /// Date: 24/10/21
    /// </summary>
    public class TextureComponent : IComponent, ITexture
    {
        #region FIELD VARIABLES

        // DECLARE a Texture2D, name it '_texture':
        private Texture2D _texture;

        #endregion


        #region IMPLEMENTATION OF ITEXTURE

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
