using Playground.Characters;
using Playground.Characters.Heros;
using Playground.Items;
using UnityEngine;

namespace Playground.Weapons
{
    public abstract class Weapon : Item
    {
        protected int damage;
        protected Character owner;
        protected new WeaponsNames name;
        protected WeaponsTypes type;
        protected Weapon()
        {
        }

        public bool Shoot()
        {
            return false;
        }
    }
}
