using Microsoft.Xna.Framework;

namespace TestRogueLike
{
    public static class ExtensionFunctions
    {
        public static Vector2 GetCentre(this Rectangle rect)
        {
            return rect.Center.ToVector2();
        }
    }
}
