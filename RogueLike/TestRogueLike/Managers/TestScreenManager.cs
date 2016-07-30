using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.Managers;
using TestRogueLike.MockClasses;

namespace TestRogueLike.Managers
{
    [TestClass]
    public class TestScreenManager
    {
        [ClassInitialize]
        public static void SetupScreenManager(TestContext testContext)
        {
            ScreenManager.Instance.LoadContent();
            ScreenManager.Instance.Initialise();
            ScreenManager.Instance.Begin();
        }

        [TestMethod]
        public void TestGetCurrentScreenAs()
        {
            MockBaseScreen mockBaseScreen = ScreenManager.Instance.AddChild(new MockBaseScreen(), true, true);
            ScreenManager.Instance.Update(0);

            Assert.AreEqual(mockBaseScreen, ScreenManager.Instance.GetCurrentScreenAs<MockBaseScreen>());
        }

        [TestMethod]
        public void TestTransition()
        {
            MockBaseScreen mockBaseScreen = ScreenManager.Instance.AddChild(new MockBaseScreen(), true, true);
            MockBaseScreen transitionScreen = new MockBaseScreen();

            ScreenManager.Instance.Update(0);
            Assert.AreEqual(mockBaseScreen, ScreenManager.Instance.CurrentScreen);

            ScreenManager.Instance.Transition(mockBaseScreen, transitionScreen);
            ScreenManager.Instance.Update(0);

            Assert.AreEqual(transitionScreen, ScreenManager.Instance.CurrentScreen);
        }
    }
}
