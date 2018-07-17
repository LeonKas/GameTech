using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoiScript : CharacterScript {

	// Use this for initialization
	void Start () {
        abilityTargetTag = "Hero";
        health = 120;
        protection = 0f;
        dmg = 15;
        speed = 0.6f;
        mana = 0;
        manaCost = 0;
        manaregen = 0;
    }

    public override void Ability(CharacterScript target)
    {
        Attack(target);
        Attack(target);
    }
}
