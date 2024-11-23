using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CarController2D : MonoBehaviour
{
    private Player player;
    private Rigidbody2D carRigidbody;

    public CarSettings carSettings;

    public float maxSpeed;

    public float acceleration;
    public float turnSpeed;
    public float rotationDelay;
    public float dragAmount;
    public bool canDriveOver = false;

    private float moveInput;
    private float turnInput;
    private float currentRotationVelocity;

    public Joystick joystick;

    private void Awake()
    {
        player = GetComponent<Player>();
        carRigidbody = GetComponent<Rigidbody2D>();

        maxSpeed = player.maxSpeed;

        if (carSettings != null)
        {
            acceleration = carSettings.acceleration;
            turnSpeed = carSettings.turnSpeed;
            rotationDelay = carSettings.rotationDelay;
            dragAmount = carSettings.dragAmount;
        }
    }
    void FixedUpdate()
    {
        DriveOnInput();
        AdjustCarRotation();
        CanDriveOverEnemies();
    }
    private void DriveOnInput()
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
    }

    private void AdjustCarRotation()
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

    private void CanDriveOverEnemies()
    {
        float speedPercentage = maxSpeed - (maxSpeed * 0.25f);
        if (carRigidbody.velocity.magnitude >= speedPercentage) 
        {
            canDriveOver = true;
        }
        else
        {
            canDriveOver = false;
        }
    }
}
