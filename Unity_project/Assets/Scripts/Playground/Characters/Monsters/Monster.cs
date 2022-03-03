using Playground.Weapons;

namespace Playground.Characters.Monsters
{
    public abstract class Monster : Character
    {
        protected new MonstersNames name;
        protected Monster(MonstersNames name, WeaponsNames primaryWeapon, int maxPv, int level) : base(maxPv, level)
        {
            this.name = name;
        }
    }

}