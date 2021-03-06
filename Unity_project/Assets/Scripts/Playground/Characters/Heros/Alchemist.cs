using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Alchemist
    /// </summary>
    public class Alchemist : Hero
    {
        public Alchemist() : base(WeaponsNames.Stick, 1500, 1,
            HerosNames.Alchemist, WeaponsNames.Stick)
        {
        }
    }
}