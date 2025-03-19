using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Reference")]
    [SerializeField] private NormalEnemySpawner normalEnemySpawner;

    [Header("All Shooter and Bomber Enemies")]
    private List<GameObject> shooterLists = new List<GameObject>();
    private List<GameObject> bomberLists = new List<GameObject>();

    [Header("Spawn Rate")]
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    private float spawnRate;
    private float currentTimeToSpawn;
    private void Start()
    {
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        currentTimeToSpawn = spawnRate;
    }
    private void Update()
    {
        for (int i = 0; i < normalEnemySpawner.spawnedShooterLists.Count; i++)
        {
            if (normalEnemySpawner.spawnedShooterLists[i].activeSelf == false)
            {
                currentTimeToSpawn -= Time.deltaTime;
                if(currentTimeToSpawn < 0)
                {
                    NewSpawnAndDestination(normalEnemySpawner.spawnedShooterLists[i].GetComponent<EnemyShooterStateController>());
                    spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
                    currentTimeToSpawn = spawnRate;
                }
            }
        }
    }
    private void NewSpawnAndDestination(EnemyShooterStateController enemy)
    {
        int spawnValue = Random.Range(1, 10);
        Transform previousStart = enemy.startPoint;
        Transform previousDestination = enemy.destination;

        if(spawnValue > 5)
        {
            enemy.destination = previousStart;
            enemy.startPoint = previousDestination;
        }
        else
        {
            enemy.destination = previousDestination;
            enemy.startPoint = previousStart;
        }
        if (enemy.startPoint.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            enemy.enemySpriteRenderer.flipX = false;
        }
        else
        {
            enemy.enemySpriteRenderer.flipX = true;
        }
        enemy.transform.position = enemy.startPoint.position;
        enemy.gameObject.SetActive(true);
    }

}
