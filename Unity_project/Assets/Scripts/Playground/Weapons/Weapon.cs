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

        public int Damage => damage;

        public Character Owner => owner;

        public WeaponsNames Name => name;

        public WeaponsTypes Type => type;

        public int NbUse => nbUse;


        protected Weapon()
        {
        }
        
        /// <summary>
        /// Try to shoot something
        /// </summary>
        /// <returns>true if the shot have been done, false otherwise</returns>
        public abstract bool Shoot();
    }
}
