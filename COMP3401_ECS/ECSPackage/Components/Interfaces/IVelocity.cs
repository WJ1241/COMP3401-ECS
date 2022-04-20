using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace COMP3401ECS_Engine.Components.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have Speed, Direction and Velocity Properties
    /// Author: William Smith
    /// Date: 23/01/22
    /// </summary>
    public interface IVelocity
    {
        #region PROPERTIES

        /// <summary>
        /// Property which gives caller read and write access to speed value
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Property which gives caller read and write access to direction value
        /// </summary>
        Vector2 Direction { get; set; }

        /// <summary>
        /// Property which gives caller read and write access to velocity value
        /// </summary>
        Vector2 Velocity { get; set; }

        #endregion
    }
}
