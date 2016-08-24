using RogueLike.Physics.Collisions;
using System.Diagnostics;

namespace RogueLike.Managers
{
    public static class CollisionsManager
    {
        public static bool CheckCollision(ICollidableShape s1, ICollidableShape s2)
        {
            if (s2 is Rektangle)
            {
                return s1.CollidedWithRectangle(s2 as Rektangle);
            }
            else if (s2 is Circle)
            {
                return s1.CollidedWithCircle(s2 as Circle);
            }
            else
            {
                Debug.Fail("Unhandled collider type");
            }

            return false;
        }
        
        public static bool CheckCollision(Rektangle rect, Rektangle rect2)
        {
            return rect.CollidedWithRectangle(rect2);
        }
        
        public static bool CheckCollision(Rektangle rect, Circle circ)
        {
            return rect.CollidedWithCircle(circ);
        }

        public static bool CheckCollision(Circle circ, Rektangle rect)
        {
            return circ.CollidedWithRectangle(rect);
        }

        public static bool CheckCollision(Circle circ, Circle circ2)
        {
            return circ.CollidedWithCircle(circ2);
        }
    }
}
