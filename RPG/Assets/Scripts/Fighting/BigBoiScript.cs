using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoiScript : CharacterScript {


	// Use this for initialization
	void Start () {
        abilityTargetTag = "Villain";
        health = 160;
        protection = 0f;
        dmg = 10;
        speed = 0.2f;
        mana = 0;
        manaCost = 0;
        manaregen = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {

            StopCoroutine("AttackTest");
            StartCoroutine(AttackTest(new Vector3(-4f, 2f, 0f)));
        }
	}

    public override void Ability(CharacterScript target)
    {
        StartCoroutine(ShieldDash(target, moveTime));
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
}
