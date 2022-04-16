using Playground.Characters;

namespace Playground.Weapons
{
    public class Stick : Weapon
    {
        public Stick() : base(10, 30, WeaponsNames.Stick, WeaponsTypes.HandToHand) {}
        
        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}