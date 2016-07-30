using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.Managers;
using RogueLike.Screens;
using System.Diagnostics;

namespace RogueLine.Managers
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

        #region Utility Functions

        /// <summary>
        /// Sets up class variables from the main Game1 class which will be useful for our game.
        /// Loads options from XML.
        /// MUST be called before LoadContent and Initialise.
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

        #endregion
    }
}
