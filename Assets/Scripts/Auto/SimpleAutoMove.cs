using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAutoMove : MonoBehaviour
{
	public float speed = 1.5f;
	public float timeToChangeDirection = 2f;
	public string tagNameToDamage = "Player";
	public TriggerColliderController trigger;
	public float damageFrequency = 0.5f;

	Rigidbody2D rigid;
	Vector2 movement = new Vector2(1, 0);
	bool isFacingRight = true;
	HP hp;
	bool canDamage = true;


	void Start()
	{
		rigid = gameObject.GetComponent<Rigidbody2D>();
		StartCoroutine(changeDirection(timeToChangeDirection));

		hp = gameObject.GetComponent<HP>();
		if (hp)
		{
			hp.EventHealthChanged += handleHealthChanged;
		}

		if(trigger){
			trigger.EventObjectInOut += handleObjectCollided;
		}
	}

	void FixedUpdate()
	{
		rigid.velocity = movement * speed;
	}

	IEnumerator changeDirection(float afterSeconds)
	{
		while (true)
		{
			yield return new WaitForSeconds(afterSeconds);

			isFacingRight = !isFacingRight;
			turnRight(isFacingRight);
			movement.x *= -1;
		}
	}

	void turnRight(bool isRight = true)
	{
		var scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void handleHealthChanged(float value)
	{
		if (value == 0)
		{
			StartCoroutine(waitThenDestroy(1.2f));
		}
	}

	IEnumerator waitThenDestroy(float seconds)
	{
		yield return new WaitForSeconds(seconds);

        // Destroy(gameObject);
		gameObject.SetActive(false);
	}


	void handleObjectCollided(GameObject other, bool isIn)
	{	
		if(!isIn) return;

		if (canDamage && other.CompareTag(tagNameToDamage))
		{
			StartCoroutine(blockDamageForSeconds(damageFrequency));

			print("collided, " + hp.attack);
			var otherHp = other.GetComponent<HP>();
			otherHp?.ChangeHealth(-hp.attack);

		}
	}

	IEnumerator blockDamageForSeconds(float afterSeconds){
		canDamage = false;
		yield return new WaitForSeconds(afterSeconds);
		canDamage = true;
	}

}
