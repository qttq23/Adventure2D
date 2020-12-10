using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoin : MonoBehaviour
{
	public Inventory inventory;
	public Text txtQuantity;

    // Start is called before the first frame update
    void Start()
    {
    	txtQuantity.text = "0";

    	if(inventory){
    		inventory.EventCoinChanged += handleCoinChanged;
    	}    
    }

    void handleCoinChanged(int newValue){
    	txtQuantity.text = "" + newValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
