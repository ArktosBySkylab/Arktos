using System.Collections;
using System.Collections.Generic;
using Playground.Characters.Heros;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Hero hero;
    public Slider slider;

    public void SetupHero(Hero hero)
    {
        slider = gameObject.GetComponent<Slider>();
        slider.maxValue = hero.MaxPv;
        slider.value = hero.MaxPv;
        this.hero = hero;
    }

    public void SetHealth()
    {
        slider.value = hero.Pv;
    }
}
