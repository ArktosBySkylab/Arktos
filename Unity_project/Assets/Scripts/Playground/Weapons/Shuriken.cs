namespace Playground.Weapons
{
    public class Shuriken : Weapon
    {
        public Shuriken() : base(10, 30, WeaponsNames.Shuriken, WeaponsTypes.Distance) {}
        
        public override bool Shoot()
        {
            return false;
        }
    }
}