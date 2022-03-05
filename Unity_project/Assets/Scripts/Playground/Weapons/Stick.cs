namespace Playground.Weapons
{
    public class Stick : Weapon
    {
        public Stick() : base(10, 30, WeaponsNames.Stick, WeaponsTypes.HandToHand) {}
        
        public override bool Shoot()
        {
            return false;
        }
        
        public override int Shooted ()
        {
            nbUse--;
            return base.Shooted();
        }
    }
}