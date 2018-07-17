using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFighterScript : CharacterScript {

    int itemStrength=15;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("heroDeath") == 1)
        {
            gameObject.SetActive(false);
        }
        else {
        abilityTargetTag = "Villain";
        maxHealth = 230;
        health = PlayerPrefs.GetInt("heroHealth", maxHealth);
        protection = 0f;
        dmg = 23;
        speed = 0.7f;
        maxMana = 100;
        mana = maxMana;
        manaregen = 8;
        manaCost = 10;
        healthregen = 30;
        itemCount = PlayerPrefs.GetInt("heroItemCount", maxItemCount);
        }
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
            Attack(target);
            Attack(target);
            ReduceMana(manaCost);
        }
    }

    public override void Item()
    {
        if (itemCount > 0)
        {
            this.Healed(itemStrength);
            itemCount -= 1;
        }
    }

    public override void SetPrefs()
    {
        PlayerPrefs.SetInt("heroHealth", health);
        PlayerPrefs.SetInt("heroItemCount", itemCount);
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
        if (isDead)
        {
            PlayerPrefs.SetInt("heroDeath", 1);
        }

    }

}
