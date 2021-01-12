using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianEffectController : MonoBehaviour
{
    public GameObject objectToHeal;
    public float hpHealEachTime;
    public float duration = 5f;

    float timer = 0;
    bool isStop = false;

    void Start()
    {
    }

    void Update(){

        timer += Time.deltaTime;
        if(timer >= duration){
            isStop = true;
            Destroy(gameObject);
            // gameObject.SetActive(false);
        }

        if(!isStop && objectToHeal){

            // go to object's position
            gameObject.transform.position = 
            new Vector3(
                objectToHeal.transform.position.x,
                objectToHeal.transform.position.y,
                objectToHeal.transform.position.z
                );

            // heal
            var hp = objectToHeal.GetComponent<HP>();
            hp.ChangeHealth(hpHealEachTime);

        }

    }




}
