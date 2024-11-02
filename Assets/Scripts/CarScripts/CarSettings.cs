using UnityEngine;

[CreateAssetMenu(fileName = "NewCarSettings", menuName = "Car/Settings")]
public class CarSettings : ScriptableObject
{
    public float acceleration = 10f;
    public float turnSpeed = 5f;
    public float rotationDelay = 0.1f;
    public float dragAmount = 2f;
}

