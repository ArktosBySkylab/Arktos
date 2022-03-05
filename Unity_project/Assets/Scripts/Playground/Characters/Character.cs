using System;
using System.Security.Cryptography;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace Playground.Characters
{
    public abstract class Character : MonoBehaviour
    {
        protected int portefolio; // Amount of money on a charcter
        protected int level;
        protected int maxLevel = 10; // Arbitrary value
        protected int pv;
        protected int maxPv;
        protected Weapon primaryWeapon; // The weapon in the first hand

        /// <summary>
        /// Variables used for Unity
        /// </summary>
        [SerializeField] protected CharacterController2D controller;
        [SerializeField] protected float runSpeed = 20f;
        /// <summary>
        /// States variables
        /// </summary>
        protected float horizontalMove = 0f;
        protected bool jump = false;
        protected bool switchGravity = false;
        protected bool UsePrimaryWeapon = false;
        
        
        // Setters and getters and associated functions
        private void Recover(int amount)
        {
            if (this.pv + amount <= this.maxPv)
            {
                this.pv += amount;
            }
            else
            {
                this.pv = this.maxPv;
            }
        }

        public int Pv
        {
            get => pv;
            set => this.Recover(value);
        }
        
        public int Portefolio
        {
            get => portefolio;
            set => portefolio = value;
        }

        public int Level => level;

        public void LevelUp()
        {
            this.level += 1;
        }

        public Weapon FirstHand
        {
            get => primaryWeapon;
            set => primaryWeapon = value;
        }

        protected Character(int maxPv, int level)
        {
            //primaryWeapon = primary;
            this.maxPv = maxPv;
            pv = maxPv;
            this.level = level;
        }

        protected virtual void Awake()
        {
            primaryWeapon = gameObject.AddComponent<Weapon>();
        }

        public virtual void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetButtonDown("SwitchGravity"))
                switchGravity = true;

            if (Input.GetButtonDown("PrimaryWeapon"))
                UsePrimaryWeapon = true;
        }

        public virtual void FixedUpdate()
        {
            // Moves
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;

            // Gravity
            if (switchGravity)
            {
                controller.SwitchGravity();
                switchGravity = false;
            }

            // Shooting
            if (UsePrimaryWeapon)
            {
                primaryWeapon.Shoot();
                UsePrimaryWeapon = false;
            }
        }

        public void OnBecameInvisible()
        {
            // TODO
            // Restart the scene directly or transfert to an end level menu ?
        }

        public void OnCollisionEnter(Collision col)
        {
            HerosNames tmp;
            if (Enum.TryParse(col.gameObject.name, out tmp)) // We can add friendly fire option here
            {
            }
            
            WeaponsNames tmp2;
            if (Enum.TryParse(col.gameObject.name, out tmp2))
            {
                Weapon weapon = col.gameObject.GetComponent<Weapon>();
                pv -= weapon.Shooted();
            }
        }
    }
}