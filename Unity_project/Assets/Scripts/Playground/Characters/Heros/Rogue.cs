using Playground.Items.Weapons;

namespace Playground.Characters.Heros
{
    public class Rogue : Hero
    {
        public Rogue() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Rogue, WeaponsNames.Stick, SpecialAttacks.Rogue)
        {
        }
    }
}