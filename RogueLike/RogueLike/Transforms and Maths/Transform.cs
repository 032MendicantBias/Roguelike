using Microsoft.Xna.Framework;

namespace RogueLike.ObjectProperties
{
    public class Transform
    {
        #region Properties and Fields

        public Vector2 LocalPosition { get; set; }
        public float LocalRotation { get; set; }
        public Vector2 Scale { get; set; }
        public Transform Parent { private get; set; }

        /// <summary>
        /// The world space position of this transform
        /// </summary>
        public Vector2 WorldPosition
        {
            get
            {
                if (Parent == null)
                {
                    return LocalPosition;
                }
                else
                {
                    // This syntax is for optimisation
                    return Vector2.Add(Parent.WorldPosition, Vector2.Transform(LocalPosition, Matrix.CreateRotationZ(WorldRotation)));
                }
            }
        }

        /// <summary>
        /// The world space rotation, calculated recursively using the parent's WorldRotation.
        /// This value will be between -PI and PI
        /// </summary>
        public float WorldRotation
        {
            get
            {
                // If we have no parent, return the local rotation
                if (Parent == null)
                {
                    return LocalRotation;
                }

                // Wrap the angle between -PI and PI
                return MathHelper.WrapAngle(Parent.WorldRotation + LocalRotation);
            }
        }

        #endregion

        public Transform(Transform parentTransform, Vector2 localPosition, float localRotation, Vector2 scale)
            : this(localPosition, localRotation, scale)
        {
            Parent = parentTransform;
        }

        public Transform(Vector2 position, float rotation, Vector2 scale)
        {
            LocalPosition = position;
            LocalRotation = rotation;
            Scale = scale;
        }

        public Transform(Transform parentTransform) : this(parentTransform, Vector2.Zero, 0.0f, Vector2.Zero)
        {
            Parent = parentTransform;
        }

        public Transform() : this(Vector2.Zero, 0.0f, Vector2.Zero) {}
    }
}
