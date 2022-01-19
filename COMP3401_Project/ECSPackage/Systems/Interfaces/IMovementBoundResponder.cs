﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using Microsoft.Xna.Framework;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to act on given moveable entities and respond in ways specific to a game
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IMovementBoundResponder
    {
        #region METHODS

        /// <summary>
        /// Method which is given an entity that needs action from reaching game bounds
        /// </summary>
        void RespondToBound(IEntity pEntity);

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Property which allows only write access to a Point specifying minimum positional game bounds
        /// </summary>
        Point MinXYBound { set; }

        /// <summary>
        /// Property which allows only write access to a Point specifying maximum positional game bounds
        /// </summary>
        Point MaxXYBound { set; }

        #endregion
    }
}