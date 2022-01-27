using Playground.Weapons;
using Playground.Weapons.SpecialAttacks;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Ninja
    /// </summary>
    public class Ninja : Hero
    {
        public Ninja() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Ninja, WeaponsNames.Stick, SpecialAttacksNames.ShurikenMaster)
        {
        }
    }
}