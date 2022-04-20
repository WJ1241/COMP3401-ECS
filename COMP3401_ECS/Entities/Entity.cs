using System.Collections.Generic;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Exceptions;

namespace COMP3401ECS_Engine.Entities
{
    /// <summary>
    /// Class used for individual entities, identified with an ID, and stores components
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public class Entity : IEntity, IRtnROIComponentDictionary
    {
        #region FIELD VARIABLES

        // DECLARE an IDictionary<string, IComponent>, name it '_componentDict':
        private IDictionary<string, IComponent> _componentDict;

        // DECLARE an int, name it '_uID':
        private int _uID;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of type Entity
        /// </summary>
        public Entity()
        {
            // INSTANTIATE _componentDict as a new Dictionary<string, IComponent>():
            _componentDict = new Dictionary<string, IComponent>();
        }

        #endregion


        #region IMPLEMENTATION OF IENTITY

        /// <summary>
        /// Method which adds a component to an IEntity object's Component Dictionary
        /// </summary>
        /// <param name="pComponent"> Instance of IComponent </param>
        public void AddComponent(IComponent pComponent)
        {
            // IF pComponent DOES HAVE an active instance:
            if (pComponent != null)
            {
                // ADD pComponent to _componentDict:
                _componentDict.Add(pComponent.GetType().Name, pComponent);
            }
            // IF pComponent DOES NOT HAVE an active instance:
            else if (pComponent == null) 
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: pComponent does not have an active instance!");
            }
        }

        /// <summary>
        /// Property which allows callers to have read and write access to unique ID
        /// </summary>
        public int UID
        {
            get
            {
                // RETURN value of _uID:
                return _uID;
            }
            set
            {
                // ASSIGN _uID with incoming value:
                _uID = value;
            }
        }

        #endregion


        #region IMPLEMENTATION OF IRTNROICOMPONENTDICTIONARY

        /// <summary>
        /// Allows callers to get reference to an instance of IReadOnlyDictionary<string, IComponent>
        /// </summary>
        /// <returns> Reference to an IReadOnlyDictionary<string, IComponent> </returns>
        public IReadOnlyDictionary<string, IComponent> ReturnComponentDictionary()
        {
            // RETURN _componentDict cast as an IReadOnlyDictionary<string, IComponent>:
            return _componentDict as IReadOnlyDictionary<string, IComponent>;
        }

        #endregion
    }
}
