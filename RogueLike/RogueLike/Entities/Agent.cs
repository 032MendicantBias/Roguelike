using Microsoft.Xna.Framework;
using RogueLike.ObjectProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLike.Entities
{
    abstract class Agent
    {
        private Transform transform;

        private int currentHealth;
        private bool isDead;

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }
        }

        public abstract void Die();
    }
}
