using Playground.Characters.Heros;
using UnityEngine;

namespace Playground.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        protected Weapon(WeaponsNames weapon)
        {
            var test = gameObject.AddComponent<Alchemist>();
        }
    }
}