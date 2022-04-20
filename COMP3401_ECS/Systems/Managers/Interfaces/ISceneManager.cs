

using COMP3401ECS_Engine.Services.Interfaces;

namespace COMP3401ECS_Engine.Systems.Managers.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to manage a currently loaded scene
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public interface ISceneManager : IService
    {
        #region METHODS

        /// <summary>
        /// Removes an Entity from the scene using an ID
        /// </summary>
        /// <param name="pID"> Identification for chosen entity to remove from scene </param>
        void Remove(int pID);

        #endregion
    }
}
