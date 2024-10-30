using UnityEngine;

public class CarController2D : MonoBehaviour
{
    private Rigidbody2D carRigidbody;
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float turnSpeed = 5f;
    public float rotationDelay = 0.1f;
    public float dragAmount = 2f;
    public float minPerfectDrag = 5f;
    public float maxPerfectDrag = 10f;

    private float moveInput;
    private float turnInput;
    private float currentRotationVelocity;

    public Joystick joystick;
    public bool isDrifting;
    public float driftThreshold = 5f;

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = joystick.Vertical;
        turnInput = joystick.Horizontal;

        if (carRigidbody.velocity.magnitude > maxSpeed)
        {
            carRigidbody.velocity = carRigidbody.velocity.normalized * maxSpeed;
        }

        if (moveInput == 0 && turnInput == 0)
        {
            carRigidbody.drag = dragAmount;
        }
        else
        {
            carRigidbody.drag = 0;
        }

        CheckForDrift();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = new Vector2(turnInput, moveInput).normalized;

        if (moveDirection.sqrMagnitude > 0)
        {
            carRigidbody.AddForce(moveDirection * acceleration);

            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;

            float smoothedAngle = Mathf.SmoothDampAngle(carRigidbody.rotation, targetAngle, ref currentRotationVelocity, rotationDelay);
            carRigidbody.MoveRotation(smoothedAngle);
        }
    }

    void CheckForDrift()
    {
        Vector2 localVelocity = transform.InverseTransformDirection(carRigidbody.velocity);

        isDrifting = Mathf.Abs(localVelocity.x) > driftThreshold && localVelocity.y > 1f;

        driftThreshold = Mathf.Lerp(minPerfectDrag, maxPerfectDrag, carRigidbody.velocity.magnitude / maxSpeed);
    }
}
