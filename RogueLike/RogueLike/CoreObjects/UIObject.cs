using Microsoft.Xna.Framework;

namespace RogueLike.CoreObjects
{
    /// <summary>
    /// A class which extends the BaseObject with properties and methods common for UI.
    /// These include a lazily evaluated SpriteFont and a ScissorRectangle for rendering extended menus.
    /// </summary>
    public abstract class UIObject : BaseObject
    {
        #region Properties and Fields

        /// <summary>
        /// An object which can be used to store values/user data.
        /// Useful for buttons etc.
        /// </summary>
        public object StoredObject { get; set; }
        
        #endregion

        public UIObject(Vector2 localPosition, string textureAsset) :
            this(Vector2.Zero, localPosition, textureAsset)
        {
        }

        public UIObject(Vector2 size, Vector2 localPosition, string textureAsset) :
            base(size, localPosition, textureAsset)
        {

        }

        public UIObject(Anchor anchor, int depth, string textureAsset) :
            this(Vector2.Zero, anchor, depth, textureAsset)
        {
        }

        public UIObject(Vector2 size, Anchor anchor, int depth, string textureAsset) :
            base(size, anchor, depth, textureAsset)
        {

        }
    }
}
