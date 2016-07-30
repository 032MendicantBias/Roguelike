using Microsoft.Xna.Framework;

namespace RogueLike.ObjectProperties
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(Transform parentTransform, Vector2 position, float rotation, Vector2 scale)
            : this(parentTransform.Position + position, parentTransform.Rotation + rotation, parentTransform.Scale + scale) {}

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Transform(Transform parentTransform) : this(parentTransform, new Vector2(), 0.0f, new Vector2()) {}

        public Transform() : this(new Vector2(), 0.0f, new Vector2()) {}
    }
}
