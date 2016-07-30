using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike.Physics;

namespace TestRogueLike
{

    [TestClass]
    public class PhysicsObjectRectangleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(100, 100, 10, 10);
            Assert.IsFalse(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(100, 0, 10, 10);
            Assert.IsFalse(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(0, 100, 10, 10);
            Assert.IsFalse(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestInRange()
        {
            Rectangle rectA = new Rectangle(0, 10, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(0, 5, 10, 10);
            Assert.IsTrue(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedVert()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(0, 10, 10, 10);
            Assert.IsTrue(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestClippedHori()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(10, 0, 10, 10);
            Assert.IsTrue(po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestCornerHit()
        {
            Rectangle rectA = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(10, 10, 10, 10);
            Assert.IsTrue(po.CollidedRectangle(rectB));
        }
    }

    [TestClass]
    public class PhysicsObjectCircleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(100, 100), 10);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(0, 100), 10);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(100, 0), 10);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTop()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(5, -5), 10);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottom()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(5, 15), 10);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionLeft()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-5, 5), 10);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionRight()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(15, 5), 10);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionInside()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(5, 5), 5);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopLeft()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-5, -5), 5);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopLeft()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-1, -1), 5);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomLeft()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-5, 15), 5);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomLeft()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-1, 11), 5);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeTopRight()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(15, -5), 5);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionTopRight()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(11, -1), 5);
            Assert.IsTrue(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestOutOfRangeBottomRight()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(-15, 15), 5);
            Assert.IsFalse(po.CollidedCircle(circ));
        }

        [TestMethod]
        public void TestCollisionBottomRight()
        {
            Rectangle rect = new Rectangle(0, 0, 10, 10);
            PhysicsObject po = new PhysicsObject(rect);

            Circle circ = new Circle(new Vector2(11, 11), 5);
            Assert.IsTrue(po.CollidedCircle(circ));
        }
    }
}
