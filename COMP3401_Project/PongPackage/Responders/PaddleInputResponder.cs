using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to user input to control Paddle on screen
    /// Author: William Smith
    /// Date: 19/01/22
    /// </summary>
    public class PaddleInputResponder : IInputResponder
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PaddleInputResponder
        /// </summary>
        public PaddleInputResponder()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IINPUTRESPONDER

        /// <summary>
        /// Method which is given an entity that needs updating from player input
        /// </summary>
        /// <param name="pEntity"> Entity which needs update from player input </param>
        public void RespondToInput(IEntity pEntity)
        {
            // DECLARE & INITIALISE a KeyboardState, name it '_kBState', give value of current Keyboard state:
            KeyboardState _kBState = Keyboard.GetState();

            // DECLARE & INITIALISE a PlayerIndex, name it '_currentPlayerNum', give value of current entity's PlayerID Property:
            PlayerIndex _currentPlayerNum = ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["PlayerComponent"] as IPlayer).PlayerID;

            // INSTANTIATE new Vector2, set as 0 to stop movement:
            ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0);

            // IF Player 1:
            if (_currentPlayerNum == PlayerIndex.One)
            {
                // IF W Key is down:
                if (_kBState.IsKeyDown(Keys.W))
                {
                    // SET direction of pEntity to '-1' to move upwards:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                }

                // ELSE IF S Key is down:
                else if (_kBState.IsKeyDown(Keys.S))
                {
                    // SET direction of pEntity to '1' to move downwards:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                }
            }

            // ELSE IF Player 2:
            else if (_currentPlayerNum == PlayerIndex.Two)
            {
                // IF Up Arrow Key is down:
                if (_kBState.IsKeyDown(Keys.Up))
                {
                    // SET direction of pEntity to '-1' to move upwards:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, -1);
                }

                // ELSE IF Down Arrow Key is down:
                else if (_kBState.IsKeyDown(Keys.Down))
                {
                    // SET direction of pEntity to '1' to move downwards:
                    ((pEntity as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Direction = new Vector2(0, 1);
                }
            }
        }

        #endregion
    }
}
