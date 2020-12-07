using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 2.5f;
    public float timeJump = 0.3f;
    public float timeAttack = 0.8f;

    // stub
    public FootCollide footCollide;
    public Weapon weapon;
    public bool isUsedByAutoMove = false;

    Rigidbody2D rigid;
    [HideInInspector]
    public Vector2 movement;
    Animator animator;
    enum MoveType
    {
        idle = 0,
        walk = 1,
        jump = 2,
        attack = 3,
        util = 4
    }
    int countJump = 0;
    int countJumpTemp = 0;
    bool isFootCollide = false;
    bool isAttack = false;
    bool isUlti = false;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        movement = new Vector2();

        footCollide.parent = this;
        weapon.parent = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsedByAutoMove)
        {
            // get jump input
            if (Input.GetKeyDown(KeyCode.Space) && countJump < 2)
            {
                countJump++;
                countJumpTemp++;
                StartCoroutine(jump(timeJump));
            }

            // get attack input
            if (Input.GetKey(KeyCode.Mouse0) && !isAttack)
            {
                isAttack = true;
                StartCoroutine(attack(timeAttack));
            }

            // get move input
            movement.x = Input.GetAxisRaw("Horizontal");

        }

        // show animattion
        if(isUlti){
            animator.SetInteger("moveType", (int)MoveType.util);

        }
        else if (isAttack)
        {
            animator.SetInteger("moveType", (int)MoveType.attack);

            // can turn right/left while attacking
            if(movement.x > 0){
                turnRight();
            }
            else if(movement.x < 0){
                turnRight(false);
            }

        }
        else if (countJump > 0)
        {
            animator.SetInteger("moveType", (int)MoveType.jump);
        }
        else if (movement.x > 0)
        {
            turnRight();
            animator.SetInteger("moveType", (int)MoveType.walk);
        }
        else if (movement.x < 0)
        {
            turnRight(false);
            animator.SetInteger("moveType", (int)MoveType.walk);
        }
        else
        {
            animator.SetInteger("moveType", (int)MoveType.idle);
        }


    }


    void FixedUpdate()
    {
        if(!canMove) return;

        // actually move the object
        rigid.velocity = movement * speed;

    }


    IEnumerator jump(float timeUp)
    {

        // if double jump, increase the time jump, down
        if (countJump == 2)
        {
            timeUp *= 1.75f;
        }

        // jump up
        movement.y = jumpHeight;
        isFootCollide = false;
        yield return new WaitForSeconds(timeUp);


        // time in the air, when gravity pull object down
        movement.y = 0;
        while (!isFootCollide)
        {
            yield return new WaitForSeconds(0.1f);
        }

        countJumpTemp--;
        if (countJumpTemp == 0)
        {
            countJump = 0;
        }
    }

    IEnumerator attack(float seconds)
    {
        weapon.Fire(true);

        // time for display animation attack
        yield return new WaitForSeconds(seconds);
        isAttack = false;
        weapon.Fire(false);
    }

    IEnumerator ulti(float seconds)
    {
        // time for display animation attack
        // canMove = false;
        yield return new WaitForSeconds(seconds);
        isUlti = false;
        canMove = true;
    }


    void turnRight(bool isRight = true)
    {
        var scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isRight ? 1 : -1);
        transform.localScale = scale;
    }

    // child foot object collided, and signal parent
    public void handleFootCollided()
    {
        isFootCollide = true;
    }

    public void handleWeaponCollided(GameObject gameObj = null)
    {
        print("weapon collided: " + gameObj.name);

        // then damage enemies if needed...
    }


    // api for AutoMoveAttack
    public void apiGoRight(bool isRight = true)
    {

        if (isRight)
        {
            movement.x = 1;
        }
        else
        {
            movement.x = -1;
        }
    }

    public void apiAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            StartCoroutine(attack(timeAttack));

        }
    }

    public void apiUlti(float utliDurationTime){
        if(isUlti) return;

        // flag to show animation
        isUlti = true;
        StartCoroutine(ulti(utliDurationTime));

    }

}
