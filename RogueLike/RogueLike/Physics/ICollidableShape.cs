using Microsoft.Xna.Framework;

namespace RogueLike.Physics.Collisions
{
    public interface ICollidableShape
    {
        Vector2 Position { get; set; }

        bool CollidedWithRectangle(Rektangle rect);
        bool CollidedWithCircle(Circle circ);
    }
}