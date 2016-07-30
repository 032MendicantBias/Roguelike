using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.CoreObjects;

namespace TestRogueLike
{
    [TestClass]
    public class TestComponent
    {
        /// <summary>
        /// We cannot instantiate component because it is private, so we create this test wrapper so that we can test the Component class
        /// </summary>
        private class MockComponent : Component
        {

        }

        [TestMethod]
        public void TestComponentConstructor()
        {
            MockComponent component = new MockComponent();

            component.CheckAlive();
        }
    }
}
