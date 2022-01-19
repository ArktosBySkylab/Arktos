using Playground.Weapons;

namespace Playground.Characters.Heros
{
    public class Drow : Hero
    {
        public Drow() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Drow, WeaponsNames.Stick, SpecialAttacks.Drow)
        {
        }
    }
}