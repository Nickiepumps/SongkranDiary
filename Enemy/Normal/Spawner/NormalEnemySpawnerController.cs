using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Reference")]
    [SerializeField] private NormalEnemySpawner normalEnemySpawner;

    [Header("Spawn Setting")]
    public bool startSpawnOnStart;

    [Header("All Shooter and Bomber Enemies")]
    private List<GameObject> shooterLists = new List<GameObject>();
    private List<GameObject> bomberLists = new List<GameObject>();
    private List<GameObject> droneLists = new List<GameObject>();

    [Header("Enemy Spawn Rate")]
    [Header("Shooter")]
    [SerializeField] private float minShooterSpawnRate;
    [SerializeField] private float maxShooterSpawnRate;
    [Header("Bomber")]
    [SerializeField] private float minBomberSpawnRate;
    [SerializeField] private float maxBomberSpawnRate;
    [Header("Drone")]
    [SerializeField] private float minDroneSpawnRate;
    [SerializeField] private float maxDroneSpawnRate;

    private float spawnRate;
    private float currentTimeToSpawnShooter;
    private float currentTimeToSpawnBomber;
    private float currentTimeToSpawnDrone;
    private void Start()
    {
        currentTimeToSpawnShooter = 5;
        currentTimeToSpawnBomber = 10;
        currentTimeToSpawnDrone = 10;
    }
    private void Update()
    {
        SpawnNormalEnemy();
    }
    private void SpawnNormalEnemy()
    {
        currentTimeToSpawnShooter -= Time.deltaTime;
        currentTimeToSpawnBomber -= Time.deltaTime;
        currentTimeToSpawnDrone -= Time.deltaTime;
        if (startSpawnOnStart == true && currentTimeToSpawnShooter <= 0)
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
                    currentTimeToSpawnShooter = Random.Range(minShooterSpawnRate, maxShooterSpawnRate);
                    return;
                }
            }
        }
        if (startSpawnOnStart == true && currentTimeToSpawnBomber <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedBomberLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedBomberLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedBomberLists[i];
                    EnemyBomberStateController enemyBomberStateController = enemyGO.GetComponent<EnemyBomberStateController>();
                    SpriteRenderer spriteRenderer = enemyBomberStateController.enemySpriteRenderer;
                    Transform[] path = NewSpawnAndDestination(enemyGO, enemyBomberStateController.startPoint, enemyBomberStateController.destination, spriteRenderer);
                    enemyBomberStateController.startPoint = path[0];
                    enemyBomberStateController.destination = path[1];
                    enemyGO.SetActive(true);
                    currentTimeToSpawnBomber = Random.Range(minBomberSpawnRate, maxBomberSpawnRate);
                    return;
                }
            }
        }
        if (startSpawnOnStart == true && currentTimeToSpawnDrone <= 0)
        {
            for (int i = 0; i < normalEnemySpawner.spawnedDroneLists.Count; i++)
            {
                if (normalEnemySpawner.spawnedDroneLists[i].activeSelf == false)
                {
                    GameObject enemyGO = normalEnemySpawner.spawnedDroneLists[i];
                    EnemyDroneStateController enemyDroneStateController = enemyGO.GetComponent<EnemyDroneStateController>();
                    SpriteRenderer spriteRenderer = enemyDroneStateController.enemySpriteRenderer;
                    Transform[] path = NewSpawnAndDestination(enemyGO, enemyDroneStateController.startPoint, enemyDroneStateController.destination, spriteRenderer);
                    enemyDroneStateController.startPoint = path[0];
                    enemyDroneStateController.destination = path[1];
                    enemyGO.SetActive(true);
                    currentTimeToSpawnDrone = Random.Range(minDroneSpawnRate, maxDroneSpawnRate);
                    return;
                }
            }
        }
    }
    private Transform[] NewSpawnAndDestination(GameObject enemyGO, Transform enemyStartPoint, Transform enemyDestination, SpriteRenderer spriteRenderer)
    {
        Transform[] newPath = new Transform[2];
        int spawnValue = Random.Range(0, 2);
        float spawnXOffset = 0f;
        Transform previousStart = enemyStartPoint;
        Transform previousDestination = enemyDestination;
        Transform newStart;
        Transform newDestination;

        if(spawnValue == 0)
        {
            newStart = previousDestination;
            newDestination = previousStart;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if(holeRayCheck.collider.tag != "E_Boundary")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = 3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        else
        {
            newStart = previousStart;
            newDestination = previousDestination;
        }

        // Flip the sprite if the starting point is facing left
        // Check if there is a hole underneath the spawn point; If there is, spawn the enemy at 3 units beside the spawn point
        if (newStart.transform.eulerAngles == new Vector3(0, 0, 90))
        {
            spriteRenderer.flipX = false;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if (holeRayCheck.collider.tag != "Side_Floor")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = 3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        else
        {
            spriteRenderer.flipX = true;
            RaycastHit2D holeRayCheck = Physics2D.Raycast(newStart.position, Vector2.down, Mathf.Infinity);
            if (holeRayCheck.collider.tag != "Side_Floor")
            {
                Debug.Log("There is a hole under the spawn, spawn the enemy at 15 units beside the spawn point");
                spawnXOffset = -3f;
            }
            else
            {
                spawnXOffset = 0f;
            }
        }
        newPath[0] = newStart;
        newPath[1] = newDestination;
        enemyGO.transform.position = new Vector2(newPath[0].position.x + spawnXOffset, newPath[0].position.y);

        return newPath;
    }
}
