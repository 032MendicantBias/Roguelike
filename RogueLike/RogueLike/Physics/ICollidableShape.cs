using Microsoft.Xna.Framework;

namespace RogueLike.Physics
{
    public interface ICollidableShape
    {
        Vector2 Position { get; set; }

        bool CollidedWithRectangle(RectangleCollider rect);
        bool CollidedWithCircle(CircleCollider circ);
    }
}