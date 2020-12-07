using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiKnight : UltiController
{

	public GameObject balloonPrefab;
	float originGravity;

	public override void Fire(){

		if(!this.canUlti) return;

    	// TODO: custom ulti for each characters
		print("firing ulti...");
		
		// set no gravity
		originGravity = parent.rigid.gravityScale;
		parent.rigid.gravityScale = 0;
		parent.rigid.velocity = new Vector2(0, 0);

		// create effect
		var ballon = Instantiate(balloonPrefab, transform.position, transform.rotation);
		StartCoroutine(waitBallonDestroy(ballon.GetComponent<SelfDestroy>().afterSeconds));
	}

	IEnumerator waitBallonDestroy(float seconds){

		yield return new WaitForSeconds(seconds);
		// re-set gravity
		parent.rigid.gravityScale = originGravity;

		// signal parent
		this.handleUltiDone();

	}

}
