using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiDragon : UltiController
{

    public DragonHelper helper;


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
    }


    public void HandleUltiDone()
    {


        // signal parent
        print("UltiDragon.cs: handleUltiDone");
        this.handleUltiDone();
    }

}
