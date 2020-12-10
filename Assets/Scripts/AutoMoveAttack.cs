using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveAttack : MonoBehaviour
{
    public Move move;
    public GameObject objectToChase;
    public WeaponRange weaponRange;
    public UltiRange ultiRange;

    [HideInInspector]
    public bool isDoingUlti = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponRange.parent = this;

        if(ultiRange){
           ultiRange.parent = this;
           ultiRange.SetCheck(true);
            
        }
    }

    void FixedUpdate()
    {
        if(isDoingUlti) return;

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

    public void handleObjectInWeaponRange(GameObject obj)
    {
        if(isDoingUlti) return;

        move.apiAttack();
    }


    public void handleObjectInUltiRange(GameObject obj){

        if(!move.ultiController.CanUlti()) return;

        // print("fire ulti");
        isDoingUlti = true;
        ultiRange.SetCheck(false);

        move.apiUlti();
        StartCoroutine(waitUltiDone(move.ultiController.utliDurationTime));
        
    }

    IEnumerator waitUltiDone(float seconds){

        yield return new WaitForSeconds(seconds);
        // print("ulti done");
        isDoingUlti = false; 
        ultiRange.SetCheck(true);
    }


}
