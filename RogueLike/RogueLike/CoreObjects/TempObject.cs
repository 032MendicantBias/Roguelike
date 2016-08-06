using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueLike.CoreObjects
{
    /// <summary>
    /// This class will get thrown away - just used for testing
    /// </summary>
    public class TempObject : BaseObject
    {
        public TempObject(Vector2 position) :
            base(position, "TestImage")
        {
            UsesCollider = true;
        }

        public override void HandleInput(float elapsedGameTime, Vector2 mousePosition)
        {
            base.HandleInput(elapsedGameTime, mousePosition);

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Transform.Position += new Vector2(5, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Transform.Position += new Vector2(-5, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Transform.Position += new Vector2(0, -5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Transform.Position += new Vector2(0, 5);
            }
        }
    }
}