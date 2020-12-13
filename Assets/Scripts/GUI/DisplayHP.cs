using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{

	public HP hpToDisplay;
	public Image imgBar;
	public Text txtQuantity;


    // Start is called before the first frame update
    void Start()
    {
        if(hpToDisplay){
        	hpToDisplay.EventHealthChanged += handleHealthChanged;
        	display();
        }

        StartCoroutine(changeHealth());
    }

    IEnumerator changeHealth(){
    	yield return new WaitForSeconds(2f);
    	hpToDisplay.ChangeHealth(5);
    }

    void handleHealthChanged(float value){
    	print("from DisplayHP: " + value);
    	display();
    }

    void display(){
    	imgBar.fillAmount = hpToDisplay.currentHealth/ hpToDisplay.maxHealth;
        txtQuantity.text = "" + Mathf.Round(imgBar.fillAmount * 100);
    }


}
