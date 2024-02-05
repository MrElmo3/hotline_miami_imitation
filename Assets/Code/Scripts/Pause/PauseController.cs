using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool isPaused;

    public static void TogglePause(){
        if(Time.timeScale > 0){
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
        }
        else if (Time.timeScale == 0){
            Time.timeScale = 1;
            AudioListener.pause = false;
            isPaused = false;
        }
    } 

}
