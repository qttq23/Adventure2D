using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour
{

    public MyConfig config;

    void Start()
    {

    }

    public void handlePlay()
    {
        SceneManager.LoadScene("Map1version2", LoadSceneMode.Single);
    }

    public void handleCreateRoom()
    {
        config.isPlayOnline = true;
        config.isHost = true;
        SceneManager.LoadScene("Map1version2", LoadSceneMode.Single);
    }

    public void handleJoinRoom()
    {
        config.isPlayOnline = true;
        config.isHost = false;
        SceneManager.LoadScene("Map1version2", LoadSceneMode.Single);
    }


    public void handleQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}