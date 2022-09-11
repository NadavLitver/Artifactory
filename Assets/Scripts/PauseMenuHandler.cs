using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public bool isPaused;
    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        AudioListener.pause = true;
    }
    public void UnPause()
    {
        Time.timeScale = 1;
        isPaused = false;
        AudioListener.pause = false;
 

    }
    private void OnDisable()
    {
        UnPause();
    }
}
