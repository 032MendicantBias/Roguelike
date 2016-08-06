using RogueLike.Physics;
using System.Diagnostics;

namespace RogueLike.Managers
{
    public static class CollisionsManager
    {
        public static bool CheckCollision(ICollidableShape s1, ICollidableShape s2)
        {
            if (s2 is RectangleCollider)
            {
                return s1.CollidedWithRectangle(s2 as RectangleCollider);
            }
            else if (s2 is CircleCollider)
            {
                return s1.CollidedWithCircle(s2 as CircleCollider);
            }
            else
            {
                Debug.Fail("Unhandled collider type");
            }

            return false;
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
