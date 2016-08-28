using Microsoft.Xna.Framework;
using System;

namespace RogueLike.Physics
{
    public class CircleCollider : ICollidableShape
    {
        #region Properties and Fields

        public float Radius { get; set; }

        #endregion

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

        #region Collision Functions

        public override bool CollidedWithRectangle(RectangleCollider rect)
        {
            Vector2 positionDiff = Vector2.Subtract(rect.GetCentre(), Position);
            positionDiff.X = Math.Abs(positionDiff.X);
            positionDiff.Y = Math.Abs(positionDiff.Y);

            if (positionDiff.X > (rect.Width / 2) + Radius) { return false; }
            if (positionDiff.Y > (rect.Height / 2) + Radius) { return false; }

            if (positionDiff.X <= (rect.Width / 2)) { return true; }
            if (positionDiff.Y <= (rect.Height / 2)) { return true; }

            float xDiff = (positionDiff.X - (rect.Width / 2));
            float yDiff = (positionDiff.Y - (rect.Height / 2));

            float cornerDistSquared = xDiff * xDiff + yDiff * yDiff;

            return cornerDistSquared <= Radius * Radius;
        }

        public override bool CollidedWithCircle(CircleCollider circ)
        {
            Vector2 positionDiff = Position - circ.Position;
            return positionDiff.LengthSquared() <= (Radius + circ.Radius) * (Radius + circ.Radius);
        }

        public override bool CollidedWithPoint(Vector2 point)
        {
            Vector2 positionDiff = Position - point;
            return positionDiff.LengthSquared() <= Radius * Radius;
        }

        #endregion
    }
}
