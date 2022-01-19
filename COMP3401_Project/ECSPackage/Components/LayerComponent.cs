﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;

namespace COMP3401_Project.ECSPackage.Components
{
    /// <summary>
    /// Class which contains an integer to represent an entity's collision layer
    /// Author: William Smith
    /// Date: 09/01/22
    /// </summary>
    public class LayerComponent : IComponent, ILayer
    {
        #region FIELD VARIABLES

        // DECLARE an int, name it '_layer':
        private int _layer;

        #endregion


        #region IMPLEMENTATION OF ILAYER

        /// <summary>
        /// Property which allows read and write access of entity layer
        /// Layer 1: Non-Player Controlled Static Entity
        /// Layer 2: Non-Player Controlled Moveable Entity
        /// Layer 3: Player Controlled Moveable Entity
        /// Layer 4: GUI
        /// </summary>
        public int Layer
        {
            get
            {
                // RETURN value of _hitBox:
                return _layer;
            }
            set
            {
                // SET value of _layer to incoming value:
                _layer = value;
            }
        }

        #endregion
    }
}