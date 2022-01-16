using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses Transform and Texture Components to draw entity on screen
    /// Author: William Smith
    /// Date: 13/01/22
    /// </summary>
    public class DrawSystem : IInitialiseIROIEntityDictionary, IDraw, IUpdatable
    {
        #region FIELD VARIABLES

        // DECLARE an IReadOnlyDictionary<int, IEntity>, name it '_roEntityDict':
        private IReadOnlyDictionary<int, IEntity> _roEntityDict;

        // DECLARE an IDictionary<int, IPosition>, name it '_transformCompDict':
        private IDictionary<int, IPosition> _transformCompDict;

        // DECLARE an IDictionary<int, ITexture>, name it '_textureCompDict':
        private IDictionary<int, ITexture> _textureCompDict;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of DrawSystem
        /// </summary>
        public DrawSystem()
        {
            // EMPTY CONSTRUCTOR
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


        #region IMPLEMENTATION OF IDRAW

        /// <summary>
        /// When called, draws entity's texture on screen
        /// </summary>
        /// <param name="pSpriteBatch"> Needed to draw entity's texture on screen </param>
        public void Draw(SpriteBatch pSpriteBatch)
        {
            // CALL Begin() on pSpriteBatch to allow entities with Draw Components to be drawn on screen:
            pSpriteBatch.Begin();

            // FOREACH UID in _textureCompDict:
            foreach (int pInt in _textureCompDict.Keys)
            {
                // DRAW given texture, given location, and colour:
                pSpriteBatch.Draw(_textureCompDict[pInt].Texture, _transformCompDict[pInt].Position, Color.AntiqueWhite);
            }

            // CALL End() on pSpriteBatch to signal end of entity drawing iteration:
            pSpriteBatch.End();
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

            // INSTANTIATE _textureCompDict as a new Dictionary<int, ITexture>():
            _textureCompDict = new Dictionary<int, ITexture>();

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

                    // IF pComponent implements ITexture:
                    if (pComponent is ITexture)
                    {
                        // ADD currently selected entity's ITexture Component to _textureCompDict:
                        _textureCompDict.Add(_roEntityDict[pInt].UID, pComponent as ITexture);
                    }
                }
            }
        }

        #endregion
    }
}
