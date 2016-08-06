using Microsoft.Xna.Framework;
using System;

namespace RogueLike.Physics
{
    public class CircleCollider : ICollidableShape
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public CircleCollider(Vector2 pos, float radius)
        {
            Position = pos;
            Radius = radius;
        }

        public CircleCollider(float x, float y, float radius)
        {
            Position = new Vector2(x, y);
            Radius = radius;
        }

        public bool CollidedWithRectangle(RectangleCollider rect)
        {
            Vector2 positionDiff = Vector2.Subtract(rect.GetCentre(), this.Position);
            positionDiff.X = Math.Abs(positionDiff.X);
            positionDiff.Y = Math.Abs(positionDiff.Y);

            if (positionDiff.X > (rect.Width / 2) + this.Radius) { return false; }
            if (positionDiff.Y > (rect.Height / 2) + this.Radius) { return false; }

            if (positionDiff.X <= (rect.Width / 2)) { return true; }
            if (positionDiff.Y <= (rect.Height / 2)) { return true; }

            float cornerDistSquared = (float)Math.Pow((positionDiff.X - (rect.Width / 2)), 2)
                + (float)Math.Pow((positionDiff.Y - (rect.Height / 2)), 2);

            return cornerDistSquared <= (float)Math.Pow(this.Radius, 2);
        }

        public bool CollidedWithCircle(CircleCollider circ)
        {
            Vector2 positionDiff = Vector2.Subtract(this.Position, circ.Position);
            return positionDiff.Length() <= (this.Radius + circ.Radius);
        }

    }
}
