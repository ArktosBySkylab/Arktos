

namespace Playground.Weapons.SpecialAttacks
{
    public abstract class SpecialAttack : Weapon
    {
        protected new SpecialAttack name;

        protected SpecialAttack()
        {
            type = WeaponsTypes.SpecialAttack;
        }
    }
}