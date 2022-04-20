using System.Collections;
using System.Collections.Generic;
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

        public override void Update()
        {
            //UsePrimaryWeapon = true;
        }

        protected override IEnumerator TheDeathIsComing()
        {
            yield return base.TheDeathIsComing();
        }
    }

}