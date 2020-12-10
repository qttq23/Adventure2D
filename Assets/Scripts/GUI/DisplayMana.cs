using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMana : MonoBehaviour
{

	public UltiController ulti;
	public Image imgBar;

    // Start is called before the first frame update
    void Start()
    {
        if(ulti){
        	ulti.EventPercentUltiChanged += handlePercentUltiChanged;
        	display();
        }

    }

    void handlePercentUltiChanged(float value){
    	// print("from Display Mana: " + value);
    	display();
    }

    void display(){
    	imgBar.fillAmount = ulti.PercentUlti;
    }


}
