using Playground.Items.Weapons;

namespace Playground.Characters.Heros
{
    public class Ninja : Hero
    {
        public Ninja() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Ninja, WeaponsNames.Stick, SpecialAttacks.Ninja)
        {
        }
    }
}