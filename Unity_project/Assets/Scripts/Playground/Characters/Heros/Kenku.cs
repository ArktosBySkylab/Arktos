using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Kenku
    /// </summary>
    public class Kenku : Hero
    {
        public Kenku() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Kenku, WeaponsNames.Stick, SpecialAttacksNames.CrowCloud)
        {
        }
    }
}