using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianHelper : MonoBehaviour
{


    public GameObject ballPrefab;
    public delegate void OnUltiAtRightPoint();
    public event OnUltiAtRightPoint EventUltiAtRightPoint;
    public delegate void OnUltiDone();
    public event OnUltiDone EventUltiDone;






    public void HandleAttackAtRightPoint()
    {

        print("MagicianHelper.cs: attack right point");
        throwBall();

    }


    public void HandleUltiAtRightPoint()
    {
        print("MagicianHelper.cs: ulti right point");
        EventUltiAtRightPoint?.Invoke();
    }


    public void HandleUltiAtEndPoint()
    {
        print("MagicianHelper.cs: attack ulti done");
        EventUltiDone?.Invoke();

    }

    void throwBall()
    {
        // throw a ball
        var controller = gameObject.GetComponent<MyCharacterController>();

        GameObject ball = Instantiate(ballPrefab, 
            transform.position + new Vector3(0.25f * (controller.isTurningRight ? 1: -1), 0, 0), 
            transform.rotation);

        var ballController = ball.GetComponent<BallController>();
        ballController.direction = new Vector2(1 * (controller.isTurningRight ? 1: -1), 0);
        ballController.speed = 2f;
        ballController.damage = gameObject.GetComponent<HP>().attack;
        ballController.RealStart();
    }




}
