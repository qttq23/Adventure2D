using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiDemon : UltiController
{
	public float fastSpeed = 35f;

	public override void Fire(){

		if(!this.canUlti) return;

    	// TODO: custom ulti for each characters
		// print("firing ulti...");
		StartCoroutine(skill());

	}

	IEnumerator skill(){

		// get orign for later use
		var originSpeed = parent.speed;
		var originMovement = parent.movement;

		// prepare
		var direction = new Vector2(1,0);
		if(!parent.IsTurnRight()){
			direction *= -1;
		}

		// move fast forward
		parent.rigid.velocity = direction * fastSpeed;
		yield return new WaitForSeconds(utliDurationTime/2);

		// move fast backward
		parent.rigid.velocity = direction * -1 * fastSpeed;
		yield return new WaitForSeconds(utliDurationTime/2);

		// re-set to origin value
		parent.rigid.velocity = originMovement * originSpeed;

		// signal parent
		this.handleUltiDone();

	}

}
