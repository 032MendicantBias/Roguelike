using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RogueLike.Input;
using RogueLike.Screens;
using System.Diagnostics;

namespace RogueLike.Managers
{
    /// <summary>
    /// A singleton class which is responsible for managing the screens in game
    /// </summary>
    public class ScreenManager : ObjectManager<BaseScreen>
    {
        #region Properties and Fields

        /// <summary>
        /// We will only have one instance of this class, so have a static Instance which can be accessed
        /// anywhere in the program by calling ScreenManager.Instance
        /// </summary>
        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// A reference to our game.  Used really for exiting.
        /// </summary>
        private Game Game { get; set; }

        /// <summary>
        /// The SpriteBatch we will use for all our rendering.
        /// It will be used in three rendering loops:
        /// Game Objects - objects we want to be affected by our camera position
        /// In Game UI Objects - UI objects we want to be affected by our camera position
        /// Screen UI Objects - UI Objects we want to be independent of our camera position
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// The device we can use to load content.
        /// Not really used as we use the AssetManager to obtain all of our Content instead.
        /// </summary>
        public ContentManager Content { get; private set; }

        /// <summary>
        /// The Graphics Device we can use to change display settings.
        /// Not really used except at startup and during Options changes.
        /// </summary>
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        /// <summary>
        /// The Viewport for our game window - can be used to access screen dimensions
        /// </summary>
        public Viewport Viewport { get; private set; }

        /// <summary>
        /// A wrapper property to return the Viewport's width and height
        /// </summary>
        public Vector2 ScreenDimensions { get; private set; }

        /// <summary>
        /// A wrapper property to return the centre of the screen
        /// </summary>
        public Vector2 ScreenCentre { get; private set; }

        /// <summary>
        /// A reference to the last child in the manager.
        /// </summary>
        public BaseScreen CurrentScreen
        {
            get
            {
                return LastChild();
            }
        }

        #endregion

        /// <summary>
        /// Constructor is private because this class will be accessed through the static 'Instance' property
        /// </summary>
        private ScreenManager() :
            base()
        {
        }

        #region Virtual Functions

        /// <summary>
        /// Loads any instance objects like the mouse before the game begins
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            GameMouse.Instance.LoadContent();
        }

        /// <summary>
        /// Calls Initialise on any instance objects like the mouse before the game begins
        /// </summary>
        public override void Initialise()
        {
            base.Initialise();

            GameMouse.Instance.Initialise();
        }

        /// <summary>
        /// Call HandleInput and Update on the mouse before anything else so that the other objects in the game have the most up to date information.
        /// Obtains information about the current Keyboard state.
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 ignoreThis)
        {
            // Handle the mouse input and update it first before we do anything for the other objects in our game, so they have the most up to date information
            GameMouse.Instance.HandleInput(elapsedGameTime, Vector2.Zero);
            GameMouse.Instance.Update(elapsedGameTime);     // This will also call GameMouse.Instance.Begin() before our ScreenManager's Begin function.

            base.HandleInput(elapsedGameTime, GameMouse.Instance.Transform.LocalPosition);

            // Update the state of the keyboard for this frame so we can work out what keys have been pressed
            GameKeyboard.HandleInput();
        }

        /// <summary>
        /// Draw our screens and then the mouse on top.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            GameMouse.Instance.Draw(spriteBatch);
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Sets up class variables from the main Game1 class which will be useful for our game.
        /// Loads options from XML.
        /// Calls LoadContent and Initialise.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch from our Game1 class</param>
        /// <param name="viewport">The Viewport corresponding to the window</param>
        public void Setup(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            // Check that we have called this before loading and initialising
            CheckShouldLoad();
            CheckShouldInitialise();

            Game = game;
            SpriteBatch = spriteBatch;
            Content = game.Content;
            Viewport = game.GraphicsDevice.Viewport;
            GraphicsDeviceManager = graphics;

            ScreenDimensions = new Vector2(Viewport.Width, Viewport.Height);
            ScreenCentre = ScreenDimensions * 0.5f;

            // Set our game to update on a fixed time step
            Game.IsFixedTimeStep = true;

            // I don't think this should stay here, but I'm putting it here as a hacky fix right now
            AssetManager.LoadAssets(Content);

            LoadContent();
            Initialise();
        }

        /// <summary>
        /// Remove one screen and add another.
        /// </summary>
        /// <param name="transitionFrom">The screen to remove</param>
        /// <param name="transitionTo">The screen to add</param>
        /// <param name="load">Whether we should call LoadContent on the screen to add</param>
        /// <param name="initialise">Whether we should call Initialise on the screen to add</param>
        /// <returns>The screen we are adding next</returns>
        public BaseScreen Transition(BaseScreen transitionFrom, BaseScreen transitionTo, bool load = true, bool initialise = true)
        {
            BaseScreen newScreen = AddChild(transitionTo, load, initialise);

            transitionFrom.Die();
            transitionFrom.ShouldDraw = true;     // Bit of a hack so that we get a continuous draw until the new screen takes over

            return newScreen;
        }

        /// <summary>
        /// A wrapper for returning the current screen as the inputted type.
        /// Performs Debug validity checking.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCurrentScreenAs<T>() where T : BaseScreen
        {
            Debug.Assert(CurrentScreen is T);
            return CurrentScreen as T;
        }

        /// <summary>
        /// Ends the game, but calls any end game events before it does.
        /// </summary>
        public void EndGame()
        {
            Game.Exit();
        }

        #endregion
    }
}
