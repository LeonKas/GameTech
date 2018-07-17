using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    /* [SerializeField]
     float stepDistance;*/
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Text winText;

    Vector2 moveVec;

	// Use this for initialization
	void Start () {
        winText.text = "";
        float x = PlayerPrefs.GetFloat("playerX", transform.position.x);
        float y = PlayerPrefs.GetFloat("playerY", transform.position.y);
        transform.position = new Vector2(x, y);
    }
	
	// Update is called once per frame
	void Update () {
       
        moveVec = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveVec += Vector2.up;
        if (Input.GetKey(KeyCode.A)) moveVec += Vector2.left;
        if (Input.GetKey(KeyCode.S)) moveVec += Vector2.down;
        if (Input.GetKey(KeyCode.D)) moveVec += Vector2.right;
        moveVec = moveVec.normalized * speed;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVec;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Goal")
        {
            winText.text = "You won! Yey";
            Debug.Log("Hi");
        }
    }
}
