using System.Collections.Generic;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Exceptions;
using COMP3401ECS_Engine.Systems.Interfaces;

namespace COMP3401ECS_Engine.Systems
{
    /// <summary>
    /// Abstract Class used for basic variables and methods that extended System classes require
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public abstract class System : IInitialiseParam<IReadOnlyDictionary<int, IEntity>>, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        protected IReadOnlyDictionary<int, IEntity> _roEntityDict;

        #endregion


        #region IMPLEMENTATION OF IINITIALISEPARAM<IREADONLYDICTIONARY<INT, IENTITY>>

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        public virtual void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict)
        {
            // IF pIRODict DOES HAVE an active instance:
            if (pIRODict != null)
            {
                // INITIALISE _roEntityDict with instance of pIRODict:
                _roEntityDict = pIRODict;
            }
            // IF _roEntityDict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _roEntityDict does not have active instance!");
            }
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public abstract void Update(GameTime pGameTime);

        #endregion


        #region PROTECTED METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        protected abstract void AddToCompDictionaries();

        #endregion
    }
}
