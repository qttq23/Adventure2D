using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonEffect : MonoBehaviour
{
	public float increaseStep = 0.025f;

    List<Collider2D> colliders = new List<Collider2D>();

    public delegate void OnObjectInRange(GameObject obj);
    public event OnObjectInRange EventObjectInRange;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        handleObjectInRange(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleObjectInRange(collision);
    }

    void handleObjectInRange(Collider2D collision){
        if (!colliders.Contains(collision))
        {
            colliders.Add(collision);
            // parent.HandleObjectInBallonRange(collision.gameObject);
            EventObjectInRange?.Invoke(collision.gameObject);
        }
    }
}
