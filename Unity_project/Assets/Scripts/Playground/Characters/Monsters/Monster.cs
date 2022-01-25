using Playground.Weapons;

namespace Playground.Characters.Monsters
{
    public abstract class Monster : Character
    {
        protected new MonstersNames name;
        protected Monster(MonstersNames name, WeaponsNames firstHand, int maxPv, int level) : base(firstHand, maxPv, level)
        {
            this.name = name;
        }
    }

}