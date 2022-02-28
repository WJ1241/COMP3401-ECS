using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.PongPackage.Responders
{
    /// <summary>
    /// Class which responds to collisions between Pong Entities
    /// Author: William Smith
    /// Date: 09/02/22
    /// </summary>
    public class PongEntityCollisionResponder : ICollisionResponder
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of PongEntityCollisionResponder
        /// </summary>
        public PongEntityCollisionResponder()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF ICOLLISIONRESPONDER

        /// <summary>
        /// Method which is given two entity IDs and acts on user defined responses for their collision
        /// </summary>
        /// <param name="pEntityOne"> First Entity </param>
        /// <param name="pEntityTwo"> Second Entity </param>
        public void RespondToCollision(IEntity pEntityOne, IEntity pEntityTwo)
        {
            // DECLARE & INITIALISE a Vector2, using pEntityOne's velocity values, name it '_tempVelEntOne':
            Vector2 _tempVelEntOne = ((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity;

            // DECLARE & INITIALISE a Vector2, using pEntityTwo's velocity values, name it '_tempVelEntTwo':
            Vector2 _tempVelEntTwo = ((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity;

            // IF pEntityOne is on Layer 3 (Ball):
            if (((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 3)
            {
                // IF moving left
                if (_tempVelEntOne.X < 0)
                {
                    // MINUS 0.2 multiplied by _tempVelEntTwo's Length, from _tempVelEntOne:
                    _tempVelEntOne.X -= 0.2f * _tempVelEntTwo.Length();
                }

                // ELSE IF moving right
                else if (_tempVelEntOne.X > 0)
                {
                    // ADD 0.2 multiplied by _tempVelEntTwo's Length, to _tempVelEntOne:
                    _tempVelEntOne.X += 0.2f * _tempVelEntTwo.Length();
                }

                // REVERSE _tempVelEntOne.X:
                _tempVelEntOne.X *= -1;

                // RE-ASSIGN _tempVelEntOne to _pEntityOne's Velocity value:
                ((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity = _tempVelEntOne;
            }
            // ELSE IF pEntityTwo is on Layer 3 (Ball):
            else if (((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 3)
            {
                // IF moving left
                if (_tempVelEntTwo.X < 0)
                {
                    // MINUS 0.2 multiplied by _tempVelEntOne's Length, from _tempVelEntTwo:
                    _tempVelEntTwo.X -= 0.2f * _tempVelEntTwo.Length();
                }

                // ELSE IF moving right
                else if (_tempVelEntTwo.X > 0)
                {
                    // ADD 0.2 multiplied by _tempVelEntOne's Length, to _tempVelEntTwo:
                    _tempVelEntTwo.X += 0.2f * _tempVelEntOne.Length();
                }

                // REVERSE _tempVelEntTwo.X:
                _tempVelEntTwo.X *= -1;

                // RE-ASSIGN _tempVelEntTwo to _pEntityTwo's Velocity value:
                ((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity = _tempVelEntTwo;
            }
        }
        
        #endregion
    }
}