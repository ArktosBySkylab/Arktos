using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Mage
    /// </summary>
    public class Mage : Hero
    {
        public Mage() : base(WeaponsNames.Stick, 1300, 1,
            HerosNames.Mage, WeaponsNames.Stick, SpecialAttacksNames.LifeStealer)
        {
        }
    }
}