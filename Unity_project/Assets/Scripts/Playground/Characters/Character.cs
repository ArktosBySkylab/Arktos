using System;
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
        protected Weapon firstHand; // The weapon in the first hand

        /// <summary>
        /// Variables used for Unity
        /// </summary>
        [SerializeField] protected CharacterController2D controller;
        [SerializeField] protected float speed = 20f;
        /// <summary>
        /// States variables
        /// </summary>
        protected float horizontalMove = 0f;
        protected bool jump = false;
        protected bool switchGravity = false;
        
        
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
            get => firstHand;
            set => firstHand = value;
        }

        protected Character(WeaponsNames firstHand, int maxPv, int level)
        {
            this.firstHand = gameObject.AddComponent<Weapon>();
            this.maxPv = maxPv;
            this.pv = maxPv;
            this.level = level;
        }
        
        public void Attack(Character ennemy)
        {
            // TODO
        }

        public void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetButtonDown("SwitchGravity"))
            {
                switchGravity = true;
            }
        }

        public void FixedUpdate()
        {
           controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
           jump = false;

           if (switchGravity)
           {
               Physics2D.gravity = -Physics2D.gravity;
               gameObject.transform.Rotate(0f, 0f, 180f);
               switchGravity = false;
           }
        }
    }
}