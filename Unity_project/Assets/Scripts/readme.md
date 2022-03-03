# Solution structure
Every script are in the same solution, the `Scripts.sln` solution (It's the file
        that have to be load in Rider)

There is a csproj file for each part of the project: Menus.csproj,
      Playground.csproj, Multiplayer.csproj and Levels.csproj

A readme can be found is each of those dir to have more explainations

# Main tree
.
├── readme.md
├── Scripts.sln
│
├── Levels/
│   ├── readme.md
│   └── Levels.csproj
│
├── Menus/
│   ├── readme.md
│   ├── Menus.csproj
│   ├── Main_Menu_Interaction.cs
│   └── Pause_Menu.cs
│
├── Multiplayer/
│   ├── readme.md
│   ├── Multiplayer.csproj
│   ├── Loading/
│   └── Lobby/
│
└── Playground/
    ├── readme.md
    ├── Playground.csproj
    ├── Characters/
    ├── Items/
    └── Weapons/
