using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike
{
    public static class ExtensionFunctions
    {

        public static Vector2 GetCentre(this Rectangle rect)
        {
            return rect.Center.ToVector2();
        }

    }
}
