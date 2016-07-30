using System;
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
}
