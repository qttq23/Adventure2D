﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{

	// [HideInInspector]
	// public AutoMoveAttack parent;

	public delegate void OnObjectIn(GameObject obj);
	public event OnObjectIn EventObjectIn;

	void OnTriggerEnter2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void handleObjectInRange(Collider2D collision){
		// if(!enabled || parent.isDoingUlti) return;
		if(!enabled) return;

		// if (GameObject.ReferenceEquals( collision.gameObject, parent.objectToChase))
		// {
		// 	parent.handleObjectInWeaponRange(collision.gameObject);
		// }
		EventObjectIn?.Invoke(collision.gameObject);
	}


}
