using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveAttack : MonoBehaviour
{
    public Move move;
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
        weaponRange.parent = this;

        if (ultiRange)
        {
            ultiRange.parent = this;
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
            yield return new WaitForSeconds(secondsForNextDetect);

            if (isDoingUlti) continue;

            if (!isTargetInRange)
            {
                moveRandom();
            }
            else
            {
                // object already in rage
                moveTowardObject();
            }


        }


    }

    public void handleObjectInWeaponRange(GameObject obj)
    {
        if (!isTargetInRange) return;
        if (isDoingUlti) return;

        move.apiAttack();
    }


    public void handleObjectInUltiRange(GameObject obj)
    {

        if (!isTargetInRange) return;
        if (!move.ultiController.CanUlti()) return;

        // print("fire ulti");
        isDoingUlti = true;
        ultiRange.SetCheck(false);

        move.apiUlti();
        StartCoroutine(waitUltiDone(move.ultiController.utliDurationTime));

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
        move.apiGoRight(!isMovingToRight);
        isMovingToRight = !isMovingToRight;
    }

    void moveTowardObject()
    {
        if (transform.position.x < objectToChase.transform.position.x)
        {
            move.apiGoRight(true);
            isMovingToRight = true;
        }
        else
        {
            move.apiGoRight(false);
            isMovingToRight = false;

        }
    }

}
