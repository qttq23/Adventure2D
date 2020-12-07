using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
	public float afterSeconds = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoDestroy(afterSeconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoDestroy(float afterSeconds){
    	yield return new WaitForSeconds(afterSeconds);
    	Destroy(gameObject);
    	print("destroyed");
    }

}
