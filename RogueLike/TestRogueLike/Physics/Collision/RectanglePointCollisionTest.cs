using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike.Physics;

namespace TestRogueLike
{
    [TestClass]
    public class RectanglePointCollisionTest
    {
        [TestMethod]
        public void Test_RectanglePointCollision_PointOutside()
        {
            RectangleCollider collider = new RectangleCollider(0, 0, 10, 10);
            Assert.IsFalse(collider.CollidedWithPoint(new Vector2(20, 20)));
        }

        [TestMethod]
        public void Test_RectanglePointCollision_PointInside()
        {
            RectangleCollider collider = new RectangleCollider(0, 0, 10, 10);
            Assert.IsTrue(collider.CollidedWithPoint(new Vector2(-2, 2)));
        }

        [TestMethod]
        public void Test_RectanglePointCollision_PointOnEdge()
        {
            RectangleCollider collider = new RectangleCollider(0, 0, 10, 10);
            Assert.IsTrue(collider.CollidedWithPoint(new Vector2(-5, 5)));
        }
    }
}
