using RogueLike.Physics;

namespace RogueLike.Managers
{
    public static class CollisionsManager
    {
        public static bool CheckCollision(Rectangle rect, Rectangle rect2)
        {
            return rect.CollidedWithRectangle(rect2);
        }
        
        public static bool CheckCollision(Rectangle rect, Circle circ)
        {
            return rect.CollidedWithCircle(circ);
        }

        public static bool CheckCollision(Circle circ, Rectangle rect)
        {
            return circ.CollidedWithRectangle(rect);
        }

        public static bool CheckCollision(Circle circ, Circle circ2)
        {
            return circ.CollidedWithCircle(circ2);
        }
    }
}
