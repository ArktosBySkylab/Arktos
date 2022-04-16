using Playground.Characters;

namespace Playground.Weapons
{
    public class MagicWand : Weapon
    {
        public MagicWand() : base(10, 30, WeaponsNames.MagicWand, WeaponsTypes.HandToHand) {}
        
        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}