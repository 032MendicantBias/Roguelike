using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike.CoreObjects;

namespace TestRogueLike
{
    public static class ExtensionFunctions
    {
        public static Vector2 GetCentre(this Rectangle rect)
        {
            return rect.Center.ToVector2();
        }

        /// <summary>
        /// Performs unit test asserts on the IsAlive, ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all true
        /// </summary>
        /// <param name="component"></param>
        public static void CheckAlive(this Component component)
        {
            Assert.IsTrue(component.IsAlive);
            Assert.IsTrue(component.ShouldHandleInput);
            Assert.IsTrue(component.ShouldUpdate);
            Assert.IsTrue(component.ShouldDraw);
        }
    }
}
