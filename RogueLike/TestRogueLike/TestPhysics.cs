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

    [TestClass]
    public class RectangleCircleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(100, 100, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(0, 100, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(100, 0, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTop()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(5, -5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottom()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(5, 15, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionLeft()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-5, 5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionRight()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(15, 5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionInside()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(5, 5, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopLeft()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-5, -5, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopLeft()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-1, -1, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomLeft()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-5, 15, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomLeft()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-1, 11, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopRight()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(15, -5, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopRight()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(11, -1, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomRight()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(-15, 15, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomRight()
        {
            RectangleCollider rect = new RectangleCollider(0, 0, 10, 10);
            CircleCollider circ = new CircleCollider(11, 11, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }
    }
}
