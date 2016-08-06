using RogueLike.Physics;

namespace RogueLike.Managers
{
    public static class CollisionsManager
    {
        public static bool CheckCollision(ICollidableShape s1, ICollidableShape s2)
        {
            if(s1 is Rectangle)
            {
                if(s2 is Rectangle)
                {
                    return CheckCollision(s1 as Rectangle, s2 as Rectangle);
                }
                else if(s2 is Circle)
                {
                    return CheckCollision(s1 as Rectangle, s2 as Circle);
                }
            }
            else if(s1 is Circle)
            {
                if (s2 is Rectangle)
                {
                    return CheckCollision(s1 as Circle, s2 as Rectangle);
                }
                else if(s2 is Circle)
                {
                    return CheckCollision(s1 as Circle, s2 as Circle);
                }
            }

            return false;
        }
        
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
