using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiTroll : UltiController
{

    public Vector3 deltaBoomScale = new Vector3(0.04f, 0.04f, 0.04f);
    public float deltaAttack = 5f;

    void Start()
    {

        parent.EventUltiAtRightPoint += HandleUltiAtRightPoint;
        parent.EventUltiDone += HandleUltiDone;
    }

    public override void Fire()
    {

        if (!this.canUlti) return;

        // increase attack + boom scale
        var hp = parent.gameObject.GetComponent<HP>();
        hp.attack += deltaAttack;

        parent.deltaBoomScale = this.deltaBoomScale;

    }



    public void HandleUltiAtRightPoint()
    {
    }


    public void HandleUltiDone()
    {
        // re-set attack + boom scale
        var hp = parent.gameObject.GetComponent<HP>();
        hp.attack -= deltaAttack;
        parent.deltaBoomScale = new Vector3();

        // signal parent
        this.handleUltiDone();
    }

}
