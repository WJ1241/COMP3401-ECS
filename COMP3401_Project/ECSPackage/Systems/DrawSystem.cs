using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using COMP3401_Project.ECSPackage.Components.Interfaces;
using COMP3401_Project.ECSPackage.Entities.Interfaces;
using COMP3401_Project.ECSPackage.Exceptions;
using COMP3401_Project.ECSPackage.Systems.Interfaces;

namespace COMP3401_Project.ECSPackage.Systems
{
    /// <summary>
    /// System which uses Transform and Texture Components to draw entity on screen
    /// Author: William Smith
    /// Date: 30/01/22
    /// </summary>
    public class DrawSystem : System, IDraw
    {
        #region FIELD VARIABLES

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
            // INSTANTIATE _transformCompDict as a new Dictionary<int, IPosition>():
            _transformCompDict = new Dictionary<int, IPosition>();

            // INSTANTIATE _textureCompDict as a new Dictionary<int, ITexture>():
            _textureCompDict = new Dictionary<int, ITexture>();
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

            // IF _textureCompDict DOES HAVE an active instance:
            if (_textureCompDict != null)
            {
                // FOREACH UID in _textureCompDict:
                foreach (int pInt in _textureCompDict.Keys)
                {
                    // DRAW given texture, given location, colour, rotation and origin:
                    pSpriteBatch.Draw(_textureCompDict[pInt].Texture, _transformCompDict[pInt].Position, null, Color.AntiqueWhite, (_transformCompDict[pInt] as IRotation).RotationAngle, (_transformCompDict[pInt] as IRotation).Origin, 1f, SpriteEffects.None, 1f);
                }
            }
            // IF _textureCompDict DOES NOT HAVE an active instance:
            else
            {
                // THROW new NullInstanceException, with corresponding message:
                throw new NullInstanceException("ERROR: _textureCompDict does not have an active instance!");
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
        public override void Update(GameTime pGameTime)
        {
            // CALL AddToCompDictionaries() iteratively so references are not kept:
            AddToCompDictionaries();
        }

        #endregion


        #region PRIVATE METHODS

        /// <summary>
        /// Method which adds temporary current component references to local component dictionaries
        /// </summary>
        protected override void AddToCompDictionaries()
        {
            // CALL Clear() on _transformCompDict, prevents entities being added multiple times:
            _transformCompDict.Clear();

            // CALL Clear() on _textureCompDict, prevents entities being added multiple times:
            _textureCompDict.Clear();

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
