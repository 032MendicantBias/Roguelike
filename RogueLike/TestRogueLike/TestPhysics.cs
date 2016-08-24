using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.Physics.Collisions;

namespace TestRogueLike
{

    [TestClass]
    public class RectangleRectangleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(100, 100, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(100, 0, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(0, 100, 10, 10);
            Assert.IsFalse(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestInRange()
        {
            Rektangle rectA = new Rektangle(0, 10, 10, 10);
            Rektangle rectB = new Rektangle(0, 5, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedVert()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(0, 10, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedHori()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(10, 0, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }

        [TestMethod]
        public void TestCornerHit()
        {
            Rektangle rectA = new Rektangle(0, 0, 10, 10);
            Rektangle rectB = new Rektangle(10, 10, 10, 10);
            Assert.IsTrue(rectA.CollidedWithRectangle(rectB));
        }
    }

    [TestClass]
    public class RectangleCircleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(100, 100, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(0, 100, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(100, 0, 10);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTop()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(5, -5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottom()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(5, 15, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionLeft()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-5, 5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionRight()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(15, 5, 10);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionInside()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(5, 5, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopLeft()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-5, -5, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopLeft()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-1, -1, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomLeft()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-5, 15, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomLeft()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-1, 11, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopRight()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(15, -5, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopRight()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(11, -1, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomRight()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(-15, 15, 5);
            Assert.IsFalse(rect.CollidedWithCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomRight()
        {
            Rektangle rect = new Rektangle(0, 0, 10, 10);
            Circle circ = new Circle(11, 11, 5);
            Assert.IsTrue(rect.CollidedWithCircle(circ));
        }
    }
}
