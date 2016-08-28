using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RogueLike.CoreObjects;
using System.Diagnostics;

namespace RogueLike.UI
{
    /// <summary>
    /// A class representing a button UI which has a click event that can be subscribed to to perform behaviour when clicked.
    /// </summary>
    public class Button : BaseObject
    {
        #region Properties and Fields

        /// <summary>
        /// The texture asset for the button texture asset we will draw when the mouse is over it
        /// </summary>
        private string HighlightedTextureAsset { get; set; }

        /// <summary>
        /// The texture that will be drawn when this button has the mouse over it (including pressed)
        /// </summary>
        private Texture2D HighlightedTexture { get; set; }

        /// <summary>
        /// The texture that will be drawn when this button is idle
        /// </summary>
        private Texture2D NormalTexture { get; set; }

        /// <summary>
        /// A reference to the Label UI this button has drawn on it.
        /// </summary>
        public Label Label { get; private set; }

        /// <summary>
        /// The event that is fired when the button is clicked.
        /// Subscribe to this event to perform custom behaviour when the button is clicked.
        /// </summary>
        public event OnClickHandler OnClick;

        #endregion

        public Button(string text,
                      Vector2 localPosition, 
                      string buttonTextureAsset = AssetManager.DefaultButtonTextureAsset, 
                      string buttonHighlightedTextureAsset = AssetManager.DefaultHighlightedButtonTextureAsset) :
            base(localPosition, buttonTextureAsset)
        {
            HighlightedTextureAsset = buttonHighlightedTextureAsset;
            UsesCollider = true;    // The button will require a collider 

            Label = AddChild(new Label(text, Vector2.Zero));
        }

        public Button(string text,
                      Anchor anchor,
                      int depth,
                      string buttonTextureAsset = AssetManager.DefaultButtonTextureAsset,
                      string buttonHighlightedTextureAsset = AssetManager.DefaultHighlightedButtonTextureAsset) :
            base(anchor, depth, buttonTextureAsset)
        {
            HighlightedTextureAsset = buttonHighlightedTextureAsset;
            UsesCollider = true;    // The button will require a collider 

            Label = AddChild(new Label(text, Vector2.Zero));
        }

        #region Virtual Functions

        /// <summary>
        /// Loads the highlighted texture asset
        /// </summary>
        public override void LoadContent()
        {
            CheckShouldLoad();

            Debug.Assert(!string.IsNullOrEmpty(HighlightedTextureAsset));
            HighlightedTexture = AssetManager.GetSprite(HighlightedTextureAsset);

            Debug.Assert(!string.IsNullOrEmpty(TextureAsset));
            NormalTexture = AssetManager.GetSprite(TextureAsset);

            base.LoadContent();
        }

        /// <summary>
        /// Checks to see if the button has been clicked and if it has, fires the event.
        /// Also updates the texture of the button based on whether the mouse is over it.
        /// </summary>
        /// <param name="elapsedGameTime"></param>
        /// <param name="mousePosition"></param>
        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            if (Collider.IsMouseOver)
            {
                // If the mouse is over the button, change the texture to the highlighted texture
                Texture = HighlightedTexture;

                if (Collider.IsClicked)
                {
                    // If we have been clicked, fire the event
                    DebugUtils.AssertNotNull(OnClick, "This button has no click events subscribed to it, so nothing will happen when clicked");
                    OnClick?.Invoke(this);
                }
            }
            else
            {
                // If the mouse is not over the button, just set the texture to be the normal texture
                Texture = NormalTexture;
            }
        }

        #endregion
    }
}