using COMP3401ECS_Engine.Components.Interfaces;

namespace COMP3401ECS_Engine.Entities.Interfaces
{
    /// <summary>
    /// Interface which allows implementations to have an ID Property and a List of IComponent objects
    /// Author: William Smith
    /// Date: 22/10/21
    /// </summary>
    public interface IEntity
    {
        #region METHODS

        /// <summary>
        /// Method which adds a component to an IEntity object's Component list
        /// </summary>
        /// <param name="pComponent"> Instance of IComponent </param>
        void AddComponent(IComponent pComponent);

        #endregion


        #region PROPERTIES

        /// <summary>
        /// Property which allows callers to have read and write access to unique ID
        /// </summary>
        int UID { get; set; }

        #endregion
    }
}
