using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiRange : MonoBehaviour
{

	[HideInInspector]
	public AutoMoveAttack parent;
	List<Collider2D> colliders = new List<Collider2D>();
	bool isCheck = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		handleObjectInRange(collision);
	}

	void handleObjectInRange(Collider2D collision){
		if (isCheck && GameObject.ReferenceEquals( collision.gameObject, parent.objectToChase))
		{
			if (!colliders.Contains(collision))
			{
				colliders.Add(collision);
				parent.handleObjectInUltiRange(collision.gameObject);
			}

		}
	}

	public void SetCheck(bool isCheck){
		this.isCheck = isCheck;
		colliders.Clear();
	}


}
