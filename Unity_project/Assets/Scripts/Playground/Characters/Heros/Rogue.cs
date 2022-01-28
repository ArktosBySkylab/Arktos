using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Rogue
    /// </summary>
    public class Rogue : Hero
    {
        public Rogue() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Rogue, WeaponsNames.Stick, SpecialAttacksNames.SwapMonster)
        {
        }
    }
}