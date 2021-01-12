using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnEffectController : MonoBehaviour
{
    public GameObject objectToBurn;
    public float hpBurnEachTime = 0.5f;
    public float duration = 3f;

    float timer = 0;
    bool isStop = false;

    void Start()
    {
        StartCoroutine(burn());
    }

    IEnumerator burn()
    {

        while (timer < duration)
        {

            if (!isStop && objectToBurn)
            {

                // go to object's position
                gameObject.transform.position =
                new Vector3(
                    objectToBurn.transform.position.x,
                    objectToBurn.transform.position.y,
                    objectToBurn.transform.position.z
                    );

                // heal
                var hp = objectToBurn.GetComponent<HP>();
                hp.ChangeHealth(-1 * hpBurnEachTime);

            }

            timer += 1;
            yield return new WaitForSeconds(1f);

        }

        Destroy(gameObject);
    }




}
