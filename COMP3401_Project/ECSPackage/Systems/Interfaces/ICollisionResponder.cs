using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Entities.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to act on given collidable entities and respond in ways specific to a game
    /// Author: William Smith
    /// Date: 14/01/22
    /// </summary>
    public interface ICollisionResponder
    {
        #region METHODS

        /// <summary>
        /// Method which is given two entity IDs and acts on user defined responses for their collision
        /// </summary>
        /// <param name="pEntityOne"> First Entity </param>
        /// <param name="pEntityTwo"> Second Entity </param>
        void RespondToCollision(IEntity pEntityOne, IEntity pEntityTwo);

        #endregion
    }
}
