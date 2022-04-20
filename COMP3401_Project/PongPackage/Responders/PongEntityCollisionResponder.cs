using Microsoft.Xna.Framework;
using COMP3401ECS_Engine.Components.Interfaces;
using COMP3401ECS_Engine.Entities.Interfaces;
using COMP3401ECS_Engine.Systems.Interfaces;

namespace COMP3401ECS.PongPackage.Responders
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
            // DECLARE & INITIALISE a Vector2, using pEntityOne's velocity values, name it 'tempVelEntOne':
            Vector2 tempVelEntOne = ((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity;

            // DECLARE & INITIALISE a Vector2, using pEntityTwo's velocity values, name it 'tempVelEntTwo':
            Vector2 tempVelEntTwo = ((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity;

            // IF pEntityOne is on Layer 3 (Ball):
            if (((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 3)
            {
                // IF moving left
                if (tempVelEntOne.X < 0)
                {
                    // MINUS 0.2 multiplied by tempVelEntTwo's Length, from tempVelEntOne:
                    tempVelEntOne.X -= 0.2f * tempVelEntTwo.Length();
                }

                // ELSE IF moving right
                else if (tempVelEntOne.X > 0)
                {
                    // ADD 0.2 multiplied by tempVelEntTwo's Length, to tempVelEntOne:
                    tempVelEntOne.X += 0.2f * tempVelEntTwo.Length();
                }

                // REVERSE tempVelEntOne.X:
                tempVelEntOne.X *= -1;

                // RE-ASSIGN tempVelEntOne to _pEntityOne's Velocity value:
                ((pEntityOne as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity = tempVelEntOne;
            }
            // ELSE IF pEntityTwo is on Layer 3 (Ball):
            else if (((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["LayerComponent"] as ILayer).Layer == 3)
            {
                // IF moving left
                if (tempVelEntTwo.X < 0)
                {
                    // MINUS 0.2 multiplied by tempVelEntOne's Length, from tempVelEntTwo:
                    tempVelEntTwo.X -= 0.2f * tempVelEntTwo.Length();
                }

                // ELSE IF moving right
                else if (tempVelEntTwo.X > 0)
                {
                    // ADD 0.2 multiplied by tempVelEntOne's Length, to tempVelEntTwo:
                    tempVelEntTwo.X += 0.2f * tempVelEntOne.Length();
                }

                // REVERSE tempVelEntTwo.X:
                tempVelEntTwo.X *= -1;

                // RE-ASSIGN tempVelEntTwo to _pEntityTwo's Velocity value:
                ((pEntityTwo as IRtnROIComponentDictionary).ReturnComponentDictionary()["VelocityComponent"] as IVelocity).Velocity = tempVelEntTwo;
            }
        }
        
        #endregion
    }
}