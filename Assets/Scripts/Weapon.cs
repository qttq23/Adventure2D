using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public Move parent;
    bool isFire = false;
    List<Collider2D> colliders = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire(bool isFire)
    {
        this.isFire = isFire;
        colliders.Clear();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        handleWeaponCollided(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleWeaponCollided(collision);
    }

    void handleWeaponCollided(Collider2D collision){
        if (isFire && collision.gameObject.CompareTag("enemy"))
        {
            if (!colliders.Contains(collision))
            {
                colliders.Add(collision);
                parent.handleWeaponCollided(collision.gameObject);
            }

        }
    }

}
