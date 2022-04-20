using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;

namespace COMP3401ECS_Engine.Components
{
    /// <summary>
    /// Class which contains a PlayerIndex to ID a Player
    /// Author: William Smith
    /// Date: 09/01/22
    /// </summary>
    public class PlayerComponent : IComponent, IPlayer
    {
        #region FIELD VARIABLES

        // DECLARE a PlayerIndex, name it '_playerID':
        private PlayerIndex _playerID;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PlayerComponent
        /// </summary>
        public PlayerComponent()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IPLAYER

        /// <summary>
        /// Property which can get and set value of a Player's ID Number
        /// </summary>
        public PlayerIndex PlayerID
        {
            get
            {
                // RETURN value of _playerID:
                return _playerID;
            }
            set
            {
                // SET value of _playerID to incoming value:
                _playerID = value;
            }
        }

        #endregion
    }
}
