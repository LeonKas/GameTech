using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealeeBoiScript : CharacterScript {

    int healstrength = 20;
	// Use this for initialization
	void Start () {
        abilityTargetTag = "Villain";
        health = 100;
        protection = 0f;
        dmg = 7;
        speed = 0.8f;
        maxMana = 100;
        mana = maxMana;
        manaregen = 5;
        manaCost = 20;
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
            StartCoroutine(HealDash(target, moveTime));
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
}
