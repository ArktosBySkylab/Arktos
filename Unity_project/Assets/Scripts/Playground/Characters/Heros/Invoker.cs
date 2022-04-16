using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Invoker
    /// </summary>
    public class Invoker : Hero
    {
        public Invoker() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Invoker, WeaponsNames.Stick)
        {
        }
    }
}