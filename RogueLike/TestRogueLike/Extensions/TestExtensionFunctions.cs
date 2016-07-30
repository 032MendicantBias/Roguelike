using Microsoft.VisualStudio.TestTools.UnitTesting;
using RogueLike.CoreObjects;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Extensions
{
    public static class TestExtensionFunctions
    {
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

        /// <summary>
        /// Performs unit test asserts on the IsAlive, ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all false
        /// </summary>
        /// <param name="component"></param>
        public static void CheckDead(this Component component)
        {
            Assert.IsFalse(component.IsAlive);
            Assert.IsFalse(component.ShouldHandleInput);
            Assert.IsFalse(component.ShouldUpdate);
            Assert.IsFalse(component.ShouldDraw);
        }

        /// <summary>
        /// Performs unit test asserts on the ShouldHandleInput, ShouldUpdate and ShouldDraw properties to make sure they are all false and the IsAlive to make sure it is true
        /// </summary>
        /// <param name="component"></param>
        public static void CheckHidden(this Component component)
        {
            Assert.IsTrue(component.IsAlive);
            Assert.IsFalse(component.ShouldHandleInput);
            Assert.IsFalse(component.ShouldUpdate);
            Assert.IsFalse(component.ShouldDraw);
        }

        /// <summary>
        /// Performs a reference check on the inputted ordered lists.
        /// Will only return true if the lists contain the same reference objects in the exact same order.
        /// Performs no unit testing asserts - this is left to the user to handle via the return bool.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static bool CheckOrderedListsEqual<T>(this List<T> expected, List<T> actual)
        {
            // Check sizes first!
            if (expected.Count != actual.Count)
            {
                return false;
            }

            // Make sure that index wise the objects are the same
            for (int i = 0; i < expected.Count; i++)
            {
                if (!expected[i].Equals(actual[i]))
                {
                    return false;
                }
            }

            // If we have got here the two lists contain the same references in the same order
            return true;
        }
    }
}
