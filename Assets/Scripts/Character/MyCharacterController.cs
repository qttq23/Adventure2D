using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
public class MyCharacterController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 2.5f;
    public float timeJump = 0.3f;
    public float timeAttack = 0.8f;
    public float timeAttackDelay = 0.2f;
    public float timeBlockWhenDamaged = 0.5f;
    public float timeShowDealth = 2f;

    public bool isTurningRight = true;
    public int currentAvailableAttack = 3;
    public int numMaxContinueAttack = 3;
    public int numAttackRecoverEachSec = 1;
    public float timeToRecoverOneAttack = 2.5f;

    // stub
    public FootCollider footCollider;
    public WeaponCollider weaponCollider;
    public UltiController ultiController;
    public bool isUsedByAutoMove = false;
    public bool isUsedByRemote = true;
    public List<string> tagsIgnoredDamage = new List<string>();


    public AudioSource audioSourceFoot;
    public AudioSource audioSourceQuick;
    public AudioClip audioAttack;
    public AudioClip audioHurt;
    public AudioClip audioJump;

    // properties
    public Rigidbody2D Rigid { get { return rigid; } }
    public Vector2 Movement { get { return movement; } }

    public int Id;
    public delegate void OnMove(int id, int moveType, Vector2 position = new Vector2());
    public event OnMove EventMove;


    // internal data
    Vector2 movement;
    Rigidbody2D rigid;
    Animator animator;
    public enum MoveType
    {
        idle = 0,
        walk = 1,
        jump = 2,
        attack = 3,
        ulti = 4,
        die = 5,
        hurt = 6
    }
    int countJump = 0;
    int countJumpTemp = 0;
    bool isFootCollide = false;
    bool isAttack = false;
    bool isUlti = false;
    bool canMove = true;
    bool isDie = false;
    bool isAnimationHurt = false;
    bool isAnimationAttack = false;
    HP hp;
    float lastHealth;
    int hurtCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        movement = new Vector2();

        // footCollider.parent = this;
        // weaponCollider.parent = this;
        footCollider.EventObjectIn += handleFootCollided;
        weaponCollider.EventObjectIn += handleWeaponCollided;

        if (ultiController)
        {
            ultiController.parent = this;
            ultiController.EventUltiDone += handleUltiDone;
            ultiController.EventObjectInUltiRange += handleObjectInUltiRange;
        }

        hp = gameObject.GetComponent<HP>();
        if (hp)
        {
            lastHealth = hp.currentHealth;
            hp.EventHealthChanged += handleHealthChanged;
        }

        // start recover for attack
        StartCoroutine(waitThenRecoverAttack(timeToRecoverOneAttack));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsedByAutoMove && !isUsedByRemote)
        {
            getUserInput();
        }

        showAnimation();
    }

    void getUserInput()
    {
        // get ulti input
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isUlti)
        {
            apiUlti();
        }

        // get jump input
        if (Input.GetKeyDown(KeyCode.Space) && countJump < 2)
        {
            apiJump();
        }

        // get attack input
        if (Input.GetKey(KeyCode.Mouse0) && !isAttack)
        {
            apiAttack();
        }

        // get move input
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x == 0)
        {
            apiIdle();
        }
        else
        {
            apiGoRight(movement.x > 0);
        }

    }

    void showAnimation()
    {
        // show animattion
        if (isDie)
        {
            animator.SetInteger("moveType", (int)MoveType.die);
        }
        else if (hurtCount > 0)
        // else if (isAnimationHurt)
        {
            animator.SetInteger("moveType", (int)MoveType.hurt);
        }
        else if (isUlti)
        {
            animator.SetInteger("moveType", (int)MoveType.ulti);

        }
        // else if (isAttack)
        else if (isAnimationAttack)
        {
            animator.SetInteger("moveType", (int)MoveType.attack);

            // can turn right/left while attacking
            if (movement.x > 0)
            {
                turnRight();
            }
            else if (movement.x < 0)
            {
                turnRight(false);
            }


        }

        else if (movement.x > 0)
        {
            turnRight();
            animator.SetInteger("moveType", (int)MoveType.walk);

            if (!audioSourceFoot.isPlaying)
            {
                audioSourceFoot.Play();
            }
        }
        else if (movement.x < 0)
        {
            turnRight(false);
            animator.SetInteger("moveType", (int)MoveType.walk);

            if (!audioSourceFoot.isPlaying)
            {
                audioSourceFoot.Play();
            }
        }
        else if (countJump > 0)
        {
            animator.SetInteger("moveType", (int)MoveType.jump);
        }
        else
        {
            animator.SetInteger("moveType", (int)MoveType.idle);

            audioSourceFoot.Stop();
        }


        if (countJump > 0)
        {
            audioSourceFoot.Stop();
        }

    }


    void FixedUpdate()
    {
        if (!canMove || isDie) return;

        // actually move the object
        // movement.Normalize();
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
        // SetMovement("y", jumpHeight);
        isFootCollide = false;
        yield return new WaitForSeconds(timeUp);


        // time in the air, when gravity pull object down
        movement.y = 0;
        // SetMovement("y", 0);
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

    IEnumerator attack(float delaySeconds)
    {
        // delay time
        yield return new WaitForSeconds(delaySeconds);


        isAnimationAttack = true;


    }

    IEnumerator ulti(float seconds)
    {
        canMove = false;

        // tell ulti controller to fire
        ultiController.Fire();

        // wait ulti done to release
        while (isUlti)
        {
            yield return new WaitForSeconds(0.1f);
        }

        // yield return new WaitForSeconds(seconds);
        // isUlti = false;
        canMove = true;
    }


    void turnRight(bool isRight = true)
    {

        if (isRight == isTurningRight)
        {
            return;
        }
        else if ((isRight && !isTurningRight) ||
            (!isRight && isTurningRight)
         )
        {
            // flip scale.x
            var scale = gameObject.transform.localScale;
            scale.x = -1 * scale.x;
            gameObject.transform.localScale = scale;

            // re-assign
            isTurningRight = !isTurningRight;
        }
    }

    IEnumerator waitThenDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Destroy(gameObject);
        gameObject.SetActive(false);
    }

    IEnumerator waitThenRecoverAttack(float seconds)
    {

        while (true)
        {
            yield return new WaitForSeconds(seconds);

            currentAvailableAttack = Mathf.Clamp(
                currentAvailableAttack + numAttackRecoverEachSec,
                0,
                numMaxContinueAttack
                );

        }
    }



    // api for AutoMoveAttack
    public void apiGoRight(bool isRight = true)
    {
        // print("MyCharacterController.cs: api go right");
        EventMove?.Invoke(this.Id, (int)MoveType.walk * (isRight ? 1 : -1));


        if (isRight)
        {
            movement.x = 1;
        }
        else
        {
            movement.x = -1;
        }

        // print("MyCharacterController.cs: " + gameObject.name + " , " + isRight);

    }

    public void apiIdle()
    {
        EventMove?.Invoke(this.Id, (int)MoveType.idle,
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));

        movement.x = 0;
        // updateVelocity();
        // SetMovement("x", 0);
    }

    public void apiJump()
    {
        EventMove?.Invoke(this.Id, (int)MoveType.jump);

        // show animation
        countJump++;
        countJumpTemp++;

        // logic
        StartCoroutine(jump(timeJump));

        audioSourceQuick.PlayOneShot(audioJump);
    }

    public void apiAttack()
    {
        EventMove?.Invoke(this.Id, (int)MoveType.attack);

        print("MyCharacterController.cs: herer attack: " + isAttack);
        if (isAttack || currentAvailableAttack <= 0) return;

        // wait delay then start attack
        isAttack = true;
        currentAvailableAttack--;
        StartCoroutine(attack(timeAttackDelay));

        // handle attack collide and attack done in handler

        audioSourceQuick.PlayOneShot(audioAttack);

    }

    public void apiUlti()
    {
        EventMove?.Invoke(this.Id, (int)MoveType.ulti);

        if (isUlti || !ultiController.CanUlti()) return;

        // if allow move, so no need to show animation and block movement
        if (ultiController.canMoveWhenUlti)
        {
            ultiController.Fire();
            return;
        }

        // flag to show animation
        isUlti = true;

        // logic
        StartCoroutine(ulti(ultiController.utliDurationTime));
        // ultiController.Fire();


    }

    public void apiDie()
    {

        var hp = gameObject.GetComponent<HP>();
        if (hp.currentHealth > 0)
        {
            hp.ChangeHealth(-100);
            return;
        }

        EventMove?.Invoke(this.Id, (int)MoveType.die);

        // show anim
        isDie = true;

        // wait some seconds then destroy
        StartCoroutine(waitThenDestroy(timeShowDealth));

    }

    public void apiHurt()
    {
        // if(isAnimationHurt) return;

        // show animation
        hurtCount++;
        // isAnimationHurt = true;

        // wait some seconds then recover
        // StartCoroutine(waitThenRecover());
        audioSourceQuick.PlayOneShot(audioHurt);
    }




    // handlers
    public void HandleAttackAnimationDone()
    {
        isAnimationAttack = false;
        isAttack = false;
    }

    public void HandleHurtAnimationDone()
    {
        // isHurt = false;
        hurtCount--;
        // isAnimationHurt = false;
        // print("MyCharacterController.cs: hurtcount: " + hurtCount);
    }

    public void handleWeaponCollided(GameObject other)
    {
        if (isDie || hurtCount > 0) return;

        print("" + gameObject.name + " attack: " + other.name);

        // check tags
        if (tagsIgnoredDamage.Contains(other.tag)) return;

        // then damage enemies if needed...
        var hp = gameObject.GetComponent<HP>();
        var otherHp = other.GetComponent<HP>();
        if (!hp || !otherHp) return;

        otherHp.ChangeHealth(-1 * hp.attack);

    }

    public void handleUltiDone()
    {
        print("ccontroller: ulti is done");
        isUlti = false;
    }

    public void handleHealthChanged(float newHealth)
    {

        print("" + gameObject.name + ": " + newHealth);

        if (newHealth <= 0)
        {
            apiDie();
            return;
        }

        float delta = newHealth - lastHealth;
        lastHealth = newHealth;
        print("" + gameObject.name + ", delta: " + delta);
        if (delta < 0)
        {
            apiHurt();
            return;
        }

    }

    public void handleFootCollided(GameObject obj)
    {
        isFootCollide = true;
    }

    public void handleObjectInUltiRange(GameObject other)
    {
        if (isDie || hurtCount > 0) return;

        // check tags
        if (tagsIgnoredDamage.Contains(other.tag)) return;

        var otherHp = other.GetComponent<HP>();
        if (!otherHp) return;

        print("Move.cs: damage: " + other.name);
        otherHp.ChangeHealth(-1 * ultiController.ultiDamage);
    }









    public bool IsTurnRight()
    {
        // var scale = transform.localScale;
        // return scale.x > 0;

        return isTurningRight;
    }

    public void ApplyMove(int moveType, Vector3 position)
    {

        if (moveType == (int)MoveType.idle)
        {
            apiIdle();
            gameObject.transform.position = position;
        }
        else if (moveType == (int)MoveType.walk)
        {
            apiGoRight();
        }
        else if (moveType == (int)MoveType.walk * -1)
        {
            apiGoRight(false);
        }
        else if (moveType == (int)MoveType.jump)
        {
            apiJump();
        }
        else if (moveType == (int)MoveType.attack)
        {
            apiAttack();
        }
        else if (moveType == (int)MoveType.ulti)
        {
            apiUlti();
        }
        else if (moveType == (int)MoveType.die)
        {
            apiDie();
        }

    }

}
