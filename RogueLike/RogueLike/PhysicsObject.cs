using Microsoft.Xna.Framework;
using System;

namespace RogueLike.Physics
{
    class PhysicsObject
    {  
        private float width { get; set; }
        private float height { get; set; }
        private Vector2 position { get; set; }

        private bool CollidedRectangle(Rectangle rect)
        {
            bool inRangeVert = false;
            bool inRangeHori = false;

            Vector2 positionDiff = Vector2.Subtract(this.position, rect.);

            if (Math.Abs(positionDiff.X) < this.width + rect.Width)
                inRangeHori = true;

            if (Math.Abs(positionDiff.Y) < this.height + rect.Height)
                inRangeVert = true;

            return (inRangeVert && inRangeHori);
        }
    }
}
