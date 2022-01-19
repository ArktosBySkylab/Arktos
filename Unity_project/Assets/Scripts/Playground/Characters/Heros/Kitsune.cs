using Playground.Weapons;

namespace Playground.Characters.Heros
{
    public class Kitsune : Hero
    {
        public Kitsune() : base(WeaponsNames.Stick, 1500, 1,
            HerosNames.Kitsune, WeaponsNames.Stick, SpecialAttacks.Kitsune)
        {
        }
    }
}