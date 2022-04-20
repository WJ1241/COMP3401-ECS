using COMP3401ECS_Engine.Entities.Interfaces;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to act on given entities that require change from player input
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public interface IInputResponder : IResponder
    {
        #region METHODS

        /// <summary>
        /// Method which is given an entity that needs updating from player input
        /// </summary>
        /// <param name="pEntity"> Entity which needs update from player input </param>
        void RespondToInput(IEntity pEntity);

        #endregion
    }
}
