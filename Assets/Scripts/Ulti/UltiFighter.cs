using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiFighter : UltiController
{

    public Vector3 deltaScale = new Vector3(0.04f, 0.04f, 0.04f);
    public float deltaAttack = 5f;
    public float deltaDefense = 5f;

    public override void Fire()
    {

        if (!this.canUlti) return;

        StartCoroutine(skill());

    }

    IEnumerator skill()
    {

        // get origin for later use
        var scale = parent.gameObject.transform.localScale;
        var hp = parent.gameObject.GetComponent<HP>();

        var originScale = new Vector3(scale.x, scale.y, scale.z);
        var originAttack = hp.attack;
        var originDefense = hp.defense;

        // prepare


        // increase scale
        var newScale = new Vector3( 
            (Mathf.Abs(scale.x) + deltaScale.x) * (scale.x >= 0 ? 1: -1),
            (Mathf.Abs(scale.y) + deltaScale.y) * (scale.y >= 0 ? 1: -1),
            (Mathf.Abs(scale.z) + deltaScale.z) * (scale.z >= 0 ? 1: -1)
            );
        parent.gameObject.transform.localScale = newScale;
        // increase defense + attack
        hp.attack += deltaAttack;
        hp.defense += deltaDefense;

        // effect?????
        // ...............


        // wait some seconds
        yield return new WaitForSeconds(utliDurationTime);


        // re-set to origin value
        scale = parent.gameObject.transform.localScale;
        newScale = new Vector3( 
            Mathf.Abs(originScale.x) * (scale.x >= 0 ? 1: -1),
            Mathf.Abs(originScale.y) * (scale.y >= 0 ? 1: -1),
            Mathf.Abs(originScale.z) * (scale.z >= 0 ? 1: -1)
            );
        parent.gameObject.transform.localScale = newScale;
        hp.attack = originAttack;
        hp.defense = originDefense;


        // signal parent
        this.handleUltiDone();



    }


}
