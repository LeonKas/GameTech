using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmurfPowerScript : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") {
            collision.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Destroy(gameObject);
        }

    }
}
