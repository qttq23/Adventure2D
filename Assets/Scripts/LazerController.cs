using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public float damage = 3f;
    public int layerToDamage = 100;
    public Vector2 direction = new Vector2();
    public float duration = 2f;


    Vector2 velocity;
    Rigidbody2D rigid;
    SpriteRenderer renderer;
    Color originColor;
    float currentA = 0;
    List<Collider2D> colliders = new List<Collider2D>();

    void Start()
    {
        originColor = gameObject.GetComponent<SpriteRenderer>().color;

        gameObject.GetComponent<SpriteRenderer>().color = 
        new Color(originColor.r, originColor.g, originColor.b, 0); // transparent
    }


    public void RealStart()
    {

        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        rigid.gravityScale = 0;
        
        currentA = 0;

        StartCoroutine(showFullIn(duration));
    }

    IEnumerator showFullIn(float seconds)
    {

        float tempSec = 0;
        while (tempSec < seconds)
        {
            print("LazerController.cs: currentA: " + currentA);

            // increase color
            currentA += 51;
            renderer.color = new Color(originColor.r, originColor.g, originColor.b, currentA);


            // wait
            yield return new WaitForSeconds(seconds / 5);

            // increase tempSec
            tempSec += seconds / 5;
        }

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        handleObjInRange(collision);



    }

    void OnTriggerStay2D(Collider2D collision)
    {
        handleObjInRange(collision);
    }

    void handleObjInRange(Collider2D collision)
    {
        if (!colliders.Contains(collision))
        {
            colliders.Add(collision);

            // // damage
            // if (collision.gameObject.layer == layerToDamage)
            // {
            //     collision.gameObject.GetComponent<HP>().ChangeHealth(-1 * damage);

            //     // disappear
            //     gameObject.SetActive(false);
            // }
        }
    }



}
