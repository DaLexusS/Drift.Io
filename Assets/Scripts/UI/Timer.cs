using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int time = 0;
    private float elapsedTime = 0f;
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            time++;
            elapsedTime -= 1f;
        }
    }
}
