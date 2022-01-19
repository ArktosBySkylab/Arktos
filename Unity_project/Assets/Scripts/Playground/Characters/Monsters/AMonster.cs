using Playground.Items.Weapons;

namespace Playground.Characters.Monsters
{
    public class AMonster : Monster
    {
        public AMonster() : base(MonstersNames.AMonster, WeaponsNames.Stick, 1000, 1)
        {
        }
    }
}