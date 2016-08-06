namespace RogueLike.Physics
{
    public interface ICollidableShape
    {
        bool CollidedWithRectangle(Rectangle rect);
        bool CollidedWithCircle(Circle circ);
    }
}