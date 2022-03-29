using Playground.Characters;

namespace Playground.Weapons
{
    public class Stick : Weapon
    {
        public Stick(Character owner) : base(10, 30, WeaponsNames.Stick, WeaponsTypes.HandToHand, owner) {}
        
        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}