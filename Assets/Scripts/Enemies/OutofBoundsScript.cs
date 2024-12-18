using UnityEngine;
using UnityEngine.Events;

public class OutofBoundsScript : MonoBehaviour
{
    private Camera mainCamera;
    public UnityEvent enemyOutOfBounds;
    [SerializeField] public float outOfBoundsLifeTime = 5f;
    private float nextTickForEvent = 0;

    void Start()
    {
        mainCamera = Camera.main;
        nextTickForEvent = Time.time + outOfBoundsLifeTime;
    }

    
    void Update()
    {
        if (IsObjectOutOfView(transform))
        {
            if(Time.time >= nextTickForEvent)
            {
                enemyOutOfBounds.Invoke();
            }
        }
        else
        {
            nextTickForEvent = Time.time + outOfBoundsLifeTime;
        }
    }

    bool IsObjectOutOfView(Transform obj)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(obj.position);

        return viewportPoint.z < 0 || viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;
    }
}
