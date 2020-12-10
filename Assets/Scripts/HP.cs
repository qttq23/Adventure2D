using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{

	public float currentHealth;
	public float maxHealth;
	public float attack;
	public float defense;

	[HideInInspector]
	public delegate void OnHealthChanged(float value);
	public event OnHealthChanged EventHealthChanged; // event

	void Start(){
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}

	}

    public void ChangeHealth(float value){
    	currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
    	EventHealthChanged?.Invoke(currentHealth);
    	print("currentHealth: " + currentHealth);
    }

}
