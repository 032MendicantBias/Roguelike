using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace RogueLike.CoreObjects
{
    public enum Anchor
    {
        kTop = 1 << 0,
        kBottom = 1 << 1,
        kLeft = 1 << 2,
        kRight = 1 << 3,
        kCentre = 1 << 4,
        kTopLeft = kTop | kLeft,
        kTopCentre = kTop | kCentre,
        kTopRight = kTop | kRight,
        kCentreLeft = kCentre | kLeft,
        kCentreRight = kCentre | kRight,
        kBottomLeft = kBottom | kLeft,
        kBottomCentre = kBottom | kCentre,
        kBottomRight = kBottom | kRight,
    }

    /// <summary>
    /// The base class for any UI or game objects in our game.
    /// Marked as abstract, because we should not be able to create an instance of this class as it does not have enough functionality to serve a purpose
    /// </summary>
    public class BaseObject : Component
    {
        #region Properties and Fields

        /// <summary>
        /// A string to store the texture asset for this object
        /// </summary>
        public string TextureAsset { get; set; }

        /// <summary>
        /// The texture for this object - override if we have different textures that need to be drawn at different times
        /// </summary>
        private Texture2D texture;
        protected virtual Texture2D Texture
        {
            get
            {
                if (texture == null)
                {
                    Debug.Assert(!string.IsNullOrEmpty(TextureAsset));
                    texture = AssetManager.GetSprite(TextureAsset);
                }

                return texture;
            }
            set { texture = value; }
        }

        /// <summary>
        /// This is a cached vector that will only be set once.  Used in the draw method to indicate the dimensions of the texture.
        /// Will be set when the texture is loaded ONLY.
        /// </summary>
        private Vector2 TextureDimensions { get; set; }

        /// <summary>
        /// This is a cached vector that will only be set once.  Used in the draw method to indicate the centre of the texture.
        /// Will be set when the texture is loaded ONLY.
        /// </summary>
        public virtual Vector2 TextureCentre { get; set; }

        /// <summary>
        /// A source rectangle used to specify a sub section of the Texture2D to draw.
        /// Useful for animations and bars and by default set to (0, 0, texture width, texture height).
        /// </summary>
        public Rectangle SourceRectangle { get; set; }
        
        /// <summary>
        /// The relative anchor to the parent.
        /// This anchor will determine where the centre of this object is placed in relation to the edge of the rectangle created by the size of the parent.
        /// </summary>
        private Anchor Anchor { get; set; }

        /// <summary>
        /// Determines where the centre of this object is placed in terms of it's anchor
        /// </summary>
        private int Depth { get; set; }
        
        /// <summary>
        /// The colour of the object - by default this is set to white, so that white in the png will appear transparent
        /// </summary>
        public Color Colour { get; set; }

        /// <summary>
        /// The opacity of the object - between 0 and 1.  A value of 0 makes the texture completely transparent, and 1 completely opaque
        /// </summary>
        public float Opacity { get; set; }

        /// <summary>
        /// A property that can be used to reverse an image - useful for animations or sprites that are facing just one way.
        /// By default, this is SpriteEffects.None.
        /// </summary>
        protected SpriteEffects SpriteEffect { get; set; }

        /// <summary>
        /// A bool to indicate whether we should add a collider during initialisation.
        /// Some objects (like text) do not need a collider - this is an optimisation step.
        /// </summary>
        public bool UsesCollider { get; set; }

        #endregion

        public BaseObject(Vector2 localPosition, string textureAsset) :
            base()
        {
            Anchor = Anchor.kCentre;
            Depth = 0;
            TextureAsset = textureAsset;
            Opacity = 1;
            UsesCollider = true;
            SpriteEffect = SpriteEffects.None;
        }

        public BaseObject(Vector2 size, Vector2 localPosition, string textureAsset) :
            this(localPosition, textureAsset)
        {
            // Implement size
        }

        public BaseObject(Anchor anchor, int depth, string textureAsset) :
            base()
        {
            Anchor = anchor;
            Depth = depth;
            TextureAsset = textureAsset;
            Opacity = 1;
            UsesCollider = true;
            SpriteEffect = SpriteEffects.None;
        }

        public BaseObject(Vector2 size, Anchor anchor, int depth, string textureAsset) :
            this(anchor, depth, textureAsset)
        {
            // Implement size
        }

        #region Virtual Functions

        /// <summary>
        /// Check that the texture has been loaded by doing a get call
        /// </summary>
        public override void LoadContent()
        {
            // Check to see whether we should load
            CheckShouldLoad();

            DebugUtils.AssertNotNull(Texture);

            base.LoadContent();
        }

        /// <summary>
        /// Set up the size if it has not been set already.
        /// Updates the local position using the anchor and depth.
        /// Adds the collider if it should.
        /// </summary>
        public override void Initialise()
        {
            CheckShouldInitialise();

            if (Texture != null)
            {
                TextureDimensions = new Vector2(Texture.Bounds.Width, Texture.Bounds.Height);
                TextureCentre = new Vector2(Texture.Bounds.Center.X, Texture.Bounds.Center.Y);

                // Set the source rectangle to the default size of the texture
                SourceRectangle = new Rectangle(
                     0, 0,
                     (int)TextureDimensions.X,
                     (int)TextureDimensions.Y);
            }

            //// If our size is zero (i.e. uninitialised) we use the texture's size (if it is not null)
            //if (Size == Vector2.Zero && Texture != null)
            //{
            //    Size = new Vector2(Texture.Bounds.Width, Texture.Bounds.Height);
            //}

            //// Check to see whether we have a non-trivial case for our positioning
            //if (Anchor != Anchor.kCentre || Depth != 0)
            //{
            //    if (Anchor.HasFlag(Anchor.kCentre))
            //    {
            //        if (Anchor.HasFlag(Anchor.kLeft) || Anchor.HasFlag(Anchor.kRight))
            //        {
            //            float xMultiplier = Anchor.HasFlag(Anchor.kLeft) ? -1 : 1;
            //            LocalPosition = new Vector2(0.5f * xMultiplier * (Parent.Size.X + Depth * Size.X), 0);
            //        }
            //        else
            //        {
            //            float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
            //            LocalPosition = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.X + Depth * Size.Y));
            //        }
            //    }
            //    else
            //    {
            //        float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
            //        LocalPosition = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.Y + Depth * Size.Y));

            //        if (Anchor.HasFlag(Anchor.kLeft))
            //        {
            //            LocalPosition -= new Vector2(0.5f * (Parent.Size.X + Depth * Size.X));
            //        }
            //        else if (Anchor.HasFlag(Anchor.kRight))
            //        {
            //            LocalPosition += new Vector2(0.5f * (Parent.Size.X + Depth * Size.Y));
            //        }
            //    }
            //}

            base.Initialise();
        }

        /// <summary>
        /// Draws the object's texture.
        /// If we wish to create an object, but not draw it, change it's ShouldDraw property to false.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // If we are drawing this object, it should have a valid texture
            // If we wish to create an object but not draw it, simply change it's ShouldDraw property
            DebugUtils.AssertNotNull(Texture);
            //spriteBatch.Draw(
            //    Texture,
            //    WorldPosition,
            //    null,
            //    SourceRectangle,
            //    TextureCentre,
            //    WorldRotation,
            //    Vector2.Divide(Size, TextureDimensions),
            //    Colour * Opacity,
            //    SpriteEffect,
            //    0);
        }
        
        #endregion
    }
}