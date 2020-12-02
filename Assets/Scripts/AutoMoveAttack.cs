using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveAttack : MonoBehaviour
{
    public Move move;
    public GameObject objectToChase;

    // Start is called before the first frame update
    void Start()
    {
        // move = gameObject.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        // detect position to chase
        if (transform.position.x < objectToChase.transform.position.x)
        {
            move.apiGoRight(true);
        }
        else
        {
            move.apiGoRight(false);

        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        handleObjectInRange(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleObjectInRange(collision);
    }

    void handleObjectInRange(Collider2D collision)
    {
    	if(!enabled) return;

        if (GameObject.ReferenceEquals( collision.gameObject, objectToChase))
        {
            move.apiAttack();
        }
    }

}
