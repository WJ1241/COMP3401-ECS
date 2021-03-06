using Microsoft.Xna.Framework;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface that allows implementations to be updated with a GameTime object
    /// Author: William Smith
    /// Date: 01/11/21
    /// </summary>
    public interface IUpdatable
    {
        #region METHODS

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        void Update(GameTime pGameTime);

        #endregion
    }
}
