using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonEffect : MonoBehaviour
{
	public float increaseStep = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var scale = transform.localScale;
        scale += new Vector3(increaseStep, increaseStep, increaseStep);
        transform.localScale = scale;
    }
}
