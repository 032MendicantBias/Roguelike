using RogueLike.Physics;

namespace RogueLike.Managers
{
    public static class CollisionsManager
    {
        public static bool CheckCollision(ICollidableShape s1, ICollidableShape s2)
        {
            return s1.CollidedWithCollider(s2);
        }
        
        public static bool CheckCollision(RectangleCollider rect, RectangleCollider rect2)
        {
            return rect.CollidedWithRectangle(rect2);
        }
        
        public static bool CheckCollision(RectangleCollider rect, CircleCollider circ)
        {
            return rect.CollidedWithCircle(circ);
        }

        public static bool CheckCollision(CircleCollider circ, RectangleCollider rect)
        {
            return circ.CollidedWithRectangle(rect);
        }

        public static bool CheckCollision(CircleCollider circ, CircleCollider circ2)
        {
            return circ.CollidedWithCircle(circ2);
        }
    }
}
