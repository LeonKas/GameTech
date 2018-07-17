using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : CharacterScript {

    float fireMoveTime = 0.5f;
	
    public void FireAttack(CharacterScript target, int abilityDmg)
    {
        StartCoroutine(FireFleight(target, abilityDmg));
    }

    IEnumerator FireFleight(CharacterScript target, int abilityDmg)
    {
        StartCoroutine(MoveTo(target.transform.position, fireMoveTime));
        yield return new WaitForSeconds(fireMoveTime);
        target.TakeDmg(abilityDmg);
        Destroy(gameObject);
    }
	

}
