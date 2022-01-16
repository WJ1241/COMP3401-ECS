﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Delegates;
using COMP3401_Project.ECSPackage.Delegates.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses Transform and Velocity Components to draw entity on screen
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public class MovementSystem : IInitialiseDeleteDel, IInitialiseIROIEntityDictionary, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        private IReadOnlyDictionary<int, IEntity> _roEntityDict;

        // DECLARE an IDictionary<int, IPosition>, name it '_transformCompDict':
        private IDictionary<int, IPosition> _transformCompDict;

        // DECLARE an IDictionary<int, IVelocity>, name it '_velocityCompDict':
        private IDictionary<int, IVelocity> _velocityCompDict;

        // DECLARE a DeleteDelegate, name it '_terminate':
        private DeleteDelegate _terminate;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of MovementSystem
        /// </summary>
        public MovementSystem()
        {
            // EMPTY CONSTRUCTOR
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEDELETEDEL

        public void Initialise(DeleteDelegate pDeleteDelegate)
        {
            // INITIALISE _terminate with value of pDeleteDelegate:
            _terminate = pDeleteDelegate;
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
            // CALL CreateDictionaries() iteratively so references are not kept:
            CreateDictionaries();

            // FOREACH Moveable entity:
            foreach (int pInt in _velocityCompDict.Keys)
            {
                // CHANGE Position of TransformComponent, using VelocityComponent's speed and direction properties:
                _transformCompDict[pInt].Position += _velocityCompDict[pInt].Speed * _velocityCompDict[pInt].Direction;

                if (_transformCompDict[pInt].Position.X >= 1000)
                {
                    // CALL _terminate, passing pInt as a parameter:
                    _terminate(pInt);
                }
            }
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which creates temporary dictionaries and adds current entities on screen to them
        /// </summary>
        private void CreateDictionaries()
        {
            // INSTANTIATE _transformCompDict as a new Dictionary<int, IPosition>():
            _transformCompDict = new Dictionary<int, IPosition>();

            // INSTANTIATE _velocityCompDict as a new Dictionary<int, IVelocity>():
            _velocityCompDict = new Dictionary<int, IVelocity>();

            // FOREACH UID in _roEntityCount:
            foreach (int pInt in _roEntityDict.Keys)
            {
                // FOREACH IComponent in currently selected entity's component dictionary:
                foreach (IComponent pComponent in (_roEntityDict[pInt] as IRtnROIComponentDictionary).ReturnComponentDictionary().Values)
                {
                    // IF pComponent implements IPosition:
                    if (pComponent is IPosition)
                    {
                        // ADD currently selected entity's IPosition Component to _transformCompDict:
                        _transformCompDict.Add(_roEntityDict[pInt].UID, pComponent as IPosition);
                    }

                    // IF pComponent requires movement:
                    if (pComponent is IVelocity)
                    {
                        // ADD currently selected entity's ITexture Component to _velocityCompDict:
                        _velocityCompDict.Add(_roEntityDict[pInt].UID, pComponent as IVelocity);
                    }
                }
            }
        }

        #endregion
    }
}