using System;
using Levels;
using Playground.Characters;
using Playground.Characters.Heros;
using Playground.Items;
using UnityEngine;

namespace Playground.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        protected int damage;
        //protected Character owner;
        protected WeaponsNames _name;
        protected WeaponsTypes type;
        protected int nbUse;
        protected Character owner;
        protected Animator animator;

        public int Damage => damage;

        //public Character Owner => owner;

        public WeaponsNames Name => _name;

        public WeaponsTypes Type => type;

        public int NbUse => nbUse;


        protected Weapon(int nbUse, int damage, WeaponsNames name, WeaponsTypes type, Character owner)
        {
            this.owner = owner;
            _name = name;
            this.nbUse = nbUse;
            this.damage = damage;
            this.type = type;
        }

        public void Awake() 
        {
            this.name = _name.ToString();
            this.animator = gameObject.GetComponent<Animator>();
        }
        
        /// <summary>
        /// Start the animation only if it is possible
        /// </summary>
        /// <remarks>Have to activate the animations</remarks>
        public virtual bool TryShoot()
        {
            if(nbUse == 0 || owner.Animator.GetInteger("IsFighting") != 0)
                return false;
            
            owner.Animator.SetInteger("IsFighting", 1);
            animator.SetInteger("IsFighting", 1);
            return true;
        }
        
        /// <summary>
        /// Try to shoot something
        /// </summary>
        /// <returns>Return the damage value</returns>
        public virtual int Shooted()
        {
            return damage;
        }
    }
}
