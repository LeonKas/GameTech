using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScript : CharacterScript {

    int itemStrength = 15;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("knightDeath") == 1)
        {
            gameObject.SetActive(false);
        }
        else { 
        abilityTargetTag = "Hero";
        maxHealth = 260;
        health = PlayerPrefs.GetInt("knightHealth", maxHealth);
        protection = 0f;
        dmg = 20;
        speed = 0.3f;
        maxMana = 100;
        mana = maxMana;
        manaregen = 6;
        manaCost = 10;
        healthregen = 30;
        itemCount = PlayerPrefs.GetInt("knightItemCount", maxItemCount);
        }
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
            StartCoroutine(ShieldDash(target, moveTime));
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

    public IEnumerator ShieldDash(CharacterScript target, float moveTime)
    {
        Vector3 oldPos = transform.position;
        StartCoroutine(MoveTo(target.transform.position, moveTime / 2));
        yield return new WaitForSeconds(moveTime / 2);
        target.ProtectionIncrease(0.5f);
        ReduceMana(manaCost);
        StartCoroutine(MoveTo(oldPos, moveTime / 2));
        yield return new WaitForSeconds(moveTime / 2);
        transform.position = oldPos;
        yield return null;
    }

    public override void SetPrefs()
    {
        PlayerPrefs.SetInt("knightHealth", health);
        PlayerPrefs.SetInt("knightItemCount", itemCount);
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
        if (isDead)
        {
            PlayerPrefs.SetInt("knightDeath", 1);
        }

    }
}
