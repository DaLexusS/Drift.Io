using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] public GameObject Zombie;
    [SerializeField] private GameObject SpawnersFolder;
    [SerializeField] public GameObject Timer;

    public int MaxEnemyCount = 10;
    public float BaseSpawnRate = 3f;
    public float spawnDelay = 3f;
    public float MaxRoundTime = 180f;
    private int currentTime;
    private bool gamePaused = false;

    List<Transform> closestToPlayerSpawners = new List<Transform>();

    private float lastTimeSpawned;
    private GameObject player;

    public bool spawnOnPlayerDirection = false;
    private Camera mainCamera;

    private void Start()
    {
        currentTime = Timer.GetComponent<Timer>().time;
        lastTimeSpawned = Time.time;
        mainCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        StartCoroutine(AdjustSpawnDelayOverTime());
    }

    private void Update()
    {
        if (Time.time > lastTimeSpawned)
        {
            lastTimeSpawned = Time.time + spawnDelay;

            if (transform.childCount >= MaxEnemyCount || gamePaused) { return; }

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        closestToPlayerSpawners.Clear();
        Transform closestSpawner = GetClosestSpawnerOutOfView();

        if (closestSpawner != null)
        {
            foreach (Transform t in closestToPlayerSpawners)
            {
                Vector3 spawnPosition = t.position;
                spawnPosition.z = 0;
                Instantiate(Zombie, spawnPosition, Quaternion.identity, transform);
            }
        }
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

    private IEnumerator AdjustSpawnDelayOverTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < MaxRoundTime)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < 60f)
            {
                MaxEnemyCount = 50;
                spawnDelay = Mathf.Lerp(3f, 1.5f, elapsedTime / 60f);
            }
            else if (elapsedTime < 65f)
            {
                spawnDelay = 1.5f;
                yield return new WaitForSeconds(5f);
                elapsedTime += 5f;
            }
            else if (elapsedTime < 90f)
            {
                spawnDelay = Mathf.Lerp(1.5f, 3f, (elapsedTime - 65f) / 25f);
            }
            else if (elapsedTime < 120f)
            {
                MaxEnemyCount = 100;
                spawnDelay = Mathf.Lerp(3f, 1.5f, (elapsedTime - 90f) / 30f);
            }
            else if (elapsedTime < 125f)
            {
                spawnDelay = 1.5f;
                yield return new WaitForSeconds(5f);
                elapsedTime += 5f;
            }
            else if (elapsedTime < 150f)
            {
                MaxEnemyCount = 150;
                spawnDelay = Mathf.Lerp(1.5f, 3f, (elapsedTime - 125f) / 25f);
            }
            else if (elapsedTime < 180f)
            {
                spawnDelay = 0.5f;
                yield return new WaitForSeconds(10f);
                elapsedTime += 10f;
            }
            else
            {
                spawnDelay = 3f;
            }

            yield return null;
        }
    }

    private bool IsObjectOutOfView(Transform obj)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(obj.position);

        return viewportPoint.z < 0 || viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;
    }
}
