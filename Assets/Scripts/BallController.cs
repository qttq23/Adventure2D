using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float damage = 3f;
    public int layerToDamage = 100;
    public Vector2 direction = new Vector2();
    public float speed = 0;


    Vector2 velocity;
    Rigidbody2D rigid;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }

    void Update()
    {
        rigid.velocity = direction * speed;
    }

    public void RealStart()
    {

        // set velocity, gravity, direction
        velocity = direction * speed;


    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        // handleObjInRange(collision);

        // damage
        if (collision.gameObject.layer == layerToDamage)
        {
            collision.gameObject.GetComponent<HP>().ChangeHealth(-1 * damage);

            // disappear
            gameObject.SetActive(false);
        }

    }

    // void OnTriggerStay2D(Collider2D collision)
    // {
    //     handleObjInRange(collision);
    // }

    // void handleObjInRange(Collider2D collision)
    // {
    //     if (!colliders.Contains(collision))
    //     {
    //         colliders.Add(collision);


    //     }
    // }



}
