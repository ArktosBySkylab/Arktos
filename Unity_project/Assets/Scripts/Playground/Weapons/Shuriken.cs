using System;
using Playground.Characters;

namespace Playground.Weapons
{
    public class Shuriken : Weapon
    {
        public Shuriken(Character owner) : base(10, 30, WeaponsNames.Shuriken, WeaponsTypes.Distance, owner) {}
    }
}