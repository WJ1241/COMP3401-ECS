﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface that allows implementations to draw an object on screen
    /// Author: William Smith
    /// Date: 01/11/21
    /// </summary>
    public interface IDraw
    {
        #region METHODS

        /// <summary>
        /// When called, draws entity's texture on screen
        /// </summary>
        /// <param name="spriteBatch"> Needed to draw entity's texture on screen </param>
        void Draw(SpriteBatch spriteBatch);

        #endregion
    }
}
