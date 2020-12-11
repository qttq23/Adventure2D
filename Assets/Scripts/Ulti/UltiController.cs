using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiController : MonoBehaviour
{

	[HideInInspector]
	public Move parent;
	public float timeToAvailable = 1f;
	public float utliDurationTime = 2f;
    public float ultiDamage = 10f;


    public delegate void OnPercentUltiChanged(float percent);
    public event OnPercentUltiChanged EventPercentUltiChanged;
    [HideInInspector]
    public float PercentUlti {  get { return timePass/timeToAvailable;}}

    protected bool canUlti = false;
    protected float timePass = 0;

	// Start is called before the first frame update
    void Start()
    {
    	// StartCoroutine(chargeUlti());
    }

    // IEnumerator chargeUlti(){

    // 	yield return new WaitForSeconds(this.timeToAvailable);
    // 	this.canUlti = true;

    //     while(true){


    //     }
    // }

    void FixedUpdate(){
        if(!canUlti){

            setTimePass(timePass + Time.fixedDeltaTime);

            if(timePass >= timeToAvailable){
                canUlti = true;
            }
        }
    }

    public void setTimePass(float seconds){
        timePass = Mathf.Clamp(seconds, 0, timeToAvailable);
        EventPercentUltiChanged?.Invoke(PercentUlti);

    }

    public bool CanUlti(){
    	return canUlti;
    }

    virtual public void Fire(){

    	// TODO: custom ulti for each characters

    }

    protected void handleUltiDone(){
    	this.canUlti = false;
        setTimePass(0);
    	// StartCoroutine(chargeUlti());

    	// signal parent
        parent.handleUltiDone();
    }

}
