using Microsoft.Xna.Framework;

namespace RogueLike.Physics
{
    public interface ICollidableShape
    {
        Vector2 Position { get; set; }

        bool CollidedWithRectangle(Rectangle rect);
        bool CollidedWithCircle(Circle circ);
    }
}