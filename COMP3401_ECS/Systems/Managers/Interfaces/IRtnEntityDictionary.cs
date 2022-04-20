using System.Collections.Generic;
using COMP3401ECS_Engine.Entities.Interfaces;

namespace COMP3401ECS_Engine.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have return an IDictionary<int, IEntity> reference
    /// Author: William Smith
    /// Date: 21/12/21
    /// </summary>
    public interface IRtnEntityDictionary
    {
        #region METHODS

        /// <summary>
        /// Returns a reference to an Entity Dictionary
        /// </summary>
        IDictionary<int, IEntity> ReturnEntityDict();

        #endregion
    }
}
