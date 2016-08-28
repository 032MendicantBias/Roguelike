using Microsoft.Xna.Framework;
using RogueLike.UI;

namespace RogueLike.Screens
{
    /// <summary>
    /// The main entry point for our game with buttons for quitting, options, loading and playing.
    /// </summary>
    public class MainMenuScreen : MenuScreen
    {
        #region Virtual Functions

        /// <summary>
        /// Quick and dirty addition of some buttons for testing
        /// </summary>
        public override void LoadContent()
        {
            CheckShouldLoad();

            Button playButton = AddScreenObject(new Button(new Vector2(ScreenCentre.X, ScreenDimensions.Y * 0.25f)));
            playButton.OnClick += delegate { };

            Button exitButton = AddScreenObject(new Button(new Vector2(ScreenCentre.X, ScreenDimensions.Y * 0.75f)));
            exitButton.OnClick += delegate { };

            base.LoadContent();
        }

        #endregion
    }
}
