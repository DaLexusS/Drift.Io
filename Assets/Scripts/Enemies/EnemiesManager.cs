using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] public GameObject Zombie;
    [SerializeField] private GameObject SpawnersFolder;

    public int MaxEnemyCount = 10;
    public float BaseSpawnRate = 2;
    public float spawnDelay = 2;
    public float MaxRoundTime = 180f;

    List<Transform> closestToPlayerSpawners = new List<Transform>();

    private float lastTimeSpawned;
    private GameObject player;

    public bool spawnOnPlayerDirection = false;
    private Camera mainCamera;

    private void Start()
    {
        lastTimeSpawned = Time.time;
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Time.time > lastTimeSpawned)
        {
            lastTimeSpawned = Time.time + spawnDelay;

            if (transform.childCount >= MaxEnemyCount) { return; }

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        closestToPlayerSpawners.Clear();
        Transform closestSpawner = GetClosestSpawnerOutOfView();

        if (closestSpawner != null)
        {
                
            foreach(Transform t in closestToPlayerSpawners)
            {
                Vector3 spawnPosition = t.position;
                spawnPosition.z = 0;
                Instantiate(Zombie, spawnPosition, Quaternion.identity, transform);
            }
        }

        spawnDelay = BaseSpawnRate / (1 + (Time.time % 60) / 60);
    }

    private Transform GetClosestSpawnerOutOfView()
    {
        List<Transform> closestSpawners = new List<Transform>();
        float closestDistance = float.MaxValue;

       
        foreach (Transform spawner in SpawnersFolder.transform)
        {
            if (IsObjectOutOfView(spawner))
            {
                float distance = Vector3.Distance(player.transform.position, spawner.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                }
            }
        }

        
        float threshold = 5f;
        foreach (Transform spawner in SpawnersFolder.transform)
        {
            if (IsObjectOutOfView(spawner))
            {
                float distance = Vector3.Distance(player.transform.position, spawner.position);
                if (Mathf.Abs(distance - closestDistance) <= threshold)
                {
                    closestSpawners.Add(spawner);
                    closestToPlayerSpawners.Add(spawner);
                }
            }
        }

        if (closestSpawners.Count > 0)
        {
            return closestSpawners[Random.Range(0, closestSpawners.Count)];
        }

        return null;
    }

    private bool IsObjectOutOfView(Transform obj)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(obj.position);

        return viewportPoint.z < 0 || viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;
    }
}
