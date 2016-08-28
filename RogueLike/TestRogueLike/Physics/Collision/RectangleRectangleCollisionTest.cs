using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.Physics;

namespace TestRogueLike
{
    [TestClass]
    public class RectangleRectangleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(100, 100, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(100, 0, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(0, 100, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestInRange()
        {
            RectangleCollider rectA = new RectangleCollider(0, 10, 10, 10);
            RectangleCollider rectB = new RectangleCollider(0, 5, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedVert()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(0, 10, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedHori()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(10, 0, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestCornerHit()
        {
            RectangleCollider rectA = new RectangleCollider(0, 0, 10, 10);
            RectangleCollider rectB = new RectangleCollider(10, 10, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }
    }
}
