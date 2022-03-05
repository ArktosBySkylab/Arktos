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
        protected WeaponsNames weaponName;
        protected WeaponsTypes type;

        public int Damage => damage;

        public Character Owner => owner;

        public WeaponsNames WeaponsName => weaponName;

        public WeaponsTypes Type => type;

        public int NbUse => nbUse;


        //protected Weapon(int nbUse, int damage, Character owner, WeaponsNames name, WeaponsTypes type)
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
