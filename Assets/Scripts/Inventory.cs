using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	public int numCoin = 0;
	public delegate void OnCoinChanged(int newValue);
	public event OnCoinChanged EventCoinChanged;

	void OnTriggerEnter2D(Collider2D collision)
	{
		Collectable item = collision.gameObject.GetComponent<Collectable>();
		if(item && item.type == Collectable.ItemType.COIN){
			changeNumCoin(1);

			Destroy(collision.gameObject);
		}

	}

	void changeNumCoin(int value){
		numCoin = numCoin + value;
		// signal
		EventCoinChanged?.Invoke(numCoin);
		print("Inventory.cs: numCoin: " + numCoin);
	}

}
