using System.Collections;
using System.Collections.Generic;
using Playground.Items;
using Playground.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

//using Photon.Pun;
//using UnityEditor.SceneManagement;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Mother class of all the heros
    /// </summary>
    public abstract class Hero : Character
    {
        
        protected GameObject GameOver;//active when the player die
        /// <summary>
        /// The name of the hero (all types grouped in <c>HerosNames</c> enum)
        /// </summary>
        protected readonly HerosNames heroName;
        
        /// <summary>
        /// The secondaryWeapon weapon (the primaryWeapon weappon is setted up in <c>Character</c> class)
        /// </summary>
        protected Weapon secondaryWeapon;
        
        /// <summary>
        /// Inventory of the hero during the game
        /// </summary>
        protected List<Item> inventory;
        
        /// <summary>
        /// Max size of the inventory
        /// </summary>
        protected int maxInventory = 16; // Arbitrary number

        protected HealthBar healthBar;

        // Getters and setters
        public HerosNames Name => heroName;

        public List<Item> Inventory => inventory;

        
        // Unity state related variables
        protected bool UseSecondaryWeapon = false;


        public void SetupHealthBar(HealthBar health)
        {
            healthBar = health;
            healthBar.SetupHero(this);
        }
        
        protected override void PvSetter(int value)
        {
            pv = value < 0 ? 0 : value > maxPv ? maxPv : value;
            healthBar.SetHealth();
            if (pv == 0)
                StartCoroutine(TheDeathIsComing());
        }

        public override int Pv
        {
            get => pv;
            set => PvSetter(value);
        }


        /// <summary>
        /// Add an item to the inventory if it's possible
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>true if the @item have been added successfuly, false otherwise</returns>
        public bool AddItem(Item item)
        {
            if (inventory.Count == maxInventory)
                return false;
            
            inventory.Add(item);
            return true;
        }

        public Weapon SecondHand
        {
            get => secondaryWeapon;
            set => secondaryWeapon = value;
        }

        public int MaxInventory
        {
            get => maxInventory;
            set => maxInventory = value;
        }

        protected Hero(WeaponsNames primaryWeapon, int maxPv, int level, HerosNames heroName, WeaponsNames secondaryWeapon,
            List<Item> defaultItems = null) : base(maxPv, level)
        {
            this.heroName = heroName;
            inventory = new List<Item>();
            if (defaultItems != null)
            {
                foreach (Item defaultItem in defaultItems)
                {
                    this.inventory.Add(defaultItem);
                }
            }
        }

        public new virtual void Awake() // Not override bc the mother function is not the same scope
        {
            base.Awake();
            name = heroName.ToString();
            GameOver = GameObject.Find("GameMaster/MenuCanvas/GameOverScreen");//permet de pointer vers l'écran gameover
            //secondaryWeapon = gameObject.AddComponent<Weapon>();
            //specialAttack = gameObject.AddComponent<SpecialAttack>();
        }

        public override void Update()
        {
            if (view.Owner == null || view.IsMine)
            {
                base.Update();
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                if (horizontalMove != 0)
                    animator.SetBool("IsRunning", true);
                
                else
                    animator.SetBool("IsRunning", false);

                if (Input.GetButtonDown("Jump"))
                {
                    jump = true;
                    animator.SetBool("IsJumping", true);
                }

                if (Input.GetButtonDown("SwitchGravity"))
                    switchGravity = true;

                if (Input.GetButtonDown("PrimaryWeapon"))
                    //try to put the soundtrigger here
                    UsePrimaryWeapon = true;
                
                if (Input.GetButtonDown("SecondaryWeapon"))
                    UseSecondaryWeapon = true;
            }
        }


        public override void FixedUpdate()
        {
            base.FixedUpdate();
            // Shooting with secondaryWeapon
            if (UseSecondaryWeapon)
            {
                secondaryWeapon.TryShoot();
                UseSecondaryWeapon = false;
            }
        }
        
        protected override IEnumerator TheDeathIsComing()
        {
            Time.timeScale = 0;
            yield return base.TheDeathIsComing();
            if (Physics2D.gravity.y > 0)
            {
                Debug.Log("cocuou");
                Physics2D.gravity *= -1;
            }

            GameOver.SetActive(true);
        }

        public IEnumerator OnBecameInvisible()
        {
            if (view.Owner == null || view.IsMine)
                yield return TheDeathIsComing();
        }
    }
}