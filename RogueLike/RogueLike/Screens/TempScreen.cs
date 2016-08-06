using Microsoft.Xna.Framework;
using RogueLike.CoreObjects;
using RogueLike.Managers;

namespace RogueLike.Screens
{
    /// <summary>
    /// Throwaway screen used for testings
    /// </summary>
    public class TempScreen : BaseScreen
    {
        private TempObject temp1, temp2;

        public TempScreen()
        {
            temp1 = AddScreenObject(new TempObject(new Vector2(500, 500)));
            temp2 = AddScreenObject(new TempObject(new Vector2(400, 600)));
            temp2.ShouldHandleInput = false;
        }

        public override void Update(float elapsedGameTime)
        {
            base.Update(elapsedGameTime);

            CollisionsManager.CheckCollision(temp1.Collider, temp2.Collider);
        }
    }
}
