using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Entities.Interfaces;

namespace COMP3401ECS_Engine.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to spawn an entity with a given position
    /// Author: William Smith
    /// Date: 10/01/22
    /// </summary>
    public interface ISpawnEntity
    {
        #region METHODS

        /// <summary>
        /// Spawns Entity on screen with a given position
        /// </summary>
        /// <param name="pEntity"> Entity to be spawned on screen </param>
        /// <param name="pPosition"> Position for Entity to be placed </param>
        void Spawn(IEntity pEntity, Vector2 pPosition);

        #endregion
    }
}
