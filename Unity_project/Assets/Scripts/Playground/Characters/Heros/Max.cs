using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Invoker
    /// </summary>
    public class Max : Hero
    {
        public Max() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Ian, WeaponsNames.Stick)
        {
        }
    }
}