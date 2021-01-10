using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiMagician : UltiController
{

    public MagicianHelper helper;
    public GameObject lazerPrefab;


    protected void Start()
    {
        base.Start();
        helper.EventUltiAtRightPoint += HandleUltiAtRightPoint;
        helper.EventUltiDone += HandleUltiDone;
    }

    public override void Fire()
    {

        if (!this.canUlti) return;



    }



    public void HandleUltiAtRightPoint()
    {
        // throw a lazer
        GameObject lazer = Instantiate(lazerPrefab,
            transform.position + new Vector3(0.25f * (parent.isTurningRight ? 1 : -1), 0, 0),
            transform.rotation);

        var lazerController = lazer.GetComponent<LazerController>();
        lazerController.direction = new Vector2(1 * (parent.isTurningRight ? 1 : -1), 0);
        lazerController.duration = 5f;
        lazerController.damage = this.ultiDamage;
        lazerController.RealStart();
    }


    public void HandleUltiDone()
    {
        // signal parent
        print("UltiTroll.cs: handleUltiDone");
        this.handleUltiDone();
    }

}
