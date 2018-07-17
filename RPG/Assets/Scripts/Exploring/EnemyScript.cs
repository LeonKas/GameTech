using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {

    [SerializeField]
    Transform player;
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D rb;


    int layer;
    Vector3 lastPlayerPos;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt(gameObject.name) == 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            layer = LayerMask.GetMask(new string[] { "Wall" });
            lastPlayerPos = transform.position;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 diffVector = player.position - transform.position;
        float dist = diffVector.magnitude;
        bool sightBlocked = Physics2D.CircleCast(transform.position, 0.4f, player.position-transform.position, dist, layer);
        Vector3 targetPosition;
        if (!sightBlocked)
        {
            targetPosition = player.position;
            lastPlayerPos = player.position;
        }
        else
        {
            if ((transform.position - lastPlayerPos).magnitude < 0.2f)
            {
                targetPosition = transform.position;
            }
            else
            {
                targetPosition = lastPlayerPos;
            }
        }
        rb.velocity = (targetPosition - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerPrefs.SetInt(gameObject.name, 1);
        PlayerPrefs.SetFloat("playerX", collision.transform.position.x);
        PlayerPrefs.SetFloat("playerY", collision.transform.position.y);
        SceneManager.LoadScene("Battle");
    }
}
