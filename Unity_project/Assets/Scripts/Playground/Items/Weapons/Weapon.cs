using Playground.Characters.Heros;

namespace Playground.Items.Weapons
{
    public abstract class Weapon : Item
    {
        protected Weapon(WeaponsNames weapon)
        {
            var test = gameObject.AddComponent<Alchemist>();
        }
    }
}