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

	bool isShowingEffect = false;

	void Start(){
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}

	}

	public void ChangeHealth(float value){

		// apply defense then apply to current health
		value = applyDefense(value);
		if(value < 0){
			StartCoroutine(showDamageEffect(0.2f));
		}
		currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);

		// signal UI component
		EventHealthChanged?.Invoke(currentHealth);
		print("HP: currentHealth: " + currentHealth);

		// show long effect if die
		if(currentHealth <= 0){
			StartCoroutine(showDamageEffect(3f));
		}

	}

	float applyDefense(float value){
		if(value >= 0 ) return value;

		float applied = value + defense;
		float smallDamage = -0.3f;
		return applied >= 0 ? smallDamage : applied;
	}

	IEnumerator showDamageEffect(float seconds){

		while(isShowingEffect){
			yield return new WaitForSeconds(0.1f);
		}
		isShowingEffect = true;

		// set color of sprite
		var renderer = gameObject.GetComponent<SpriteRenderer>();
		var whiteColor = new Color(255, 255, 255, 255);

		renderer.color = new Color(255, 0, 0, 255);

		// wait a time then re-set to origin color
		yield return new WaitForSeconds(seconds);
		renderer.color = whiteColor;

		isShowingEffect = false;
	}

}
