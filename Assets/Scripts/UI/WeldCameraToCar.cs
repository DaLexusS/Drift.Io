using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldCameraToCar : MonoBehaviour
{
    [SerializeField] GameObject car;
    
    private void FixedUpdate()
    {
        gameObject.transform.position = car.transform.position;
    }
}
