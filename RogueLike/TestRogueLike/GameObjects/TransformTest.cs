using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RogueLike.ObjectProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    [TestClass]
    public class TransformTest
    {
        [TestMethod]
        public void ChildTransformIsAnchored()
        {
            Transform parentTransform = new Transform(new Vector2(10, 2), 0.0f, new Vector2(1, 1));
            Transform childTransform = new Transform(parentTransform);
            
            Assert.AreEqual(parentTransform.LocalPosition, childTransform.LocalPosition);
            Assert.AreEqual(parentTransform.LocalRotation, childTransform.LocalRotation);
            Assert.AreEqual(parentTransform.Scale, childTransform.Scale);
        }
    }
}
