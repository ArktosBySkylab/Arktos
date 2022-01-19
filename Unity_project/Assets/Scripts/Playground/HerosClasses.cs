using System.Collections.Generic;

namespace Playground
{
    public class Alchemist : Hero
    {
        public Alchemist() : base(WeaponsType.Stick, 1500, 1,
            HerosNames.Alchemist, WeaponsType.Stick, SpecialAttacks.Alchemist)
        {
        }
    }
    
    public class Ninja : Hero
    {
        public Ninja() : base(WeaponsType.Stick, 2000, 1,
            HerosNames.Ninja, WeaponsType.Stick, SpecialAttacks.Ninja)
        {
        }
    }
    
    public class Kitsune : Hero
    {
        public Kitsune() : base(WeaponsType.Stick, 1500, 1,
            HerosNames.Kitsune, WeaponsType.Stick, SpecialAttacks.Kitsune)
        {
        }
    }
    
    public class Mage : Hero
    {
        public Mage() : base(WeaponsType.Stick, 1300, 1,
            HerosNames.Mage, WeaponsType.Stick, SpecialAttacks.Mage)
        {
        }
    }
    
    public class Rogue : Hero
    {
        public Rogue() : base(WeaponsType.Stick, 2000, 1,
            HerosNames.Rogue, WeaponsType.Stick, SpecialAttacks.Rogue)
        {
        }
    }
    
    public class Drow : Hero
    {
        public Drow() : base(WeaponsType.Stick, 2000, 1,
            HerosNames.Drow, WeaponsType.Stick, SpecialAttacks.Drow)
        {
        }
    }
    
    public class Kenku : Hero
    {
        public Kenku() : base(WeaponsType.Stick, 2000, 1,
            HerosNames.Kenku, WeaponsType.Stick, SpecialAttacks.Kenku)
        {
        }
    }
    
    public class Invoker : Hero
    {
        public Invoker() : base(WeaponsType.Stick, 2000, 1,
            HerosNames.Invoker, WeaponsType.Stick, SpecialAttacks.Invoker)
        {
        }
    }
    
    public class JojoTheKing : Hero
    {
        public JojoTheKing() : base(WeaponsType.Stick, 20000, 10,
            HerosNames.JojoTheKing, WeaponsType.Stick, SpecialAttacks.JojoTheKing)
        {
        }
    }
}