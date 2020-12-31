using UnityEngine;

[CreateAssetMenu(menuName = "MyConfig")]

public class MyConfig : ScriptableObject
{



    public bool isPlayOnline = false;
    public string roomName = "";
    public bool isHost = false;
    public bool isAudioActionEnabled = false;
}