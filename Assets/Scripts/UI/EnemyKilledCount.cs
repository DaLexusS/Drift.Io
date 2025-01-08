using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyKilledCount : MonoBehaviour
{
    private Player player;
    private int currentCount = 0;
    [SerializeField] TMP_Text countText;
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
