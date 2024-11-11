using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] public GameObject Zombie;
    [SerializeField] private Transform[] spawners;
    public int MaxEnemyCount = 10;
    public int spawnDelay = 2;

    private int currentCount = 0;
    private float lastTimeSpawned;
    private GameObject player;
    private float spawnOffset = 2f;

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

            if (currentCount >= MaxEnemyCount) { return; }

            SpawnEnemy();

            currentCount += 1;
        }

    }

    private void SpawnEnemy()
    {
        if (spawners.Length == 0) { return; }

        int randomIndex = Random.Range(0, spawners.Length);
        Transform selectedSpawner = spawners[randomIndex];

        Vector3 spawnDirection = (selectedSpawner.position - Camera.main.transform.position).normalized;
        Vector3 spawnPosition = selectedSpawner.position + spawnDirection * spawnOffset;
        spawnPosition.z = 0;

        Instantiate(Zombie, spawnPosition, Quaternion.identity, transform);
    }
}
