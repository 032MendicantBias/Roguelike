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
            bool inRangeVert = false;
            bool inRangeHori = false;



            return (inRangeVert && inRangeHori);
        }
    }

    public class Circle
    {
        private Vector2 Position { get; set; }
        private float Radius { get; set; }

        public Circle(Vector2 Position, float Radius)
        {
            this.Position = Position;
            this.Radius = Radius;
        }

        public Vector2 GetCentre()
        {
            return Position;
        }

    }
}
