using System;

namespace Playground.Weapons
{
    public class HugeSword : Weapon
    {
        public HugeSword() : base(10, 30, WeaponsNames.HugeSword, WeaponsTypes.HandToHand) {}
        
        public override bool Shoot()
        {
            return false;
        }

        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}