using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamedicScript : CharacterScript {

    int healstrength = 20;
    int itemStrength = 15;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("medicDeath") == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            abilityTargetTag = "Hero";
            maxHealth = 200;
            health = PlayerPrefs.GetInt("medicHealth", maxHealth);
            protection = 0f;
            dmg = 10;
            speed = 0.9f;
            maxMana = 150;
            mana = maxMana;
            manaregen = 10;
            manaCost = 20;
            healthregen = 30;
            itemCount = PlayerPrefs.GetInt("medicItemCount", maxItemCount);
        }
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
            StartCoroutine(HealDash(target, moveTime));
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

    public IEnumerator HealDash(CharacterScript target, float moveTime)
    {
        Vector3 oldPos = transform.position;
        StartCoroutine(MoveTo(target.transform.position, moveTime / 2));
        yield return new WaitForSeconds(moveTime / 2);
        target.Healed(healstrength);
        ReduceMana(manaCost);
        StartCoroutine(MoveTo(oldPos, moveTime / 2));
        yield return new WaitForSeconds(moveTime / 2);
        transform.position = oldPos;
        yield return null;
    }

    public override void SetPrefs()
    {
        PlayerPrefs.SetInt("medicHealth", health);
        PlayerPrefs.SetInt("medicItemCount", itemCount);
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
        if (isDead)
        {
            PlayerPrefs.SetInt("medicDeath", 1);
        }

    }
}
