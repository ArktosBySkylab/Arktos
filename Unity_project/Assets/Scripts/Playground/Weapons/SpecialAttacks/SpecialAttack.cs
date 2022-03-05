

namespace Playground.Weapons.SpecialAttacks
{
    public abstract class SpecialAttack : Weapon
    {
        protected new SpecialAttack name;

        protected SpecialAttack() : base(10, 30, WeaponsNames.Bow, WeaponsTypes.SpecialAttack)
        {
            type = WeaponsTypes.SpecialAttack;
        }

        public override bool Shoot()
        {
            return false;
        }
    }
}