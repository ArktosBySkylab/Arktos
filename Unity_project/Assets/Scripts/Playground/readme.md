# Structure

All files related to characters class (heros AND monster) are in Characters directory
Weapon class inherit of Item class, but there aren't in the same directory to not have too many sub directories

Here is the relation between the different classes:

Character (abstract)
├── Hero (abstract)
│   ├── Alchemist
│   ├── Dow
│   ├── Invoker
│   ├── JojoTheKing
│   ├── Kenku
│   ├── Kitsune
│   ├── Mage
│   ├── Ninja
│   └── Rogue
└── Monster (abstract)
   ├── AMonster
   └── AnotherMonster

Item (abstract)
└── Weapon (abstract)
    ├── Bow
    ├── Dogger
    ├── HugeSword
    ├── MagicWand
    ├── Shield
    ├── Shuriken
    ├── SmallSword
    ├── Stick
    └── SpecialAttack (abstract)
        ├── CrowCloud (Kenku)
        ├── GoldenBomb (Alchemist)
        ├── HouseOfCards (Invoker)
        ├── Invisibility (Drow)
        ├── LifeStealer (Mage)
        ├── Plouf (JojoTheKing)
        ├── Resurgence (Kitsune)
        ├── ShurikenMaster (Ninja)
        └── SwapMonster (Rogue)

