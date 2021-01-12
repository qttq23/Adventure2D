using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour
{

    public delegate void OnObjectIn(GameObject obj);
    public event OnObjectIn EventObjectIn;

    // [HideInInspector]
    // public Move parent;
   
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    // 	// tell parent object
    // 	// var move = gameObject.transform.parent.gameObject.GetComponent<Move>();
    // 	parent.handleFootCollided();
    // }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Collider2D>().isTrigger) return;
        
        // tell parent object
        // parent.handleFootCollided();
        EventObjectIn?.Invoke(collision.gameObject);
    }

}
