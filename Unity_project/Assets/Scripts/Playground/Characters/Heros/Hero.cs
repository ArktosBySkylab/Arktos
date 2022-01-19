using System.Collections.Generic;
using Playground.Items;
using Playground.Items.Weapons;

namespace Playground.Characters.Heros
{
    public abstract class Hero : Character
    {
        protected readonly HerosNames name;
        protected Weapon secondHand;
        protected readonly Weapon specialAttack;
        protected List<Item> inventory;
        protected int maxInventory = 16; // Arbitrary number

        // Getters and setters
        public HerosNames Name => name;

        public List<Item> Inventory => inventory;

        // Add the item @item if it is possible
        // Returm true if done, and false otherwise
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
            SpecialAttacks specialAttack, List<Item> defaultItems = null) : base(firstHand, maxPv, level)
        {
            this.name = name;
            this.secondHand = gameObject.AddComponent<Weapon>();
            this.specialAttack = gameObject.AddComponent<Weapon>();
            this.inventory = new List<Item>();
            if (defaultItems != null)
                foreach (Item defaultItem in defaultItems)
                {
                    this.inventory.Add(defaultItem);
                }
        }
    }
}