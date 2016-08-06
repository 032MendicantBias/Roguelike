using Microsoft.Xna.Framework;
using RogueLike.CoreObjects;

namespace RogueLike.Screens
{
    /// <summary>
    /// Throwaway screen used for testings
    /// </summary>
    public class TempScreen : BaseScreen
    {
        public TempScreen()
        {
            AddScreenObject(new TempObject(new Vector2(500, 500)));
            TempObject temp = AddScreenObject(new TempObject(new Vector2(400, 600)));
            temp.ShouldHandleInput = false;
        }
    }
}
