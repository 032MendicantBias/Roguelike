using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike.Physics;

namespace TestRogueLike
{
    [TestClass]
    public class CircleePointCollisionTest
    {
        [TestMethod]
        public void Test_CirclePointCollision_PointOutside()
        {
            CircleCollider collider = new CircleCollider(0, 0, 10);
            Assert.IsFalse(collider.CollidedWithPoint(new Vector2(20, 20)));
        }

        [TestMethod]
        public void Test_CirclePointCollision_PointInside()
        {
            CircleCollider collider = new CircleCollider(0, 0, 10);
            Assert.IsTrue(collider.CollidedWithPoint(new Vector2(-2, 2)));
        }

        [TestMethod]
        public void Test_CirclePointCollision_PointOnEdge()
        {
            CircleCollider collider = new CircleCollider(0, 0, 10);
            Assert.IsTrue(collider.CollidedWithPoint(new Vector2(10, 0)));
        }
    }
}
