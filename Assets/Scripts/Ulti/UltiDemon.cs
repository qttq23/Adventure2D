using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiDemon : UltiController
{
    public float fastSpeed = 35f;

    public AudioSource audioSource;

    bool isDonex = true;

    public override void Fire()
    {

        if (!this.canUlti) return;

        // TODO: custom ulti for each characters
        // print("firing ulti...");
        StartCoroutine(skill());

    }

    IEnumerator skill()
    {

        // get orign for later use
        var originSpeed = parent.speed;
        var originMovement = parent.Movement;

        // prepare
        var direction = new Vector2(1, 0);
        if (!parent.IsTurnRight())
        {
            direction *= -1;
        }

        /// -----------> fix this: record Ulti animation, trigger script when collide
        // fire weapon
        // parent.weaponCollider.Fire(true);

        // move fast forward

        isDonex = false;
        StartCoroutine(playSound());
        

        parent.Rigid.velocity = direction * fastSpeed;
        yield return new WaitForSeconds(utliDurationTime / 2);

        // move fast backward
        parent.Rigid.velocity = direction * -1 * fastSpeed;
        yield return new WaitForSeconds(utliDurationTime / 2);

        // re-set to origin value
        parent.Rigid.velocity = originMovement * originSpeed;
        // stop weapon
        // parent.weaponCollider.Fire(false);

        isDonex = true;

        // signal parent
        this.handleUltiDone();



    }

    // skill ver2: go through
    // {
    // no gravity, and only trigger
    // 	 var originGravity = parent.Rigid.gravityScale;
    // 	 parent.Rigid.gravityScale = 0;
    // 	 parent.gameObject.GetComponent<Collider2D>().isTrigger = true;
    // parent.weapon.Fire(true);
    // wait forward...
    // parent.weapon.Fire(true); // reset colliders in weapon to continue damage
    // wait backward...
    // reset gravity and trigger
    // }


    IEnumerator playSound(){

        while(!isDonex){
            audioSource.Play();

            yield return new WaitForSeconds(0.25f);

            audioSource.Stop();
        }
    }

}
