using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Playground
{
    public abstract class Character : MonoBehaviour
    {
        protected int portefolio; // Amount of money on a charcter
        protected int level; 
        protected int pv;
        protected readonly int maxPv;
        protected Weapon firstHand; // The weapon in the first hand

        
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

        protected Character(Weapon firstHand, int portefolio, int level, int maxPv)
        {
            this.pv = maxPv;
            this.maxPv = maxPv;
            this.portefolio = portefolio;
            this.level = level;
            this.firstHand = firstHand;
        }

        protected Character()
        {
            //TODO
            // Do something when no arguments is given
        }
        
        public void Attack(Character ennemy)
        {
            // TODO
        }
    }
}