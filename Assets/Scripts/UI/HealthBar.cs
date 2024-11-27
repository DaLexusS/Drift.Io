using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private Player playerData;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
            return;
        }

        playerData = player.GetComponent<Player>();

        if (playerData == null)
        {
            Debug.LogError("Player component not found!");
            return;
        }

        slider.maxValue = playerData.maxHealth;
        slider.minValue = 0;
    }

    void Update()
    {
        slider.maxValue = playerData.maxHealth;
        slider.minValue = 0;

        if (playerData != null)
        {
            slider.value = playerData.health;
        }
    }
}
