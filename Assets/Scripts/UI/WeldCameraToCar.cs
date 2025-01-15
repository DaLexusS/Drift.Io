using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldCameraToCar : MonoBehaviour
{
    [SerializeField] GameObject car;
    private float yOffset = 0.55f;
    private Rigidbody2D canvasRigidBoy;

    private void Awake()
    {
        canvasRigidBoy = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (car != null)
        {
            Vector2 targetPosition = car.transform.position;

            targetPosition.y += yOffset;

            canvasRigidBoy.MovePosition(targetPosition);
        }
    }
}
