﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP3401_Project.ECSPackage.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have Speed and Direction Properties
    /// </summary>
    public interface IVelocity
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller read and write access to speed value
        /// </summary>
        int Speed { get; set; }

        /// <summary>
        /// Property which gives caller read and write access to direction value
        /// </summary>
        Vector2 Direction { get; set; }

        #endregion
    }
}