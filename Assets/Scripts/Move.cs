﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    // public float speed = 2f;
    // public float jumpHeight = 2.5f;
    // public float timeJump = 0.3f;
    // public float timeAttack = 0.8f;
    // public float timeAttackDelay = 0.2f;
    // public float timeBlockWhenDamaged = 0.5f;
    // public float timeShowDealth = 2f;

    // // stub
    // public FootCollide footCollide;
    // public Weapon weapon;
    // public UltiController ultiController;
    // public bool isUsedByAutoMove = false;
    // public List<string> tagsIgnoredDamage = new List<string>();

    // [HideInInspector]
    // public Rigidbody2D rigid;
    // [HideInInspector]
    // public Vector2 movement;
    // Animator animator;
    // enum MoveType
    // {
    //     idle = 0,
    //     walk = 1,
    //     jump = 2,
    //     attack = 3,
    //     util = 4,
    //     die = 5,
    //     hurt = 6
    // }
    // int countJump = 0;
    // int countJumpTemp = 0;
    // bool isFootCollide = false;
    // bool isAttack = false;
    // bool isUlti = false;
    // bool canMove = true;
    // bool isDie = false;
    // HP hp;
    // float lastHealth;
    // int hurtCount = 0;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     rigid = gameObject.GetComponent<Rigidbody2D>();
    //     animator = gameObject.GetComponent<Animator>();
    //     movement = new Vector2();

    //     footCollide.parent = this;
    //     weapon.parent = this;
    //     if (ultiController)
    //     {
    //         ultiController.parent = this;
    //     }

    //     hp = gameObject.GetComponent<HP>();
    //     if (hp)
    //     {
    //         lastHealth = hp.currentHealth;
    //         hp.EventHealthChanged += handleHealthChanged;
    //     }
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (!isUsedByAutoMove)
    //     {
    //         // get ulti input
    //         if (Input.GetKeyDown(KeyCode.Mouse1) && !isUlti)
    //         {
    //             apiUlti();
    //         }

    //         // get jump input
    //         if (Input.GetKeyDown(KeyCode.Space) && countJump < 2)
    //         {
    //             countJump++;
    //             countJumpTemp++;
    //             StartCoroutine(jump(timeJump));
    //         }

    //         // get attack input
    //         if (Input.GetKey(KeyCode.Mouse0) && !isAttack)
    //         {
    //             isAttack = true;
    //             StartCoroutine(attack(timeAttack, timeAttackDelay));
    //         }

    //         // get move input
    //         movement.x = Input.GetAxisRaw("Horizontal");

    //     }

    //     // show animattion
    //     if (isDie)
    //     {
    //         animator.SetInteger("moveType", (int)MoveType.die);
    //     }
    //     else if(hurtCount > 0){
    //         animator.SetInteger("moveType", (int)MoveType.hurt);
    //     }
    //     else if (isUlti)
    //     {
    //         animator.SetInteger("moveType", (int)MoveType.util);

    //     }
    //     else if (isAttack)
    //     {
    //         animator.SetInteger("moveType", (int)MoveType.attack);

    //         // can turn right/left while attacking
    //         if (movement.x > 0)
    //         {
    //             turnRight();
    //         }
    //         else if (movement.x < 0)
    //         {
    //             turnRight(false);
    //         }

    //     }

    //     else if (movement.x > 0)
    //     {
    //         turnRight();
    //         animator.SetInteger("moveType", (int)MoveType.walk);
    //     }
    //     else if (movement.x < 0)
    //     {
    //         turnRight(false);
    //         animator.SetInteger("moveType", (int)MoveType.walk);
    //     }
    //     else if (countJump > 0)
    //     {
    //         animator.SetInteger("moveType", (int)MoveType.jump);
    //     }
    //     else
    //     {
    //         animator.SetInteger("moveType", (int)MoveType.idle);
    //     }


    // }


    // void FixedUpdate()
    // {
    //     if (!canMove || isDie) return;

    //     // actually move the object
    //     // movement.Normalize();
    //     rigid.velocity = movement * speed;

    // }


    // IEnumerator jump(float timeUp)
    // {

    //     // if double jump, increase the time jump, down
    //     if (countJump == 2)
    //     {
    //         timeUp *= 1.75f;
    //     }

    //     // jump up
    //     movement.y = jumpHeight;
    //     isFootCollide = false;
    //     yield return new WaitForSeconds(timeUp);


    //     // time in the air, when gravity pull object down
    //     movement.y = 0;
    //     while (!isFootCollide)
    //     {
    //         yield return new WaitForSeconds(0.1f);
    //     }

    //     countJumpTemp--;
    //     if (countJumpTemp == 0)
    //     {
    //         countJump = 0;
    //     }
    // }

    // IEnumerator attack(float seconds, float delaySeconds)
    // {
    //     // delay time
    //     yield return new WaitForSeconds(delaySeconds);

    //     // some padding time for weapon to collide in view
    //     yield return new WaitForSeconds(seconds * 4 / 5);

    //     // real damage
    //     weapon.Fire(true);
    //     yield return new WaitForSeconds(seconds * 1 / 5);
        
    //     // done, turn off attack
    //     isAttack = false;
    //     weapon.Fire(false);
    // }

    // IEnumerator ulti(float seconds)
    // {
    //     // time for display animation attack
    //     canMove = false;
    //     yield return new WaitForSeconds(seconds);
    //     isUlti = false;
    //     canMove = true;
    // }


    // void turnRight(bool isRight = true)
    // {
    //     var scale = transform.localScale;
    //     scale.x = Mathf.Abs(scale.x) * (isRight ? 1 : -1);
    //     transform.localScale = scale;
    // }

    // public bool IsTurnRight()
    // {
    //     var scale = transform.localScale;
    //     return scale.x > 0;

    // }

    // // child foot object collided, and signal parent
    // public void handleFootCollided()
    // {
    //     isFootCollide = true;
    // }

    // public void handleWeaponCollided(GameObject other)
    // {
    //     if(isDie || hurtCount > 0) return;

    //     print("" + gameObject.name + " attack: " + other.name);

    //     // check tags
    //     if (tagsIgnoredDamage.Contains(other.tag)) return;

    //     // then damage enemies if needed...
    //     var hp = gameObject.GetComponent<HP>();
    //     var otherHp = other.GetComponent<HP>();
    //     if (!hp || !otherHp) return;

    //     otherHp.ChangeHealth(-1 * hp.attack);

    // }

    // public void handleObjectInUltiRange(GameObject other)
    // {
    //     if(isDie || hurtCount > 0) return;

    //     // check tags
    //     if (tagsIgnoredDamage.Contains(other.tag)) return;

    //     var otherHp = other.GetComponent<HP>();
    //     if (!otherHp) return;

    //     print("Move.cs: damage: " + other.name);
    //     otherHp.ChangeHealth(-1 * ultiController.ultiDamage);
    // }


    // // api for AutoMoveAttack
    // public void apiGoRight(bool isRight = true)
    // {

    //     if (isRight)
    //     {
    //         movement.x = 1;
    //     }
    //     else
    //     {
    //         movement.x = -1;
    //     }
    // }

    // public void apiAttack()
    // {
    //     if (!isAttack)
    //     {
    //         isAttack = true;
    //         StartCoroutine(attack(timeAttack, timeAttackDelay));

    //     }
    // }

    // public void apiUlti()
    // {
    //     if (isUlti || !ultiController.CanUlti()) return;

    //     // flag to show animation
    //     isUlti = true;
    //     StartCoroutine(ulti(ultiController.utliDurationTime));
    //     ultiController.Fire();

    // }

    // public void handleUltiDone()
    // {
    //     // print("move: ulti is done");
    // }

    // public void apiDie()
    // {

    //     // show anim
    //     isDie = true;

    //     // wait some seconds then destroy
    //     StartCoroutine(waitThenDestroy(timeShowDealth));

    // }

    // IEnumerator waitThenDestroy(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);

    //     // Destroy(gameObject);
    //     gameObject.SetActive(false);
    // }

    // public void handleHealthChanged(float newHealth)
    // {

    //     print("" + gameObject.name + ": " + newHealth );

    //     if (newHealth <= 0)
    //     {
    //         apiDie();
    //         return;
    //     }

    //     float delta = newHealth - lastHealth;
    //     lastHealth = newHealth;
    //     print("" + gameObject.name + ", delta: " + delta );
    //     if (delta < 0)
    //     {
    //         apiHurt();
    //         return;
    //     }

    // }

    // public void apiHurt()
    // {
    //     // show anim
    //     // character stops all damage (attack, ulti) to enemies 
    //     hurtCount++;

    //     // wait some seconds then recover
    //     StartCoroutine(waitThenRecover(timeBlockWhenDamaged));
    // }

    // IEnumerator waitThenRecover(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);

    //     hurtCount--;
    // }

}
