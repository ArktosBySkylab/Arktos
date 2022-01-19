using Playground.Weapons;

namespace Playground.Characters.Heros
{
    public class Mage : Hero
    {
        public Mage() : base(WeaponsNames.Stick, 1300, 1,
            HerosNames.Mage, WeaponsNames.Stick, SpecialAttacks.Mage)
        {
        }
    }
}