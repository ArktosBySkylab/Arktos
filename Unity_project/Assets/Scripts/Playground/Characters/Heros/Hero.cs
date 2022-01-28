using System.Collections.Generic;
using Playground.Items;
using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;
using UnityEngine;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Mother class of all the heros
    /// </summary>
    public abstract class Hero : Character
    {
        /// <summary>
        /// The name of the hero (all types grouped in <c>HerosNames</c> enum)
        /// </summary>
        protected new readonly HerosNames name;
        
        /// <summary>
        /// The secondaryWeapon weapon (the primaryWeapon weappon is setted up in <c>Character</c> class)
        /// </summary>
        protected Weapon secondaryWeapon;
        
        /// <summary>
        /// The special attack of the hero
        /// </summary>
        protected readonly SpecialAttack specialAttack;
        
        /// <summary>
        /// Inventory of the hero during the game
        /// </summary>
        protected List<Item> inventory;
        
        /// <summary>
        /// Max size of the inventory
        /// </summary>
        protected int maxInventory = 16; // Arbitrary number

        // Getters and setters
        public HerosNames Name => name;

        public List<Item> Inventory => inventory;

        
        // Unity state related variables
        protected bool UseSecondaryWeapon = false;
        
        
        /// <summary>
        /// Add an item to the inventory if it's possible
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>true if the @item have been added successfuly, false otherwise</returns>
        public bool AddItem(Item item)
        {
            if (inventory.Count == maxInventory)
            {
                return false;
            }
            
            inventory.Add(item);
            return true;
        }

        public Weapon SpecialAttack => specialAttack;

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

        protected Hero(WeaponsNames primaryWeapon, int maxPv, int level, HerosNames name, WeaponsNames secondaryWeapon,
            SpecialAttacksNames specialAttack, List<Item> defaultItems = null) : base(primaryWeapon, maxPv, level)
        {
            this.name = name;
            this.secondaryWeapon = gameObject.AddComponent<Weapon>();
            this.specialAttack = gameObject.AddComponent<SpecialAttack>();
            this.inventory = new List<Item>();
            if (defaultItems != null)
            {
                foreach (Item defaultItem in defaultItems)
                {
                    this.inventory.Add(defaultItem);
                }
            }
        }


        public void Update()
        {
            if (Input.GetButtonDown("SecondaryWeapon"))
                UseSecondaryWeapon = true;
        }


        public void FixedUpdate()
        {
            // Shooting with secondaryWeapon
            if (UseSecondaryWeapon)
            {
                secondaryWeapon.Shoot();
                UseSecondaryWeapon = false;
            }
        }
    }
}