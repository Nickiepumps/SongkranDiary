using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawner : MonoBehaviour
{
    [Header("Enemy type to spawn")]
    [SerializeField] private bool allowShooterSpawn;
    [SerializeField] private bool allowBomberSpawn;
    [SerializeField] private bool allowStationarySpawn;
    [SerializeField] private bool allowDroneSpawn;

    [Header("Spawn Properties")]
    [SerializeField] private int shooterAmountToSpawn;
    [SerializeField] private int bomberAmountToSpawn;
    [SerializeField] private int droneAmountToSpawn;

    [Header("Spawn Group")]
    [SerializeField] private Transform spawnedShooterGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedBomberGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedStationaryGroup; // Prevent too many enemy gameObjects appear in Hierachy
    [SerializeField] private Transform spawnedDroneGroup; // Prevent too many enemy gameObjects appear in Hierachy

    [Header("Spawners and Prefab")]
    public List<Transform> shooterSpawnerlist;
    public List<Transform> bomberSpawnerlist;
    public List<Transform> droneSpawnerlist;
    [SerializeField] private List<Transform> stationarySpawnerlist;
    [SerializeField] private GameObject shooterEnemyPrefab;
    [SerializeField] private GameObject bomberEnemyPrefab;
    [SerializeField] private GameObject droneEnemyPrefab;
    public List<GameObject> spawnedShooterLists = new List<GameObject>();
    public List<GameObject> spawnedBomberLists = new List<GameObject>();
    public List<GameObject> spawnedDroneLists = new List<GameObject>();

    [Header("Enemy Scriptable Object")]
    [SerializeField] private NormalEnemySO shooterSO;
    [SerializeField] private NormalEnemySO bomberSO;
    [SerializeField] private NormalEnemySO stationarySO;
    [SerializeField] private NormalEnemySO droneSO;
    private void Start()
    {
        CreateNormalEnemy();
    }
    private void CreateNormalEnemy()
    {
        if (allowShooterSpawn == true)
        {
            for (int i = 0; i < shooterAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject shooterEnemy;
                if (spawnElement == 0)
                {
                    shooterEnemy = Instantiate(shooterEnemyPrefab, shooterSpawnerlist[0].position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    shooterEnemy = Instantiate(shooterEnemyPrefab, shooterSpawnerlist[1].position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedShooterLists.Add(shooterEnemy);
                shooterEnemy.SetActive(false);
            }
        }
        if (allowBomberSpawn == true)
        {
            for (int i = 0; i < bomberAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject bomberEnemy;
                if (spawnElement == 0)
                {
                    bomberEnemy = Instantiate(bomberEnemyPrefab, bomberSpawnerlist[0].position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().destination = bomberSpawnerlist[1];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = bomberSpawnerlist[0];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    bomberEnemy = Instantiate(bomberEnemyPrefab, bomberSpawnerlist[1].position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().destination = bomberSpawnerlist[0];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = bomberSpawnerlist[1];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedBomberLists.Add(bomberEnemy);
                bomberEnemy.SetActive(false);
            }
        }
        if (allowDroneSpawn == true)
        {
            for (int i = 0; i < droneAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 2); // Random Spawnpoint
                GameObject droneEnemy;
                if (spawnElement == 0)
                {
                    droneEnemy = Instantiate(droneEnemyPrefab, droneSpawnerlist[0].position, Quaternion.identity, spawnedDroneGroup);
                    droneEnemy.GetComponent<EnemyDroneStateController>().destination = droneSpawnerlist[1];
                    droneEnemy.GetComponent<EnemyDroneStateController>().startPoint = droneSpawnerlist[0];
                    droneEnemy.GetComponent<EnemyDroneStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    droneEnemy = Instantiate(droneEnemyPrefab, droneSpawnerlist[1].position, Quaternion.identity, spawnedDroneGroup);
                    droneEnemy.GetComponent<EnemyDroneStateController>().destination = droneSpawnerlist[0];
                    droneEnemy.GetComponent<EnemyDroneStateController>().startPoint = droneSpawnerlist[1];
                    droneEnemy.GetComponent<EnemyDroneStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedDroneLists.Add(droneEnemy);
                droneEnemy.SetActive(false);
            }
        }
        if (allowStationarySpawn == true)
        {

        }
    }
}
