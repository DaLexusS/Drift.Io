
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] public GameObject Zombie;
    [SerializeField] private GameObject SpawnersFolder;

    public int MaxEnemyCount = 10;
    public int spawnDelay = 2;

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
        int randomIndex = Random.Range(0, SpawnersFolder.transform.childCount - 1);

        Transform randomSpawner = SpawnersFolder.transform.GetChild(randomIndex);

        if (IsObjectOutOfView(randomSpawner))
        {
            Vector3 spawnPosition = randomSpawner.position;
            spawnPosition.z = 0;
            Instantiate(Zombie, spawnPosition, Quaternion.identity, transform);
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
