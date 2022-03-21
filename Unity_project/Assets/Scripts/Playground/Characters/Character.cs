using System;
using System.Security.Cryptography;
using Levels;
using Playground.Characters.Heros;
using Playground.Weapons;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEngine.SceneManagement;

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

        public int Pv
        {
            get => pv;
            // The first condition is used to see if the result is out of bound
            // The second one return 0 if the player loose pv, max weither
            set => pv = pv + value < 0 || pv + value > maxPv ? (value < 0 ? 0 : maxPv) : pv + value;
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

        protected void Awake()
        {
            view = gameObject.GetComponent<PhotonView>();
            animator = gameObject.GetComponent<Animator>();
        }

        protected virtual void Start() // Start and not awake bc there is an awake in the weapon
        {
            primaryWeapon = gameObject.GetComponentInChildren<SmallSword>(); // gameObject.AddComponent<Weapon>();
        }

        /// <summary>
        /// Setup the right jump animation
        /// </summary>
        protected void JumpAnimation()
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

        public virtual void Update()
        {
            JumpAnimation();
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
                primaryWeapon.TryShoot();
                UsePrimaryWeapon = false;
            }
        }

        public void OnBecameInvisible()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            GameMaster.PersonnalDebug("Trigger");

            if (Enum.TryParse(col.gameObject.name, out WeaponsNames _))
            {
                Debug.Log(name + ": LOST PV -> " + pv);
                Weapon weapon = col.gameObject.GetComponent<Weapon>();
                Pv -= weapon.Shooted();
            }
        }

        public void OnLanding()
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsGChanging", false);
        }
    }
}