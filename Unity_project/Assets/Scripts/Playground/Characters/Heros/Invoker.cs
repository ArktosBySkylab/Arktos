using Playground.Weapons;

namespace Playground.Characters.Heros
{
    public class Invoker : Hero
    {
        public Invoker() : base(WeaponsNames.Stick, 2000, 1,
            HerosNames.Invoker, WeaponsNames.Stick, SpecialAttacks.Invoker)
        {
        }
    }
}