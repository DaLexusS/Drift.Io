using UnityEngine;
using UnityEngine.UI;

public class XpSlider : MonoBehaviour
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

        slider.maxValue = playerData.xpNeededToLevel;
        slider.minValue = 0;
    }

    void Update()
    {
        slider.maxValue = playerData.xpNeededToLevel;
        slider.minValue = 0;

        if (playerData != null)
        {
            slider.value = playerData.roundXp;
        }
    }
}
