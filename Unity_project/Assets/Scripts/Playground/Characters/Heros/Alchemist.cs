using Playground.Weapons;

namespace Playground.Characters.Heros
{
    public class Alchemist : Hero
    {
        public Alchemist() : base(WeaponsNames.Stick, 1500, 1,
            HerosNames.Alchemist, WeaponsNames.Stick, SpecialAttacks.Alchemist)
        {
        }
    }
}