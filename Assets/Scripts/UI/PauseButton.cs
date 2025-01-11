using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public bool IsPaused = false;

    public void PauseGame()
    {
        IsPaused = true;
    }

    public void UnPauseGame()
    {
        IsPaused = false;
    }
}
