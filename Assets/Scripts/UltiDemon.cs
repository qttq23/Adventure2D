using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiDemon : UltiController
{


	public override void Fire(){

    	// TODO: custom ulti for each characters

		print("firing ulti...");
		// var targetTransform = parent.move.gameObject.GetComponent<Transform>();
    	// print(target.name);
    	// targetTransform.Rotate(0, 0, 90);
		StartCoroutine(skill());

	}

	IEnumerator skill(){

		var originSpeed = parent.move.speed;
		var originMovement = parent.move.movement;

		// var rigid = parent.move.gameObject.GetComponent<Rigidbody2D>();
		// var collider = parent.move.gameObject.GetComponent<BoxCollider2D>();
		// var originGravity = rigid.gravityScale;
		// rigid.gravityScale = 0;
		// collider.isTrigger = true;

		parent.move.movement = new Vector2(-1, 0);
		parent.move.speed = 20f;
		yield return new WaitForSeconds(utliDurationTime/2);

		parent.move.movement = new Vector2(1, 0);
		parent.move.speed = 20f;
		yield return new WaitForSeconds(utliDurationTime/2);

		parent.move.movement = originMovement;
		parent.move.speed = originSpeed;

		// rigid.gravityScale = originGravity;
		// collider.isTrigger = false;


		parent.handleUltiDone();

	}

}
