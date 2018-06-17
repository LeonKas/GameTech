using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    [SerializeField]
    GameObject[] backgroundItems = new GameObject[5];

    private float lastX;
	// Use this for initialization
	void Start () {
        lastX = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float deltaX = gameObject.transform.position.x - lastX;

        foreach (GameObject item in backgroundItems)
        {

            //float newX = item.transform.position.x + deltaX * 1 / item.transform.position.z;
            float newX = item.transform.position.x - deltaX * 1 / item.transform.position.z;
            item.transform.position = new Vector3(newX, item.transform.position.y, item.transform.position.z);
        }

        lastX = gameObject.transform.position.x;

    }
}
