using Game1.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestRogueLike.MockClasses;

namespace TestRogueLike
{
    [TestClass]
    public class TestComponent
    {
        [TestMethod]
        public void TestComponentConstructor()
        {
            MockComponent component = new MockComponent();
            component.CheckAlive();
        }

        [TestMethod]
        public void TestComponentDie()
        {
            MockComponent component = new MockComponent();
            component.Die();
            component.CheckDead();
        }

        [TestMethod]
        public void TestComponentHide()
        {
            MockComponent component = new MockComponent();
            component.Hide();
            component.CheckHidden();
        }

        [TestMethod]
        public void TestComponentShow()
        {
            MockComponent component = new MockComponent();
            component.Hide();
            component.CheckHidden();

            component.Show();
            component.CheckAlive(); // Same check as Show would be
        }

        [TestMethod]
        public void TestShouldLoadFalseWhenLoadContentCalled()
        {
            MockComponent component = new MockComponent();
            component.LoadContent();

            Assert.IsFalse(component.ShouldLoad);
        }

        [TestMethod]
        public void TestShouldLoadTrueWhenLoadContentNotCalled()
        {
            MockComponent component = new MockComponent();
            Assert.IsTrue(component.ShouldLoad);
        }

        [TestMethod]
        public void TestShouldInitialiseFalseWhenInitialiseCalled()
        {
            MockComponent component = new MockComponent();
            component.Initialise();

            Assert.IsFalse(component.ShouldInitialise);
        }

        [TestMethod]
        public void TestShouldInitialiseTrueWhenInitialiseNotCalled()
        {
            MockComponent component = new MockComponent();
            Assert.IsTrue(component.ShouldInitialise);
        }

        [TestMethod]
        public void TestIsBegunTrueWhenBeginCalled()
        {
            MockComponent component = new MockComponent();
            component.LoadContent();
            component.Initialise();
            component.Begin();

            Assert.IsTrue(component.IsBegun);
        }

        [TestMethod]
        public void TestIsFalseTrueWhenBeginNotCalled()
        {
            MockComponent component = new MockComponent();
            component.LoadContent();
            component.Initialise();

            Assert.IsFalse(component.IsBegun);
        }
    }
}
