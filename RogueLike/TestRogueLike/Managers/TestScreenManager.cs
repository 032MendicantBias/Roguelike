using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.Managers;
using TestRogueLike.MockClasses;

namespace TestRogueLike.Managers
{
    [TestClass]
    public class TestScreenManager
    {
        [TestMethod]
        public void TestGetCurrentScreenAs()
        {
            MockBaseScreen mockBaseScreen = ScreenManager.Instance.AddChild(new MockBaseScreen());
            ScreenManager.Instance.LoadContent();
            ScreenManager.Instance.Initialise();
            ScreenManager.Instance.Begin();

            Assert.AreEqual(mockBaseScreen, ScreenManager.Instance.GetCurrentScreenAs<MockBaseScreen>());
        }
    }
}
