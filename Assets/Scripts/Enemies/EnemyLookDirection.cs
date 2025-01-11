using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookDirection : MonoBehaviour
{
    [SerializeField] public GameObject visual;
    private float centerThreshold = 0.05f;
    private float checkCooldown = 3f;
    private float lastCheckTick;

    private void Awake()
    {
        lastCheckTick = Time.time;
    }

    private void Update()
    {
        if(lastCheckTick < Time.time)
        { 
            CheckPositionRelativeToCamera();
            lastCheckTick = Time.time + checkCooldown;
        }
    }

    private void CheckPositionRelativeToCamera()
    {
        Camera mainCamera = Camera.main;

        if (!mainCamera || visual == null) return;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (Mathf.Abs(viewportPosition.x - 0.5f) < centerThreshold) return;

        if (viewportPosition.x < 0.5f && visual.transform.localScale.x < 0)
        {
            MirrorVisual(true);
        }
        else if (viewportPosition.x > 0.5f && visual.transform.localScale.x > 0)
        {
            MirrorVisual(false);
        }
    }

    private void MirrorVisual(bool isLeftSide)
    {
        Vector3 scale = visual.transform.localScale;
        scale.x = isLeftSide ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        visual.transform.localScale = scale;
    }
}
