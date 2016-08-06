using Microsoft.Xna.Framework;
using System;

namespace RogueLike.Physics
{
    public class RectangleCollider : ICollidableShape
    {
        public Vector2 Position { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public RectangleCollider(float x, float y, float width, float height)
        {
            Position = new Vector2(x, y);
            Width = width;
            Height = height;
        }

        public RectangleCollider(Vector2 pos, float width, float height)
        {
            Position = pos;
            Width = width;
            Height = height;
        }
        
        public RectangleCollider(Vector2 pos, Vector2 size)
        {
            Position = pos;
            Width = size.X;
            Height = size.Y;
        }

        public Vector2 Left
        {
            get
            {
                return new Vector2(Position.X, Position.Y + (Height / 2));
            }
        }

        public Vector2 Right
        {
            get
            {
                return new Vector2(Position.X + Width, Position.Y + (Height / 2));
            }
        }

        public Vector2 Top
        {
            get
            {
                return new Vector2(Position.X + (Width / 2), Position.Y);
            }
        }

        public Vector2 Bottom
        {
            get
            {
                return new Vector2(Position.X + (Width / 2), Position.Y + Height);
            }
        }

        public Vector2 GetCentre()
        {
            return new Vector2(Position.X + (Width / 2), Position.Y + (Height / 2));
        }

        public bool CollidedWithRectangle(RectangleCollider rect)
        {
            bool inRangeVert = false;
            bool inRangeHori = false;

            Vector2 positionDiff = Vector2.Subtract(this.GetCentre(), rect.GetCentre());

            if (Math.Abs(positionDiff.X) <= (rect.Width / 2) + (rect.Width / 2))
            {
                inRangeHori = true;
            }

            if (Math.Abs(positionDiff.Y) <= (rect.Height / 2) + (rect.Height / 2))
            {
                inRangeVert = true;
            }

            return (inRangeVert && inRangeHori);
        }

        public bool CollidedWithCircle(CircleCollider circ)
        {
            Vector2 positionDiff = Vector2.Subtract(this.GetCentre(), circ.Position);
            positionDiff.X = Math.Abs(positionDiff.X);
            positionDiff.Y = Math.Abs(positionDiff.Y);

            if (positionDiff.X > (this.Width / 2) + circ.Radius) { return false; }
            if (positionDiff.Y > (this.Height / 2) + circ.Radius) { return false; }

            if (positionDiff.X <= (this.Width / 2)) { return true; }
            if (positionDiff.Y <= (this.Height / 2)) { return true; }

            float cornerDistSquared = (float)Math.Pow((positionDiff.X - (this.Width / 2)), 2)
                + (float)Math.Pow((positionDiff.Y - (this.Height / 2)), 2);

            return cornerDistSquared <= (float) Math.Pow(circ.Radius, 2);
        }
    }
}
