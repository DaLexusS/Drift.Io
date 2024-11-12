using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    private RectTransform joystickRectTransform;

    void Start()
    {
        joystickRectTransform = joystick.GetComponent<RectTransform>();
        joystick.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Input.mousePosition;

            joystickRectTransform.position = touchPosition;
            joystick.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystick.SetActive(false);
        }
    }
}
