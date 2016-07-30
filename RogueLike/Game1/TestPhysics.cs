using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace TestRogueLike
{
    [TestClass]
    public class PhysicsObjectRectangleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rectangle rectA = new Rectangle(0, 10, 10, 10);
        }
    }
}
