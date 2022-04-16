using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Drow
    /// </summary>
    public class Drow : Hero
    {
        public Drow() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Drow, WeaponsNames.Stick)
        {
        }
    }
}