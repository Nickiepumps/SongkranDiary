using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Reference")]
    [SerializeField] private NormalEnemySpawner normalEnemySpawner;

    [Header("Spawn Setting")]
    [SerializeField] private bool startSpawnOnStart;

    [Header("All Shooter and Bomber Enemies")]
    private List<GameObject> shooterLists = new List<GameObject>();
    private List<GameObject> bomberLists = new List<GameObject>();

    [Header("Enemy Spawn Rate")]
    [Header("Shooter")]
    [SerializeField] private float minShooterSpawnRate;
    [SerializeField] private float maxShooterSpawnRate;
    [Header("Bomber")]
    [SerializeField] private float minBomberSpawnRate;
    [SerializeField] private float maxBomberSpawnRate;

    private float spawnRate;
    private float currentTimeToSpawn;
    private void Start()
    {
        currentTimeToSpawn = 5;
    }
    private void Update()
    {
        currentTimeToSpawn -= Time.deltaTime;
        if (startSpawnOnStart == true && currentTimeToSpawn <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedShooterLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedShooterLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedShooterLists[i];
                    EnemyShooterStateController enemyStateController = enemyGO.GetComponent<EnemyShooterStateController>();
                    SpriteRenderer spriteRenderer = enemyStateController.enemySpriteRenderer;
                    Transform[] path = NewSpawnAndDestination(enemyGO, enemyStateController.startPoint, enemyStateController.destination, spriteRenderer);
                    enemyStateController.startPoint = path[0];
                    enemyStateController.destination = path[1];
                    enemyGO.SetActive(true);
                    currentTimeToSpawn = Random.Range(minShooterSpawnRate, maxShooterSpawnRate);
                    return;
                }
            }
            for (int i = 0; i < normalEnemySpawner.spawnedBomberLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedBomberLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedBomberLists[i];
                    EnemyBomberStateController enemyBomberStateController = enemyGO.GetComponent<EnemyBomberStateController>();
                    SpriteRenderer spriteRenderer = enemyBomberStateController.enemySpriteRenderer;
                    Transform[] path = NewSpawnAndDestination(enemyGO,enemyBomberStateController.startPoint, enemyBomberStateController.destination, spriteRenderer);
                    enemyBomberStateController.startPoint = path[0];
                    enemyBomberStateController.destination = path[1];
                    enemyGO.SetActive(true);
                    currentTimeToSpawn = Random.Range(minBomberSpawnRate, maxBomberSpawnRate);
                    return;
                }
            }
        }
    }
    private Transform[] NewSpawnAndDestination(GameObject enemyGO, Transform enemyStartPoint, Transform enemyDestination, SpriteRenderer spriteRenderer)
    {
        Transform[] newPath = new Transform[2];
        int spawnValue = Random.Range(1, 10);
        Transform previousStart = enemyStartPoint;
        Transform previousDestination = enemyDestination;

        if(spawnValue > 5)
        {
            enemyDestination = previousStart;
            enemyStartPoint = previousDestination;
        }
        else
        {
            enemyDestination = previousDestination;
            enemyStartPoint = previousStart;
        }
        if (enemyStartPoint.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        newPath[0] = enemyStartPoint;
        newPath[1] = enemyDestination;
        enemyGO.transform.position = enemyStartPoint.position;
        return newPath;
    }
}
