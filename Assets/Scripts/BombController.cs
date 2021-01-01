using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float timeExplode = 2f;
    public float damage = 5f;
    public int layerToDamage = 8;
    public ParticleSystem explosionEffect; 
    public ParticleSystem fireEffect; 
    public AudioSource audioSource;
    public AudioClip audioClip;

    List<Collider2D> colliders = new List<Collider2D>();
    bool isDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(explodeAfter(timeExplode));
    }

    IEnumerator explodeAfter(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        explosionEffect.Play();
        // audio
        audioSource.PlayOneShot(audioClip);
        
        // hide
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        fireEffect.gameObject.SetActive(false);
        fireEffect.Stop();

        // start damage obj in range
        isDamage = true;

        yield return new WaitForSeconds(seconds/2);
        explosionEffect.Stop();
        isDamage = false;
        gameObject.SetActive(false);
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
        if (isDamage && !colliders.Contains(collision))
        {
            colliders.Add(collision);

            // damage
            if (collision.gameObject.layer == layerToDamage)
            {
                collision.gameObject.GetComponent<HP>().ChangeHealth(-1 * damage);
            }
        }
    }

    public void SetScale(float nTimes){
        var scale = gameObject.transform.localScale;
        var fireScale = fireEffect.gameObject.transform.localScale;
        var smokeScale = explosionEffect.gameObject.transform.localScale;

        scale *= nTimes;
        fireScale *= nTimes;
        smokeScale *= nTimes;

        gameObject.transform.localScale = scale;
        fireEffect.gameObject.transform.localScale = fireScale;
        explosionEffect.gameObject.transform.localScale = smokeScale;
    }

}
