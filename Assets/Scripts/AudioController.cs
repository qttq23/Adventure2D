using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public MyConfig config;

    void Start()
    {

        applyAudioConfig();
        StartCoroutine(waitThenApplyaAudioConfig(2f));

    }

    IEnumerator waitThenApplyaAudioConfig(float seconds)
    {

        while (true)
        {
            yield return new WaitForSeconds(seconds);

            applyAudioConfig();
        }

    }



    void applyAudioConfig()
    {

        // get all child audio sources
        // mute/unmute

		foreach(Transform child in gameObject.transform)
        {
            var audioSource = child.gameObject.GetComponent<AudioSource>();
            audioSource.mute = !config.isAudioActionEnabled;
        }
    }



}
