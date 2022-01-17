using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses Player Components to allow an entity to be controlled by a User/Player
    /// Author: William Smith
    /// Date: 17/01/22
    /// </summary>
    public class InputSystem : IInitialiseIROIEntityDictionary, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        private IReadOnlyDictionary<int, IEntity> _roEntityDict;

        // DECLARE an IDictionary<int, IEntity>, name it '_playerEntityDict':
        private IDictionary<int, IEntity> _playerEntityDict;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of InputSystem
        /// </summary>
        public InputSystem()
        {
            // INSTANTIATE _playerEntityDict as a new Dictionary<int, IEntity>():
            _playerEntityDict = new Dictionary<int, IEntity>();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEIROIENTITYDICTIONARY

        /// <summary>
        /// Method which initialises caller with an IReadOnlyDictionary<int, IEntity> instance
        /// </summary>
        /// <param name="pIRODict"> Instance of IReadOnlyDictionary<int, IEntity> </param>
        public void Initialise(IReadOnlyDictionary<int, IEntity> pIRODict)
        {
            // INITIALISE _roEntityDict with instance of pIRODict:
            _roEntityDict = pIRODict;
        }

        #endregion


        #region IMPLEMENTATION OF IUPDATABLE

        /// <summary>
        /// Updates system when a frame has been rendered on screen
        /// </summary>
        /// <param name="pGameTime"> holds reference to GameTime object </param>
        public void Update(GameTime pGameTime)
        {
            // CALL Clear() on _playerEntityDict, prevents entities being added multiple times:
            _playerEntityDict.Clear();
        }

        #endregion
    }
}
