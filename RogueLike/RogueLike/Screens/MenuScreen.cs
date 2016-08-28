using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueLike.Input;

namespace RogueLike.Screens
{
    /// <summary>
    /// A fairly basic class that sets up transitioning between menus.
    /// </summary>
    public abstract class MenuScreen : BaseScreen
    {
        #region Virtual Functions

        /// <summary>
        /// Handle input and check whether we are transitioning to the previous screen if it exists (press escape)
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            if (GameKeyboard.IsKeyPressed(Keys.Escape))
            {
                GoToPreviousScreen();
            }
        }

        /// <summary>
        /// An overridable function used to traverse between menu screens.
        /// </summary>
        /// <returns>The type of the previous screen we wish to go back to</returns>
        protected virtual void GoToPreviousScreen() { }

        #endregion
    }
}
