using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{
    public bool IsPaused = false;
    public void PausePressed()
    {
        if (IsPaused)
        {
            IsPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            IsPaused = true;
            Time.timeScale = 0f;
        }
    }
}
