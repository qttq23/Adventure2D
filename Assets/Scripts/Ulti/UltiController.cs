using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiController : MonoBehaviour
{

    [HideInInspector]
    // public Move parent;
    public MyCharacterController parent;
    public float timeToAvailable = 1f;
    public float utliDurationTime = 2f;
    public float ultiDamage = 10f;
    public bool canMoveWhenUlti = false;

    // used by parent such CharcterController to handle events
    public delegate void OnUltiDone();
    public delegate void OnObjectInUltiRange(GameObject obj);
    public event OnUltiDone EventUltiDone;
    public event OnObjectInUltiRange EventObjectInUltiRange;

    // used by UI such as Ulti Bar
    public delegate void OnPercentUltiChanged(float percent);
    public event OnPercentUltiChanged EventPercentUltiChanged;
    [HideInInspector]
    public float PercentUlti { get { return timePass / timeToAvailable; } }

    // internal data 
    protected bool canUlti;
    protected float timePass;

    protected void Start()
    {
        canUlti = false;
        timePass = 0;
        StartCoroutine(chargeUlti());
    }

    IEnumerator chargeUlti()
    {

        // re-set time pass
        canUlti = false;
        timePass = 0;

        float timeStep = 0.02f;
        while (timePass < timeToAvailable)
        {
            yield return new WaitForSeconds(timeStep);

            timePass = Mathf.Clamp(timePass + timeStep, 0, timeToAvailable);
            EventPercentUltiChanged?.Invoke(PercentUlti);
        }

        canUlti = true;

    }



    public bool CanUlti()
    {
        return canUlti;
    }

    virtual public void Fire()
    {

        // TODO: custom ulti for each character

    }

    protected void handleUltiDone()
    {
        // this.canUlti = false;
        // setTimePass(0);
        StartCoroutine(chargeUlti());

        // signal parent
        // parent.handleUltiDone();
        print("UltiController.cs: handleUltiDone");

        EventUltiDone?.Invoke();
    }

    protected void handleObjectInRange(GameObject obj)
    {
        EventObjectInUltiRange?.Invoke(obj);
    }

}
