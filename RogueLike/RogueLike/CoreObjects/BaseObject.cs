using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.ObjectProperties;
using RogueLike.Physics;
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
        /// The transform of this object which holds information on it's local position and rotation in relation to a parent transform
        /// </summary>
        public Transform Transform { get; set; }

        /// <summary>
        /// The explicit size of the object in pixels (used for rendering).
        /// By default set to the dimensions of the texture of this object if set (otherwise zero).
        /// </summary>
        public Vector2 Size { get; set; }

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
        /// By default it is true.
        /// </summary>
        public bool UsesCollider { get; set; }

        /// <summary>
        /// The collision shape responsible for storing information and methods for collision detection.
        /// Will only be created if the UsesCollider flag is set to true.
        /// </summary>
        public ICollidableShape Collider { get; set; }

        /// <summary>
        /// A reference to a parent object which we will use for anchoring and positioning our object using the Anchor & Depth properties.
        /// Also useful for navigating a hierarchy at runtime to obtain information about another object (providing the hierarchy is known).
        /// </summary>
        public BaseObject Parent { get; private set; }

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
            Colour = Color.White;

            Transform = new Transform(localPosition, 0, Vector2.Zero);
        }

        public BaseObject(Vector2 size, Vector2 localPosition, string textureAsset) :
            this(localPosition, textureAsset)
        {
            Size = size;
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
            Colour = Color.White;

            Transform = new Transform();
        }

        public BaseObject(Vector2 size, Anchor anchor, int depth, string textureAsset) :
            this(anchor, depth, textureAsset)
        {
            Size = size;
        }

        #region Virtual Functions

        /// <summary>
        /// Check that the texture has been loaded by doing a get call
        /// </summary>
        public override void LoadContent()
        {
            // Check to see whether we should load
            CheckShouldLoad();

            // This assert is useful when debugging, because it checks whether our texture was set properly using lazy evaluation.
            // This is not necessary in a release build, but do not remove - it will save your life
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

            // If our size is zero (i.e. uninitialised) we use the texture's size (if it is not null)
            if (Size == Vector2.Zero && Texture != null)
            {
                Size = new Vector2(Texture.Bounds.Width, Texture.Bounds.Height);
            }

            // Check to see whether we have a non-trivial case for our positioning
            // If we have anchoring set up, we had better have a parent set too
            // The parent of our transform must also be our parent's transform too, otherwise we will be setting an incorrect local offset
            if (Anchor != Anchor.kCentre || Depth != 0)
            {
                DebugUtils.AssertNotNull(Parent);
                //Debug.Assert(Transform.Parent == Parent.Transform);

                if (Anchor.HasFlag(Anchor.kCentre))
                {
                    if (Anchor.HasFlag(Anchor.kLeft) || Anchor.HasFlag(Anchor.kRight))
                    {
                        float xMultiplier = Anchor.HasFlag(Anchor.kLeft) ? -1 : 1;
                        Transform.Position = new Vector2(0.5f * xMultiplier * (Parent.Size.X + Depth * Size.X), 0);
                    }
                    else
                    {
                        float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
                        Transform.Position = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.X + Depth * Size.Y));
                    }
                }
                else
                {
                    float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
                    Transform.Position = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.Y + Depth * Size.Y));

                    if (Anchor.HasFlag(Anchor.kLeft))
                    {
                        Transform.Position -= new Vector2(0.5f * (Parent.Size.X + Depth * Size.X));
                    }
                    else if (Anchor.HasFlag(Anchor.kRight))
                    {
                        Transform.Position += new Vector2(0.5f * (Parent.Size.X + Depth * Size.Y));
                    }
                }
            }

            base.Initialise();
        }

        /// <summary>
        /// If this object has specified to use a collider then we create it here.
        /// </summary>
        public override void Begin()
        {
            base.Begin();

            if (UsesCollider)
            {
                Collider = new RectangleCollider(Transform.Position, Size);
            }
        }

        /// <summary>
        /// Calls HandleInput on the collider if it is being used.
        /// This allows us to check if this object has been clicked, mouse hovered over etc.
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            if (UsesCollider)
            {
                DebugUtils.AssertNotNull(Collider, "The object has 'UsesCollider' as true, but has no Collider initialised");
                Collider.HandleInput(mousePosition);
            }
        }

        /// <summary>
        /// Update the position of our collider using this object's transform
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            if (UsesCollider)
            {
                DebugUtils.AssertNotNull(Collider, "The object has 'UsesCollider' as true, but has no Collider initialised");
                Collider.Position = Transform.Position;
            }
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
            spriteBatch.Draw(
                Texture,
                Transform.Position,
                null,
                SourceRectangle,
                TextureCentre,
                Transform.Rotation,
                Vector2.Divide(Size, TextureDimensions),
                Colour * Opacity,
                SpriteEffect,
                0);
        }
        
        #endregion
    }
}