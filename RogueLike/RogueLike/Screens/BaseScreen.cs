using Microsoft.Xna.Framework;
using RogueLike.CoreObjects;
using RogueLike.Managers;

namespace RogueLike.Screens
{
    public abstract class BaseScreen : Component
    {
        #region Properties and Fields

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
    }
}
