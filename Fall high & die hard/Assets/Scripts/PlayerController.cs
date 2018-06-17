using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    float jumpSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    float breakFactor;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform antiJump;
    bool grounded;
    //LayerMask layer;
    int layer;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        antiJump = gameObject.transform.GetChild(0);
        layer = LayerMask.GetMask(new string[] { "Ground" });

        Debug.Log(layer);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        bool grounded = Physics2D.Linecast(transform.position, antiJump.position, layer);
        if (grounded)
            animator.SetBool("jumping", false);
        Debug.DrawLine(transform.position, antiJump.position, Color.blue, 0f, false);

        Vector3 acc = rb.velocity;

        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * speed);
            animator.SetTrigger("walking");
        }
        else if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * speed);
            animator.SetTrigger("walking");
        }
        else
        {
            float factor = acc.x * breakFactor;
            rb.velocity = new Vector3(acc.x - factor, acc.y, acc.z);
        }

        if(acc.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, acc.y, acc.z);
        }else if (acc.x < minSpeed)
        {
            rb.velocity = new Vector3(minSpeed, acc.y, acc.z);
        }

            if (Input.GetKeyDown("w") && grounded)
        {
            grounded = false;
            rb.AddForce(Vector2.up * jumpSpeed);
            animator.SetBool("jumping", true);
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("DeathTrap"))
        {
            Debug.Log("fck you and get me dead");
            gameObject.transform.position = new Vector3(0f, 0f, 0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
        }
    }
}
