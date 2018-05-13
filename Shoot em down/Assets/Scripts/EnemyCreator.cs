using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour {

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private float spawnchance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float random = UnityEngine.Random.Range(1, 100);
        if (random <= spawnchance)
            Spawn();
       
    }

    void Spawn() {
        float ranFact = UnityEngine.Random.Range(-8, 8);
        transform.position = new Vector3(ranFact, transform.position.y, transform.position.z);
        GameObject soonDead = Instantiate(enemy);
        soonDead.transform.position = transform.position;
    }
    
}
