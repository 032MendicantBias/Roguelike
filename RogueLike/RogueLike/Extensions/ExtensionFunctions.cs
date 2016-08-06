using Microsoft.Xna.Framework;

namespace RogueLike
{
    public static class ExtensionFunctions
    {
        public static Vector2 GetCentre(this Rectangle rect)
        {
            return rect.Center.ToVector2();
        }
    }
}
