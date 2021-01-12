using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class MyGameManager : MonoBehaviour
{

    public GameObject canvasPopup;
    public GameObject canvasGameEnd;
    public GameObject canvasGameWin;
    public GameObject canvasStoryPopup;

    public GameObject player;
    public GameObject dumpPlayer;
    public GameObject boss;
    public MyConfig config;

    public AudioSource audioSource;
    public AudioClip audioBackground;
    public AudioClip audioLoose;
    public AudioClip audioWin;

    float dumpPlayerHp;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8, true);
        Physics2D.IgnoreLayerCollision(9, 11, true);
        Physics2D.IgnoreLayerCollision(12, 11, true);


        // show story panel
        canvasStoryPopup.SetActive(true);

        // track player,boss health
        player.GetComponent<HP>().EventHealthChanged += handlePlayerHealthChanged;
        boss.GetComponent<HP>().EventHealthChanged += handleBossHealthChanged;
        if (dumpPlayer)
        {

            dumpPlayer.GetComponent<HP>().EventHealthChanged += handleDumpPlayerHealthChanged;
        }

        // audio
        audioSource.clip = audioBackground;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasPopup.SetActive(!canvasPopup.activeSelf);
        }


    }

    public void HandlePopupRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }


    public void HandlePopupSave()
    {

    }


    public void HandlePopupSettings()
    {

    }

    public void HandlePopupQuit()
    {
        print("MenuManager.cs: handle quit");
        SceneManager.LoadScene("menuScene", LoadSceneMode.Single);
    }

    public void HandlePopupClose()
    {
        print("MyGameManager.cs: close popup");
        canvasPopup.SetActive(false);
    }



    void handlePlayerHealthChanged(float value)
    {
        if (!config.isPlayOnline)
        {
            if (value == 0)
            {
                // game over
                StartCoroutine(waitThenShowLoosePopup());
            }
        }
        else
        {
            if (value == 0 && dumpPlayerHp == 0)
            {
                StartCoroutine(waitThenShowLoosePopup());

            }
        }

    }


    void handleBossHealthChanged(float value)
    {
        // boss die
        if (value == 0)
        {
            StartCoroutine(waitThenShowWinPopup());
        }

    }

    void handleDumpPlayerHealthChanged(float value)
    {
        dumpPlayerHp = value;

    }

    public void handleStoryOk()
    {

        canvasStoryPopup.SetActive(false);
    }



    IEnumerator waitThenShowLoosePopup()
    {

        yield return new WaitForSeconds(2f);
        canvasGameEnd.SetActive(true);

        audioSource.Stop();
        audioSource.PlayOneShot(audioLoose);
    }

    IEnumerator waitThenShowWinPopup()
    {

        yield return new WaitForSeconds(2f);
        canvasGameWin.SetActive(true);
        player.SetActive(false);


        audioSource.Stop();
        audioSource.PlayOneShot(audioWin);
    }

}
