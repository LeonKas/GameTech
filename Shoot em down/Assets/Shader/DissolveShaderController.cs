using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShaderController : MonoBehaviour {

    [SerializeField] float increasePerTick;
    [SerializeField] float waitTime;

    [SerializeField] KeyCode dissolveKey;

    MeshRenderer mr;
    MaterialPropertyBlock mpb;

	void Start () {
        mr = GetComponent<MeshRenderer>();
        mpb = new MaterialPropertyBlock();
        mr.GetPropertyBlock(mpb);
        mpb.SetFloat("_DissolveVal", 0f);
        mr.SetPropertyBlock(mpb);
	}
	
	void Update () {
        if (Input.GetKeyDown(dissolveKey))
        {
            StartCoroutine(Dissolve());

        }
	}

    public IEnumerator Dissolve()
    {
        float value = 0;
        while(value < 1)
        {
            value += increasePerTick;
            mpb.SetFloat("_DissolveVal", Mathf.Clamp01(value));
            mr.SetPropertyBlock(mpb);
            yield return new WaitForSeconds(waitTime);

        }
        yield return null;
    }
}
