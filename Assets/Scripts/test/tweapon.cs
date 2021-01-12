using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tweapon : MonoBehaviour
{
	// Collider2D collider;
	List<Collider2D> colliders = new List<Collider2D>();

	// void Start(){
	// 	collider = gameObject.GetComponent<Collider2D>();
	// }

	void OnTriggerEnter2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void handleObjectInRange(Collider2D collision){
		if (enabled)
		{
			if (!colliders.Contains(collision))
			{
				colliders.Add(collision);
				print("tweapon.cs: collider: " + collision.gameObject.name);
				// parent.handleObjectInUltiRange(collision.gameObject);
			}

		}
	}

	void OnEnable()
	{
		colliders.Clear();
		Debug.Log("tweapon.cs:PrintOnEnable: script was enabled");
	}

	void OnDisable()
	{
		colliders.Clear();
		Debug.Log("tweapon.cs:PrintOnDisable: script was disabled");
	}
}
