using System;
using Playground.Weapons;
using UnityEngine;

namespace Playground.Characters.Heros
{
    /// <summary>
    /// Class of the hero of type JojoTheKing
    /// </summary>
    public class JojoTheKing : Hero
    {
        public JojoTheKing() : base(WeaponsNames.Stick, 20000, 10,
            HerosNames.JojoTheKing, WeaponsNames.Stick) 
        {
        }
    }
}