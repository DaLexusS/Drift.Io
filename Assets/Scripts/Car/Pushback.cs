using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pushback : MonoBehaviour
{
    [SerializeField] public GameObject car;
    [SerializeField] public float pushBackPower = 0.005f;
    [SerializeField] public float angleOffset = 15f;
    private CarController2D carController;

    private void Awake()
    {
        carController = car.GetComponent<CarController2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (carController.canDriveOver) { return; }
        if (!collision.transform.CompareTag("Enemy")) { return; }

        Rigidbody2D enemyRigidbody = collision.transform.GetComponent<Rigidbody2D>();
        if (enemyRigidbody == null) { return; }

        Vector2 direction = collision.transform.position - car.transform.position;
        Vector2 pushDirection = -direction.normalized;

        bool adjustRight = Random.value > 0.5f;
        float adjustedAngle = adjustRight ? angleOffset : -angleOffset;

        pushDirection = RotateVector(pushDirection, adjustedAngle);
        enemyRigidbody.AddForce(pushDirection * pushBackPower, ForceMode2D.Impulse);
    }

    private Vector2 RotateVector(Vector2 vector, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRadians);
        float sin = Mathf.Sin(angleRadians);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}
