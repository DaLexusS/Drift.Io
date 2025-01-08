using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int time = 0;
    private float elapsedTime = 0f;
    public string formattedTime = "00:00";
    [SerializeField] TMP_Text TimerText;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            time++;
            elapsedTime -= 1f;
            UpdateFormattedTime();
        }
    }
    private void UpdateFormattedTime()
    {
        int minutes = time / 60;
        int seconds = time % 60;
        formattedTime = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        TimerText.text = formattedTime;
    }
}
