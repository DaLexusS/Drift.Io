using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldCameraToCar : MonoBehaviour
{
    [SerializeField] Transform car;
    private float yOffset = 0.55f;

    private void Update()
    { 
        Vector2 targetPosition = car.transform.position;

        targetPosition.y += yOffset;

        transform.position = targetPosition;
    }
}
