namespace Playground.Weapons
{
    public class Dagger : Weapon
    {
        public Dagger() : base(10, 30, WeaponsNames.Dagger, WeaponsTypes.Distance) {}
        
        public override bool Shoot()
        {
            return false;
        }
    }
}