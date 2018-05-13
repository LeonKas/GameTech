using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    private Vector3 startPos;
    
    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);

        if (transform.position.y > startPos.y + range)
           Destroy(gameObject);
    }
}
