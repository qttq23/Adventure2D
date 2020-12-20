using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveAttack : MonoBehaviour
{
    // public Move move;
    public MyCharacterController characterController;
    public GameObject objectToChase;
    public WeaponRange weaponRange;
    public UltiRange ultiRange;
    public TriggerColliderController triggerCollider;
    public float timeToMakeDecision = 2f;

    [HideInInspector]
    public bool isDoingUlti = false;
    bool isTargetInRange = false;
    bool isMovingToRight = false;

    // Start is called before the first frame update
    void Start()
    {
        // weaponRange.parent = this;
        weaponRange.EventObjectIn += handleObjectInWeaponRange;

        if (ultiRange)
        {
            // ultiRange.parent = this;
            ultiRange.EventObjectIn += handleObjectInUltiRange;
            ultiRange.SetCheck(true);
        }


        if (triggerCollider)
        {
            triggerCollider.EventObjectInOut += handleObjectInTriggerColliderRange;
        }

        StartCoroutine(makeDecision(timeToMakeDecision));
    }

    // void FixedUpdate()
    IEnumerator makeDecision(float secondsForNextDetect)
    {
        while (true)
        {
            print("AutoMoveAttack.cs: making decision");
            yield return new WaitForSeconds(secondsForNextDetect);

            if (isDoingUlti) continue;

            if (!isTargetInRange)
            {
                print("AutoMoveAttack.cs: moveRandom");
                moveRandom();
            }
            else
            {
                print("AutoMoveAttack.cs: moveTowardObject");
                // object already in rage
                moveTowardObject();
            }


        }


    }

    public void handleObjectInWeaponRange(GameObject obj)
    {
        if (!isTargetInRange) return;
        if (isDoingUlti) return;
        if (!GameObject.ReferenceEquals(obj, objectToChase)) return;

        characterController.apiAttack();
    }


    public void handleObjectInUltiRange(GameObject obj)
    {
        if (!GameObject.ReferenceEquals(obj, objectToChase)) return;
        if (!isTargetInRange) return;
        if (!characterController.ultiController.CanUlti()) return;

        // print("fire ulti");
        isDoingUlti = true;
        ultiRange.SetCheck(false);

        characterController.apiUlti();
        StartCoroutine(waitUltiDone(characterController.ultiController.utliDurationTime));

    }

    IEnumerator waitUltiDone(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        // print("ulti done");
        isDoingUlti = false;
        ultiRange.SetCheck(true);
    }

    void handleObjectInTriggerColliderRange(GameObject obj, bool isIn)
    {

        if (!GameObject.ReferenceEquals(obj, objectToChase)) return;

        isTargetInRange = isIn;
        // print("obj in range: " + obj.name);

    }

    void moveRandom()
    {
        // if character is moving to the right side
        // we want it to reverse direction -> go to the left
        print("AutoMoveAttack.cs: moveRandom here");
        characterController.apiGoRight(!isMovingToRight);
        isMovingToRight = !isMovingToRight;
        print("AutoMoveAttack.cs: moveRandom done");
    }

    void moveTowardObject()
    {
        if (transform.position.x < objectToChase.transform.position.x)
        {
            characterController.apiGoRight(true);
            isMovingToRight = true;
        }
        else
        {
            characterController.apiGoRight(false);
            isMovingToRight = false;

        }
    }

}
