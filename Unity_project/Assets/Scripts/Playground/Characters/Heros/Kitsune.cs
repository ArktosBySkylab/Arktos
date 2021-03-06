using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Kitsune
    /// </summary>
    public class Kitsune : Hero
    {
        public Kitsune() : base(WeaponsNames.Stick, 1500, 1,
            HerosNames.Kitsune, WeaponsNames.Stick)
        {
        }
    }
}