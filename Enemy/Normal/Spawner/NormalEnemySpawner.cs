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

    [Header("Spawn Group")]
    [SerializeField] private Transform spawnedEnemyGroup; // Prevent too many enemy gameObjects appear in Hierachy

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
                    shooterEnemy = Instantiate(enemyPrefab, shooterSpawnerlist[0].position, Quaternion.identity, spawnedEnemyGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = true;
                }
                else
                {
                    shooterEnemy = Instantiate(enemyPrefab, shooterSpawnerlist[1].position, Quaternion.identity, spawnedEnemyGroup);
                    shooterEnemy.GetComponent<EnemyShooterStateController>().destination = shooterSpawnerlist[0];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().startPoint = shooterSpawnerlist[1];
                    shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX = false;
                }
                spawnedShooterLists.Add(shooterEnemy);
                shooterEnemy.GetComponent<EnemyShooterStateController>().normalEnemyType = shooterSO.NormalEnemyType;
                shooterEnemy.GetComponent<EnemyShooterStateController>().enemyHP = shooterSO.hp;
                shooterEnemy.GetComponent<EnemyShooterStateController>().enemyASPD = shooterSO.aspd;
                shooterEnemy.GetComponent<EnemyShooterStateController>().walkSpeed = shooterSO.movementSpeed;
                shooterEnemy.GetComponent<EnemyShooterStateController>().damage = shooterSO.damage;
                shooterEnemy.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.sprite = shooterSO.normalSprite;
                shooterEnemy.SetActive(false);
            }
        }
        if (allowBomberSpawn == true)
        {

        }
        if (allowStationarySpawn == true)
        {

        }
    }
}
