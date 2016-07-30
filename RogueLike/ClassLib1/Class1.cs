using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassLib1
{
    [TestClass]
    public class PhysicsObjectRectangleCollisionTest
    {
        [TestMethod]
        public void TestOutOfRange()
        {
            Rectangle rectA = new Rectangle(new Vector2(0f, 0f), 10, 10);
        }
    }
}
