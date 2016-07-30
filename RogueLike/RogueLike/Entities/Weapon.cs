using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Entities
{
    class Weapon
    {
        private int damageRating;
        private float range;

        public void dealDamage(Agent target)
        {
            target.TakeDamage(damageRating);
        }
    }
}
