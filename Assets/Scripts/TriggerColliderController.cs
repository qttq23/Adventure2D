using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderController : MonoBehaviour
{

	public delegate void OnObjectInOut(GameObject obj, bool isIn);
	public event OnObjectInOut EventObjectInOut;


    void OnTriggerEnter2D(Collider2D collision)
	{
		EventObjectInOut?.Invoke(collision.gameObject, true);
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		EventObjectInOut?.Invoke(collision.gameObject, true);
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		EventObjectInOut?.Invoke(collision.gameObject, false);
	}


}
