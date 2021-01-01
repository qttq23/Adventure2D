using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiTroll : UltiController
{

    public TrollHelper helper;

    // public Vector3 deltaBoomScale = new Vector3(0.6f, 0.6f, 0.6f);
    public float nScaleTimes = 2f;
    public float deltaAttack = 5f;

    protected void Start()
    {
        base.Start();
        helper.EventUltiAtRightPoint += HandleUltiAtRightPoint;
        helper.EventUltiDone += HandleUltiDone;
    }

    public override void Fire()
    {

        if (!this.canUlti) return;

        // increase attack + boom scale
        var hp = helper.gameObject.GetComponent<HP>();
        hp.attack += deltaAttack;

        // helper.deltaBoomScale = this.deltaBoomScale;
        helper.nScaleTimes = nScaleTimes;

    }



    public void HandleUltiAtRightPoint()
    {
    }


    public void HandleUltiDone()
    {
        // re-set attack + boom scale
        var hp = helper.gameObject.GetComponent<HP>();
        hp.attack -= deltaAttack;
        // helper.deltaBoomScale = new Vector3();
        helper.nScaleTimes = 1f;

        // signal parent
        print("UltiTroll.cs: handleUltiDone");
        this.handleUltiDone();
    }

}
