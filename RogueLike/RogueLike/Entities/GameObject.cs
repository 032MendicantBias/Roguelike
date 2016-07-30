using RogueLike.ObjectProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Entities
{
    class GameObject
    {
        public GameObject Parent { get; set; }
        public List<GameObject> Children;

        private Transform Transform { get; set; }

        public GameObject(GameObject parent)
        {
            Parent = parent;
            Transform = new Transform(Parent.Transform);
        }

        public GameObject()
        {
            Transform = new Transform();
        }

    }
}
