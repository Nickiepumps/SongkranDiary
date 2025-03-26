using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawner : MonoBehaviour
{
    [Header("Enemy type to spawn")]
    [SerializeField] private bool allowShooterSpawn;
    [SerializeField] private bool allowBomberSpawn;
    [SerializeField] private bool allowStationarySpawn;

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
    [SerializeField] private List<Transform> stationarySpawnerlist;
    [SerializeField] private GameObject enemyPrefab;
    public List<GameObject> spawnedShooterLists = new List<GameObject>();
    public List<GameObject> spawnedBomberLists = new List<GameObject>();

    [Header("Enemy Scriptable Object")]
    [SerializeField] private NormalEnemySO shooterSO;
    [SerializeField] private NormalEnemySO bomberSO;
    [SerializeField] private NormalEnemySO stationarySO;
    private void Start()
    {
        if (allowShooterSpawn == true)
        {
            for(int i = 0; i < shooterAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 5); // Random Spawnpoint
                GameObject shooterEnemy;
                if (spawnElement > 2)
                {
                    shooterEnemy = Instantiate(enemyPrefab, shooterSpawnerlist[0].position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    shooterEnemy = Instantiate(enemyPrefab, shooterSpawnerlist[1].position, Quaternion.identity, spawnedShooterGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedShooterLists.Add(shooterEnemy);
                //shooterEnemy.GetComponent<EnemyShooterStateController>().normalEnemyType = shooterSO.NormalEnemyType;
                //shooterEnemy.GetComponent<EnemyShooterStateController>().enemyHP = shooterSO.hp;
                //shooterEnemy.GetComponent<EnemyShooterStateController>().enemyASPD = shooterSO.aspd;
                //shooterEnemy.GetComponent<EnemyShooterStateController>().walkSpeed = shooterSO.movementSpeed;
                //shooterEnemy.GetComponent<EnemyShooterStateController>().damage = shooterSO.damage;
                //shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.sprite = shooterSO.normalSprite;
                shooterEnemy.SetActive(false);
            }
        }
        if (allowBomberSpawn == true)
        {
            for (int i = 0; i < bomberAmountToSpawn; i++)
            {
                int spawnElement = Random.Range(0, 5); // Random Spawnpoint
                GameObject bomberEnemy;
                if (spawnElement > 2)
                {
                    bomberEnemy = Instantiate(enemyPrefab, bomberSpawnerlist[0].position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().destination = bomberSpawnerlist[1];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = bomberSpawnerlist[0];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    bomberEnemy = Instantiate(enemyPrefab, bomberSpawnerlist[1].position, Quaternion.identity, spawnedBomberGroup);
                    bomberEnemy.GetComponent<EnemyBomberStateController>().destination = bomberSpawnerlist[0];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().startPoint = bomberSpawnerlist[1];
                    bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedShooterLists.Add(bomberEnemy);
                bomberEnemy.GetComponent<EnemyBomberStateController>().normalEnemyType = bomberSO.NormalEnemyType;
                bomberEnemy.GetComponent<EnemyBomberStateController>().enemyHP = bomberSO.hp;
                bomberEnemy.GetComponent<EnemyBomberStateController>().walkSpeed = bomberSO.movementSpeed;
                bomberEnemy.GetComponent<EnemyBomberStateController>().damage = bomberSO.damage;
                bomberEnemy.GetComponent<EnemyBomberStateController>().enemySpriteRenderer.sprite = bomberSO.normalSprite;
                bomberEnemy.SetActive(false);
            }
        }
        if (allowStationarySpawn == true)
        {

        }
    }
}
