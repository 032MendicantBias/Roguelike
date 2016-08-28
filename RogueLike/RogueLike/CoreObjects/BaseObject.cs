using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Managers;
using RogueLike.ObjectProperties;
using RogueLike.Physics;
using System;
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
    public class BaseObject : Component, IContainer<BaseObject>
    {
        #region Properties and Fields

        /// <summary>
        /// An object manager for the children of this object
        /// </summary>
        protected ObjectManager<BaseObject> Children { get; private set; }

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
        private BaseObject parent;
        public BaseObject Parent
        {
            get
            {
                return parent;
            }
            private set
            {
                parent = value;

                if (value != null)
                {
                    Transform.Parent = parent.Transform;
                }
            }
        }

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
            Children = new ObjectManager<BaseObject>();
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
            Children = new ObjectManager<BaseObject>();
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

            Children.LoadContent();

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
                        Transform.LocalPosition = new Vector2(0.5f * xMultiplier * (Parent.Size.X + Depth * Size.X), 0);
                    }
                    else
                    {
                        float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
                        Transform.LocalPosition = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.X + Depth * Size.Y));
                    }
                }
                else
                {
                    float yMultiplier = Anchor.HasFlag(Anchor.kTop) ? -1 : 1;
                    Transform.LocalPosition = new Vector2(0, 0.5f * yMultiplier * (Parent.Size.Y + Depth * Size.Y));

                    if (Anchor.HasFlag(Anchor.kLeft))
                    {
                        Transform.LocalPosition -= new Vector2(0.5f * (Parent.Size.X + Depth * Size.X));
                    }
                    else if (Anchor.HasFlag(Anchor.kRight))
                    {
                        Transform.LocalPosition += new Vector2(0.5f * (Parent.Size.X + Depth * Size.Y));
                    }
                }
            }

            Children.Initialise();

            base.Initialise();
        }

        /// <summary>
        /// If this object has specified to use a collider then we create it here.
        /// </summary>
        public override void Begin()
        {
            base.Begin();

            Children.Begin();

            if (UsesCollider)
            {
                Collider = new RectangleCollider(Transform.LocalPosition, Size);
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

            if (Children.ShouldHandleInput)
            {
                Children.HandleInput(elapsedGameTime, mousePosition);
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
                Collider.Position = Transform.WorldPosition;
            }

            if (Children.ShouldUpdate)
            {
                Children.Update(elapsedGameTime);
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
                Transform.WorldPosition,
                null,
                SourceRectangle,
                TextureCentre,
                Transform.WorldRotation,
                Vector2.Divide(Size, TextureDimensions),
                Colour * Opacity,
                SpriteEffect,
                0);

            if (Children.ShouldDraw)
            {
                Children.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// We must make sure that we show the children if necessary
        /// </summary>
        /// <param name="showChildren">A flag to indicate whether the children should be shown</param>
        public override void Show(bool showChildren = true)
        {
            base.Show();

            Children.Show(showChildren);
        }

        /// <summary>
        /// We must make sure that we hide the children if necessary
        /// </summary>
        /// <param name="hideChildren">A flag to indicate whether the children should be hidden</param>
        public override void Hide(bool hideChildren = true)
        {
            base.Hide();

            Children.Hide(hideChildren);
        }

        /// <summary>
        /// We must make sure that we explicitly call Die on each child.
        /// The IsAlive attributes are connected up, but this will not result in a call to Die.
        /// </summary>
        public override void Die()
        {
            base.Die();

            Children.Die();
        }

        #endregion

        #region Extra non-virtual IContainer functions

        /// <summary>
        /// A function which will be used to add a child and sets it's parent to this
        /// </summary>
        /// <typeparam name="K">The type of the child</typeparam>
        /// <param name="childToAdd">The child itself</param>
        /// <param name="load">A flag to indicate whether we wish to call LoadContent on the child</param>
        /// <param name="initialise">A flag to indicate whether we wish to call Initialise on the child</param>
        /// <returns></returns>
        public virtual K AddChild<K>(K childToAdd, bool load = false, bool initialise = false) where K : BaseObject
        {
            DebugUtils.AssertNotNull(childToAdd);
            DebugUtils.AssertNull(childToAdd.Parent);

            // Set the parent to be this
            childToAdd.Parent = this;

            return Children.AddChild(childToAdd, load, initialise);
        }

        /// <summary>
        /// A function to remove a child
        /// </summary>
        /// <param name="childToRemove">The child we wish to remove</param>
        public void RemoveChild(BaseObject childToRemove)
        {
            DebugUtils.AssertNotNull(childToRemove);

            // This function will set IsAlive to false so that the object gets cleaned up next Update loop
            Children.RemoveChild(childToRemove);
        }

        /// <summary>
        /// Extracts the inputted child from this container, but keeps it alive for insertion into another
        /// </summary>
        /// <param name="childToExtract">The child we wish to extract from this container</param>
        public T ExtractChild<T>(T childToExtract) where T : BaseObject
        {
            DebugUtils.AssertNotNull(childToExtract);
            childToExtract.Parent = null;

            return Children.ExtractChild(childToExtract);
        }

        /// <summary>
        /// Searches through the children and returns all that match the inputted type
        /// </summary>
        /// <typeparam name="T">The inputted type we will use to find objects</typeparam>
        /// <returns>All the objects we have found of the inputted type</returns>
        public List<T> GetChildrenOfType<T>(bool includeObjectsToAdd = false) where T : BaseObject
        {
            return Children.GetChildrenOfType<T>(includeObjectsToAdd);
        }

        /// <summary>
        /// Finds an object of the inputted name and casts to the inputted type K.
        /// First searches the ActiveObjects and then the ObjectsToAdd
        /// </summary>
        /// <typeparam name="K">The type we wish to return the found object as</typeparam>
        /// <param name="name">The name of the object we wish to find</param>
        /// <returns>Returns the object casted to K or null</returns>
        public K FindChild<K>() where K : BaseObject
        {
            return Children.FindChild<K>();
        }

        /// <summary>
        /// Finds an object of the inputted name and casts to the inputted type T.
        /// First searches the ActiveObjects and then the ObjectsToAdd
        /// </summary>
        /// <typeparam name="T">The type we wish to return the found object as</typeparam>
        /// <param name="predicate">The predicate we will use to find our object</param>
        /// <returns>Returns the object casted to T or null</returns>
        public T FindChild<T>(Predicate<BaseObject> predicate) where T : BaseObject
        {
            return Children.FindChild<T>(predicate);
        }

        /// <summary>
        /// Returns whether an object satisfying the inputted predicate exists within our children.
        /// </summary>
        /// <param name="predicate">The predicate the child must satisfy</param>
        /// <returns>True if such a child exists and false if not</returns>
        public bool Exists(Predicate<BaseObject> predicate)
        {
            return Children.Exists(predicate);
        }

        /// <summary>
        /// Returns the first child.
        /// Shouldn't really be called unless we have children
        /// </summary>
        /// <returns>The first child we added</returns>
        public BaseObject FirstChild()
        {
            return Children.FirstChild();
        }

        /// <summary>
        /// Returns the most recent child we added which is castable to the inputted type.
        /// Shouldn't really be called unless we have children
        /// </summary>
        /// <returns>The most recent child we added</returns>
        public BaseObject LastChild()
        {
            return Children.LastChild();
        }

        #endregion
    }
}