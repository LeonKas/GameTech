using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform bulletDaddy;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float cooldownTime;
    private float cooldown;
    private bool soFckingDead;

    [SerializeField] float increasePerTick;
    [SerializeField] float waitTime;

    [SerializeField] KeyCode dissolveKey;

    MeshRenderer mr;
    MaterialPropertyBlock mpb;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mpb = new MaterialPropertyBlock();
        mr.GetPropertyBlock(mpb);
        mpb.SetFloat("_DissolveVal", 0f);
        mr.SetPropertyBlock(mpb);
        soFckingDead = false;
    }

    // Update is called once per frame
    void Update() {
        if (cooldown > 0)
            cooldown--;

        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }

        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }

        if (Input.GetKey("b"))
        {
            if (cooldown == 0)
            {
                GameObject obj = Instantiate(bullet);
                obj.transform.position = bulletDaddy.position;
                cooldown = cooldownTime;
            }
        }




    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 6f), transform.position.z);
        if (transform.position.x > 8 || transform.position.x < -8) {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
        }

        if (soFckingDead)
        {
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        StartCoroutine(Dissolve());
    }

    public IEnumerator Dissolve()
    {
        float value = 0;
        while (value < 1)
        {
            value += increasePerTick;
            mpb.SetFloat("_DissolveVal", Mathf.Clamp01(value));
            mr.SetPropertyBlock(mpb);
            yield return new WaitForSeconds(waitTime);

        }
        soFckingDead = true;
        yield return null;
    }
}

