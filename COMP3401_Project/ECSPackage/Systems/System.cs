using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;


namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// Abstract Class used for basic variables and methods that extended System classes require
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public abstract class System : IInitialiseIROIEntityDictionary, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        protected IReadOnlyDictionary<int, IEntity> _roEntityDict;

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIROIENTITYDICTIONARY

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
