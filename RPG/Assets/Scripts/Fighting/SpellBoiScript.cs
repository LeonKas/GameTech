using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBoiScript : CharacterScript {

    [SerializeField]
    GameObject fireBall;

    int abilityDmg = 40;
   

	// Use this for initialization
	void Start () {
        abilityTargetTag = "Hero";
        health = 90;
        protection = 0f;
        dmg = 10;
        speed = 0.4f;
        maxMana = 100;
        mana = maxMana;
        manaregen = 5;
        manaCost = 10;
    }

    public override void Ability(CharacterScript target)
    {
        if (mana >= manaCost)
        {
           // target.TakeDmg(abilityDmg);
            ReduceMana(manaCost);
            FireBallScript fireAttack = Instantiate(fireBall).GetComponent<FireBallScript>();
            fireAttack.gameObject.transform.position = transform.position;
            fireAttack.FireAttack(target, abilityDmg);
        }
    }

}
