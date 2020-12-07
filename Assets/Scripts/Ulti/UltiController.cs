using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiController : MonoBehaviour
{

	[HideInInspector]
	public Move parent;
	public float timeToAvailable = 1f;
	public float utliDurationTime = 2f;
	
	protected bool canUlti = false;

	// Start is called before the first frame update
    void Start()
    {
    	StartCoroutine(chargeUlti());
    }

    IEnumerator chargeUlti(){

    	yield return new WaitForSeconds(this.timeToAvailable);
    	this.canUlti = true;	
    }

    public bool CanUlti(){
    	return canUlti;
    }

    virtual public void Fire(){

    	// TODO: custom ulti for each characters

    }

    protected void handleUltiDone(){
    	this.canUlti = false;
    	StartCoroutine(chargeUlti());

    	// signal parent
    	parent.handleUltiDone();
    }

}
