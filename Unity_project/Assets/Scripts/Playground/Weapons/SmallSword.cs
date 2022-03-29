using Playground.Characters;

namespace Playground.Weapons
{
    public class SmallSword : Weapon
    {
        public SmallSword(Character owner) : base(10, 30, WeaponsNames.SmallSword, WeaponsTypes.HandToHand, owner) {}

        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}