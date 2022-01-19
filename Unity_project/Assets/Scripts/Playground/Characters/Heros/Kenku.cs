using Playground.Items.Weapons;

namespace Playground.Characters.Heros
{
    public class Kenku : Hero
    {
        public Kenku() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Kenku, WeaponsNames.Stick, SpecialAttacks.Kenku)
        {
        }
    }
}