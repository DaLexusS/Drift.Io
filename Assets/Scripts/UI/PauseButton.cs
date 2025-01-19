using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{
    public bool IsPaused = false;
    [SerializeField] public GameObject panel;
    public void PausePressed()
    {
        if (IsPaused)
        {
            IsPaused = false;
            panel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            IsPaused = true;
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
