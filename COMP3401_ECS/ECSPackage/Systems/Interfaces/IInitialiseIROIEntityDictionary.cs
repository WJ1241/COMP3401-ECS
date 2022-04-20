using System.Collections.Generic;
using COMP3401ECS_Engine.Entities.Interfaces;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to be initialised with an IReadOnlyDictionary
    /// Author: William Smith
    /// Date: 01/11/21
    /// </summary>
    public interface IInitialiseIROIEntityDictionary
    {
        #region METHODS

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict);

        #endregion
    }
}
