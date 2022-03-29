using Playground.Characters;

namespace Playground.Weapons
{
    public class MagicWand : Weapon
    {
        public MagicWand(Character owner) : base(10, 30, WeaponsNames.MagicWand, WeaponsTypes.HandToHand, owner) {}
        
        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}