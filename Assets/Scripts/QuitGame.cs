using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public Canvas canvas;
    public bool a = false;
    public void popup()
    {
        if(a == false)
        {
            a = true;
            canvas.enabled = true;
        }else if (a)
        {
            a = false;
            canvas.enabled = false;
        }
    }
}
