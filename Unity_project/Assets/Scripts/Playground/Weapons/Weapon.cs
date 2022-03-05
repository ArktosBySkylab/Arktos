using System;
using Playground.Characters;
using Playground.Characters.Heros;
using Playground.Items;
using UnityEngine;

namespace Playground.Weapons
{
    public abstract class Weapon : Item
    {
        protected int damage;
        //protected Character owner;
        protected WeaponsNames _name;
        protected WeaponsTypes type;

        public int Damage => damage;

        //public Character Owner => owner;

        public WeaponsNames Name => _name;

        public WeaponsTypes Type => type;

        public int NbUse => nbUse;


        protected Weapon(int nbUse, int damage, WeaponsNames name, WeaponsTypes type)
        {
            _name = name;
            this.nbUse = nbUse;
            this.damage = damage;
            this.type = type;
        }

        public void Awake()
        {
            this.name = _name.ToString();
        }

        /// <summary>
        /// Try to shoot something
        /// </summary>
        /// <returns>true if the shot have been done, false otherwise</returns>
        public abstract bool Shoot();

        public virtual int Shooted()
        {
            return damage;
        }
    }
}
