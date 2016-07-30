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
            Rectangle rectA = new Rectangle(0, 10, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(100, 100, 1, 1);
            Assert.AreEqual(false, po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestVertInRange()
        {
            Rectangle rectA = new Rectangle(0, 10, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(100, 100, 1, 1);
            Assert.AreEqual(false, po.CollidedRectangle(rectB));
        }

        [TestMethod]
        public void TestHoriInRange()
        {
            Rectangle rectA = new Rectangle(0, 10, 10, 10);
            PhysicsObject po = new PhysicsObject(rectA);

            Rectangle rectB = new Rectangle(100, 100, 1, 1);
            Assert.AreEqual(false, po.CollidedRectangle(rectB));
        }
    }
}
