using Microsoft.Xna.Framework;
using System;

namespace RogueLike.Physics
{
    public class PhysicsObject
    {
        private Rectangle rect;

        public PhysicsObject(Rectangle rect) { this.rect = rect; }

        public bool CollidedRectangle(Rectangle rectIn)
        {
            bool inRangeVert = false;
            bool inRangeHori = false;

            Vector2 positionDiff = Vector2.Subtract(rect.GetCentre(), rectIn.GetCentre());

            if (Math.Abs(positionDiff.X) <= (rect.Width / 2) + (rectIn.Width / 2))
            {
                inRangeHori = true;
            }

            if (Math.Abs(positionDiff.Y) <= (rect.Height / 2) + (rectIn.Height / 2))
            {
                inRangeVert = true;
            }

            return (inRangeVert && inRangeHori);
        }

        public bool CollidedCircle(Circle circ)
        {
            Vector2 positionDiff = Vector2.Subtract(rect.GetCentre(), circ.Position);
            positionDiff.X = Math.Abs(positionDiff.X);
            positionDiff.Y = Math.Abs(positionDiff.Y);

            if (positionDiff.X > (rect.Width / 2) + circ.Radius) { return false; }
            if (positionDiff.Y > (rect.Height / 2) + circ.Radius) { return false; }

            if (positionDiff.X <= (rect.Width / 2)) { return true; }
            if (positionDiff.Y <= (rect.Height / 2)) { return true; }

            float cornerDistSquared = (float)Math.Pow((positionDiff.X - (rect.Width / 2)), 2)
                + (float)Math.Pow((positionDiff.Y - (rect.Height / 2)), 2);

            return cornerDistSquared <= (float) Math.Pow(circ.Radius, 2);
        }
    }


    public class Circle
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 Position, float Radius)
        {
            this.Position = Position;
            this.Radius = Radius;
        }

    }
}
