using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playground
{
    public class Hero : Character
    {
        private herosType type;
        private Weapon secondHand;
        private Weapon specialAttack;

        // Here, the public constructor
        // will process information about the hero's type
        // and will call the private constructor with the right arguments
        // to call the mother constructor
        public Hero(herosType type)
        {
            this.type = type;
            switch (type)
            {
                case herosType.Alchimist:
                    break;
                
                case herosType.Drow:
                    break;
                
                case herosType.Invoker:
                    break;
                
                case herosType.Kenku:
                    break;
                
                case herosType.Kitsune:
                    break;
                
                case herosType.Mage:
                    break;
                
                case herosType.Ninja:
                    break;
                
                case herosType.Rogue:
                    break;
                
                case herosType.JojoTheKing:
                    break;
                
                default:
                    throw new ArgumentException("Hero Class Constructor: Unknown hero type.");
            }
        }
    }
}