using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiMagician : UltiController
{

    public MagicianHelper helper;
    public GameObject magicianEffectPrefab;


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
        var playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerList)
        {
            var magicianEffect = Instantiate(
                magicianEffectPrefab, 
                player.transform.position, 
                player.transform.rotation
                );
            var controller = magicianEffect.GetComponent<MagicianEffectController>();
            controller.objectToHeal = player;
            controller.hpHealEachTime = 0.05f;
            controller.duration = 5f;

        }
    }


    public void HandleUltiDone()
    {
        // signal parent
        print("UltiTroll.cs: handleUltiDone");
        this.handleUltiDone();
    }

}
