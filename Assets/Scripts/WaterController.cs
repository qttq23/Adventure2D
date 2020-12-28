using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D collision)
    {
        var hp = collision.gameObject.GetComponent<HP>();
        if(hp){
            hp.ChangeHealth(-1000);
        }
    }





}
