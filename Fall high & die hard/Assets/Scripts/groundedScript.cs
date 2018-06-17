using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundedScript : MonoBehaviour
{

    public bool grounded = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Ihr habt geleutet?");
        if (collision.gameObject.tag.Equals("ground"))
        {
            Debug.Log("fck you and get me grounded");
            grounded = true;
        }
    }
}
