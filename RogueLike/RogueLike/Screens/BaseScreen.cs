using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.CoreObjects;
using RogueLike.Managers;

namespace RogueLike.Screens
{
    public abstract class BaseScreen : Component
    {
        #region Properties and Fields

        /// <summary>
        /// A manager class for the screen objects - this is wildly incomplete as it provides no separation for camera dependency/independency
        /// </summary>
        private ObjectManager<BaseObject> ScreenObjects { get; set; }

        /// <summary>
        /// Returns the dimensions of the game window
        /// </summary>
        protected Vector2 ScreenDimensions
        {
            get { return ScreenManager.Instance.ScreenDimensions; }
        }

        /// <summary>
        /// Returns the centre of the game window
        /// </summary>
        protected Vector2 ScreenCentre
        {
            get { return ScreenManager.Instance.ScreenCentre; }
        }

        #endregion

        public BaseScreen()
        {
            ScreenObjects = new ObjectManager<BaseObject>();
        }

        #region Virtual Functions

        /// <summary>
        /// Load the content of all the objects in this screen
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            ScreenObjects.LoadContent();
        }

        /// <summary>
        /// Initialise all of the objects in this screen
        /// </summary>
        public override void Initialise()
        {
            base.Initialise();

            ScreenObjects.Initialise();
        }

        /// <summary>
        /// Make sure that all objects in this screen have their 'Begun' function called and have been added to the object manager.
        /// </summary>
        public override void Begin()
        {
            base.Begin();

            ScreenObjects.Begin();
        }

        /// <summary>
        /// Handle input for all of the objects in our screen
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            ScreenObjects.HandleInput(elapsedGameTime, mousePosition);
        }

        /// <summary>
        /// Update all of the objects in our screen
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            ScreenObjects.Update(elapsedGameTime);
        }

        /// <summary>
        /// Draw all of the objects in our screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // Need to add camera dependency/independency
            // Think about render order
            ScreenObjects.Draw(spriteBatch);
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Calls the ScreenManager Transition function.  Moves from the current screen to the inputted screen and disposes the current screen.
        /// </summary>
        /// <param name="screenToTransitionTo">The screen to transition to</param>
        /// <param name="load">Whether we should call LoadContent on the screen</param>
        /// <param name="initialise">Whether we should call Initialise on the screen</param>
        /// <returns>The new screen we are adding</returns>
        public BaseScreen Transition(BaseScreen screenToTransitionTo, bool load = true, bool initialise = true)
        {
            return ScreenManager.Instance.Transition(this, screenToTransitionTo, load, initialise);
        }

        #endregion

        #region Object Management Functions

        protected T AddScreenObject<T>(T objectToAdd, bool load = false, bool initialise = false) where T : BaseObject
        {
            return ScreenObjects.AddChild(objectToAdd, load, initialise);
        }

        #endregion
    }
}