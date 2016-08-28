using Microsoft.Xna.Framework;
using RogueLike.Input;
using System.Diagnostics;

namespace RogueLike.Physics
{
    public abstract class ICollidableShape
    {
        #region Properties and Fields

        public Vector2 Position { get; set; }

        /// <summary>
        /// A flag to indicate whether we have been clicked on.
        /// Updated on a per frame basis.
        /// </summary>
        public bool IsClicked { get; private set; }

        /// <summary>
        /// A flag to indicate whether we have been pressed on.
        /// Updated on a per frame basis.
        /// </summary>
        public bool IsPressed { get; private set; }

        /// <summary>
        /// A flag to indicate whether our mouse is over the object
        /// </summary>
        public bool IsMouseOver { get; private set; }

        /// <summary>
        /// A flag to indicate that the mouse was not over this object last frame, but is this frame.
        /// True if we have gone from mouse not over to mouse over this frame, otherwise false
        /// </summary>
        public bool IsEntered { get; private set; }

        /// <summary>
        /// A flag to indicate that the mouse was over this object last frame, but is not this frame.
        /// True if we have gone from mouse over to not mouse over this frame, otherwise false
        /// </summary>
        public bool IsExited { get; private set; }

        /// <summary>
        /// A flag which represents a click toggle.
        /// Set to true if the mouse clicks on the object and only set to false when the mouse clicks again and it is either not on this object, or on this object which is already selected.
        /// </summary>
        public bool IsSelected { get; private set; }

        #endregion

        #region Collision Utility Functions

        public abstract bool CollidedWithRectangle(RectangleCollider rect);
        public abstract bool CollidedWithCircle(CircleCollider circ);
        public abstract bool CollidedWithPoint(Vector2 point);

        public bool CollidedWithCollider(ICollidableShape collider)
        {
            if (collider is CircleCollider)
            {
                return CollidedWithCircle(collider as CircleCollider);
            }
            
            if (collider is RectangleCollider)
            {
                return CollidedWithRectangle(collider as RectangleCollider);
            }

            Debug.Fail("Collider not implemented in method 'CollidedWithCollider'.  Please implement");
            return false;
        }

        #endregion

        #region Collider Update Functions

        /// <summary>
        /// Updates the mouse specific flags on this collider using the inputted position of the mouse
        /// </summary>
        /// <param name="mousePosition"></param>
        public void HandleInput(Vector2 mousePosition)
        {
            // If the mouse position and this have collided the mouse is over it
            bool mouseOverLastFrame = IsMouseOver;

            IsMouseOver = CollidedWithPoint(mousePosition);
            IsEntered = IsMouseOver && !mouseOverLastFrame;
            IsExited = !IsMouseOver && mouseOverLastFrame;

            bool mouseClicked = GameMouse.Instance.IsClicked(MouseButton.kLeftButton) ||
                                GameMouse.Instance.IsClicked(MouseButton.kMiddleButton) ||
                                GameMouse.Instance.IsClicked(MouseButton.kRightButton);

            if (mouseClicked)
            {
                IsSelected = IsMouseOver && !IsSelected;       // If the mouse has been clicked over our object and we are not already selected we are now selected
                IsClicked = IsMouseOver;                       // If the mouse has been clicked we toggle the click flag using whether the mouse is over the collider
            }
            else
            {
                IsClicked = false;              // If the mouse has not been clicked we cannot have been clicked
            }

            // If the mouse is over this and a mouse button is down, the object is pressed
            if (IsMouseOver && (GameMouse.Instance.IsDown(MouseButton.kLeftButton) ||
                                GameMouse.Instance.IsDown(MouseButton.kMiddleButton) ||
                                GameMouse.Instance.IsDown(MouseButton.kRightButton)))
            {
                IsPressed = true;
            }
            else
            {
                IsPressed = false;
            }
        }

        #endregion
    }
}