using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private float speed;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        if (transform.position.y < -5)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerController>().Death();
        if (collision.gameObject.tag == "Bullet")
        {
            RespawnScript.Instance.UpdateHits();
            Destroy(gameObject);
        }
        

    }
}
