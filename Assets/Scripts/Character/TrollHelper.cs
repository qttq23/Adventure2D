using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollHelper : MonoBehaviour
{


    public GameObject boomPrefab;
    // [HideInInspector]
    // public Vector3 deltaBoomScale = new Vector3();
    [HideInInspector]
    public float nScaleTimes = 1f;

    public delegate void OnUltiAtRightPoint();
    public event OnUltiAtRightPoint EventUltiAtRightPoint;
    public delegate void OnUltiDone();
    public event OnUltiDone EventUltiDone;






    public void HandleAttackAtRightPoint()
    {

        print("TrollHelper.cs: attack right point");
        throwBoom();

    }


    public void HandleUltiAtRightPoint()
    {
        print("TrollHelper.cs: attack ulti point");
        EventUltiAtRightPoint?.Invoke();
        throwBoom();
    }


    public void HandleUltiAtEndPoint()
    {
        print("TrollHelper.cs: attack ulti done");
        EventUltiDone?.Invoke();

    }

    void throwBoom()
    {
        // throw a boom
        var controller = gameObject.GetComponent<MyCharacterController>();
        GameObject boom = Instantiate(boomPrefab, 
            transform.position + new Vector3(1 * (controller.isTurningRight ? 1: -1), 0, 0), 
            transform.rotation);
        boom.GetComponent<Rigidbody2D>().velocity = 
        2f * new Vector2(1 *  (controller.isTurningRight ? 1: -1), 0);

        // scale boom
        boom.GetComponent<BombController>().SetScale(nScaleTimes);
        // var scale = boom.GetComponent<Transform>().localScale;
        // var newScale = scale + deltaBoomScale;
        // boom.transform.localScale = newScale;

        // boom.EventObjectInRange += handleObjectInBoomRange;
        // StartCoroutine(waitBallonDestroy(boom.GetComponent<SelfDestroy>().afterSeconds));
    }




}
