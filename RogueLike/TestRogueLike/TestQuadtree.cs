using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike;
using RogueLike.CoreObjects;
using System.Collections.Generic;

namespace TestRogueLike
{
    [TestClass]
    public class TestQuadtree
    {
        private static List<BaseObject> objects;

        [ClassInitialize]
        public static void Initialise()
        {
            objects = new List<BaseObject>();
        }
    }
}
