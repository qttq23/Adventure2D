using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireController : MonoBehaviour
{
    public float duration = 1.5f;
    public int layerToDamage = 20;
    public float damage = 5f;
    public GameObject burnPrefab;

    List<Collider2D> colliders = new List<Collider2D>();

    void Start()
    {
        Physics2D.IgnoreLayerCollision(12, 11, true);

        StartCoroutine(destroyAfter(duration));
    }

    IEnumerator destroyAfter(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
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

            // damage
            if (collision.gameObject.layer == layerToDamage)
            {
                collision.gameObject.GetComponent<HP>().ChangeHealth(-1 * damage);

                var burnEffect = Instantiate(
                    burnPrefab,
                    collision.gameObject.transform.position,
                    collision.gameObject.transform.rotation
                    );
                var controller = burnEffect.GetComponent<BurnEffectController>();
                controller.objectToBurn = collision.gameObject;
                controller.hpBurnEachTime = 0.05f;
                controller.duration = 2f;


            }

        }
    }


}
