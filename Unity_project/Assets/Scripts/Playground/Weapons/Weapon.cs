using Playground.Characters;
using Playground.Characters.Heros;
using Playground.Items;
using UnityEngine;

namespace Playground.Weapons
{
    public abstract class Weapon : Items.Item
    {
        protected int nbUse;
        protected int damage;
        protected Character owner;
        protected WeaponsNames name;
        protected WeaponsTypes type;
        protected Weapon()
        {
        }
    }
}
