using COMP3401ECS_Engine.Entities.Interfaces;

namespace COMP3401ECS_Engine.Systems.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to act on given collidable entities and respond in ways specific to a game
    /// Author: William Smith
    /// Date: 14/01/22
    /// </summary>
    public interface ICollisionResponder : IResponder
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
