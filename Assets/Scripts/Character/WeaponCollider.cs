using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    // public List<string> tagsAttacked = new List<string>();

    // [HideInInspector]
    // public Move parent;

    public delegate void OnObjectIn(GameObject obj);
    public event OnObjectIn EventObjectIn;


    // bool isFire = false;
    List<Collider2D> colliders = new List<Collider2D>();


    // public void Fire(bool isFire)
    // {
    //     this.isFire = isFire;
    //     colliders.Clear();
    // }

    void OnTriggerEnter2D(Collider2D collision)
    {
        handleWeaponCollided(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleWeaponCollided(collision);
    }

    void handleWeaponCollided(Collider2D collision)
    {
        // if (isFire && collision.gameObject.CompareTag("enemy"))
        // if (isFire && tagsAttacked.Contains(collision.gameObject.tag))
        // if (isFire)
        // {


        // }

        if (enabled && !colliders.Contains(collision))
        {
            colliders.Add(collision);
            // parent.handleWeaponCollided(collision.gameObject);
            EventObjectIn?.Invoke(collision.gameObject);
        }
    }

    void OnEnable()
    {
        colliders.Clear();
        Debug.Log("tweapon.cs:PrintOnEnable: script was enabled");
    }

    void OnDisable()
    {
        colliders.Clear();
        Debug.Log("tweapon.cs:PrintOnDisable: script was disabled");
    }

}
