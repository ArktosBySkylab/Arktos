using System;
using System.Threading;
using ExitGames.Client.Photon.StructWrapping;
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
        public Animator animator;

        public Character Owner
        {
            get => owner;
            set => owner = owner ? owner : value;
        }

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

        public void ToogleActivation()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled ^= true;
            gameObject.GetComponent<CapsuleCollider2D>().enabled ^= true;
        }
        
        /// <summary>
        /// Start the animation only if it is possible
        /// </summary>
        /// <remarks>Have to activate the animations</remarks>
        public virtual bool TryShoot()
        {
            // if(nbUse == 0 || owner.Animator.GetInteger("IsFighting") != 0)
            //     return false;

            ToogleActivation();
            animator.SetInteger("IsFighting", 1);
            owner.Animator.SetInteger("IsFighting", 1);
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
