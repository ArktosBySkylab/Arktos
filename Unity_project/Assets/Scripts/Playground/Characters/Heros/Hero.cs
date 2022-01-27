using System.Collections.Generic;
using Playground.Items;
using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;

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
        /// The secondHand weapon (the firstHand weappon is setted up in <c>Character</c> class)
        /// </summary>
        protected Weapon secondHand;
        
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
            get => secondHand;
            set => secondHand = value;
        }

        public int MaxInventory
        {
            get => maxInventory;
            set => maxInventory = value;
        }

        protected Hero(WeaponsNames firstHand, int maxPv, int level, HerosNames name, WeaponsNames secondHand,
            SpecialAttacksNames specialAttack, List<Item> defaultItems = null) : base(firstHand, maxPv, level)
        {
            this.name = name;
            this.secondHand = gameObject.AddComponent<Weapon>();
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
    }
}