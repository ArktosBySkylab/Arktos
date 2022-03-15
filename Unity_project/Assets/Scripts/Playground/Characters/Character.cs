using System;
using System.Security.Cryptography;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

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
        [SerializeField] public CharacterController2D controller;

        [SerializeField] public float runSpeed = 50f;
        protected Animator animator;

        /// <summary>
        /// States variables
        /// </summary>
        protected float horizontalMove = 0f;
        protected bool jump = false;
        protected bool switchGravity = false;
        protected bool UsePrimaryWeapon = false;
        protected PhotonView view;

        #region Setters & Getters

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

        #endregion

        protected virtual void Awake()
        {
            view = gameObject.GetComponent<PhotonView>();
            primaryWeapon = gameObject.AddComponent<Weapon>();
            animator = gameObject.GetComponent<Animator>();
        }

        public virtual void Update()
        {
            if (GetComponent<Rigidbody2D>().velocity.y > 0 && Physics2D.gravity.y < 0)
            {
                animator.SetBool("BeginJump", true);
            }
            
            else if (GetComponent<Rigidbody2D>().velocity.y < 0 && Physics2D.gravity.y < 0)
            {
                animator.SetBool("BeginJump", false);
            }
            
            else if (GetComponent<Rigidbody2D>().velocity.y > 0 && Physics2D.gravity.y > 0)
            {
                animator.SetBool("BeginJump", false);
            }
            
            else if (GetComponent<Rigidbody2D>().velocity.y < 0 && Physics2D.gravity.y > 0)
            {
                animator.SetBool("BeginJump", true);
            }
        }

        public virtual void FixedUpdate()
        {
            // Moves
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;

            // Gravity
            if (switchGravity)
            {
                if (controller.SwitchGravity())
                {
                    animator.SetBool("IsGChanging", true);
                }
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

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsGChanging", false);
        }
    }
}