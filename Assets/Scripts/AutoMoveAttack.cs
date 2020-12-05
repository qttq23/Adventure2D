using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveAttack : MonoBehaviour
{
    public Move move;
    public GameObject objectToChase;
    public UltiCollide ulti;

    // Start is called before the first frame update
    void Start()
    {
    	ulti.parent = this;
        Invoke("chargeUlti", 1f);
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

    // make ulti full for using
    void chargeUlti(){

        print("ready for ulti");
    	ulti.CanUlti(true);
    }

    public void handleObjectInRangeUlti(GameObject obj){

        print("fire ulti");
        
        ulti.CanUlti(false);

    	move.apiUlti();
    	ulti.Fire();
    }
}
