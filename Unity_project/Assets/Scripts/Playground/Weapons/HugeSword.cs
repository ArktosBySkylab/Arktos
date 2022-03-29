using System;
using Playground.Characters;

namespace Playground.Weapons
{
    public class HugeSword : Weapon
    {
        public HugeSword(Character owner) : base(10, 30, WeaponsNames.HugeSword, WeaponsTypes.HandToHand, owner) {}

        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}