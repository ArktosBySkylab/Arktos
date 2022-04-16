using Playground.Characters;

namespace Playground.Weapons
{
    public class SmallSword : Weapon
    {
        public SmallSword() : base(10, 30, WeaponsNames.SmallSword, WeaponsTypes.HandToHand) {}

        public override int Shooted ()
        {
            //nbUse--;
            return base.Shooted();
        }
    }
}