using Playground.Weapons;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type Invoker
    /// </summary>
    public class Ian : Hero
    {
        public Ian() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Ian, WeaponsNames.Stick)
        {
        }
    }
}