using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour {

    public static RespawnScript Instance = null;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text countText;
    private int count;
    

	// Use this for initialization
	void Start () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        count =-1;
        UpdateHits();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r") && GameObject.FindGameObjectWithTag("Player") == null) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies) {
                GameObject.Destroy(enemy);
            }
            count = -1;
            UpdateHits();
            Instantiate(player);
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void UpdateHits() {
        count++;
        countText.text = "Hits: " + count.ToString();
    }
}
