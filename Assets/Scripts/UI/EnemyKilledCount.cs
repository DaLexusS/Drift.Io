using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyKilledCount : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    private Player player;
    private int currentCount = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        countText.text = currentCount.ToString();
    }

    public void UpdateText(int amount)
    {
        countText.text = amount.ToString();
    }
}
