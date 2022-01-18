using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground {
    public abstract class Character : MonoBehaviour
    {
        protected int portefolio;
        protected int level;
        protected int pv;
        protected readonly int maxPv;
        protected Weapon firstHand;

        public abstract Character(Weapon firstHand, int portefolio, int level, int pv, int maxPv) {
            this.portefolio = portefolio;
            this.level = level;
            this.pv = pv;
            this.maxPv = maxPv;
            this.firstHand = firstHand;
        }

        public abstract attack(Character ennemy);
    }
}
