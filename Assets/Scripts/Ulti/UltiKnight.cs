using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiKnight : UltiController
{

    public BallonEffect balloonPrefab;


    float originGravity;

    public override void Fire()
    {

        if (!this.canUlti) return;

        // TODO: custom ulti for each characters
        print("firing ulti...");

        // set no gravity to lock character's position
        originGravity = parent.Rigid.gravityScale;
        parent.Rigid.gravityScale = 0;
        parent.Rigid.velocity = new Vector2(0, 0);

        // create effect
        BallonEffect ballon = Instantiate(balloonPrefab, transform.position, transform.rotation);
        ballon.EventObjectInRange += handleObjectInBallonRange;
        StartCoroutine(waitBallonDestroy(ballon.GetComponent<SelfDestroy>().afterSeconds));
    }

    IEnumerator waitBallonDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        print("UltiKnight.cs: ballon destroyed");

        // re-set gravity
        parent.Rigid.gravityScale = originGravity;

        // signal parent
        this.handleUltiDone();

    }


    void handleObjectInBallonRange(GameObject obj)
    {
        // parent.handleObjectInUltiRange(obj);
        // EventObjectInUltiRange?.Invoke(obj);
        this.handleObjectInRange(obj);

    }

}
