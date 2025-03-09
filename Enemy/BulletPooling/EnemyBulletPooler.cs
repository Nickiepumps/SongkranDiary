using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooler : MonoBehaviour
{
    [Header("Enemy Bullet Prefab")]
    [SerializeField] private GameObject enemyBulletPrefab;

    [Header("Pooling Property")]
    [SerializeField] private int amountTopool;
    [SerializeField] private Transform pooledEnemyBulletGroup; // Prevent too many enemy bullet gameObjects appear in Hierachy
    private List<GameObject> pooledEnemyBulletList = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < amountTopool; i++)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, pooledEnemyBulletGroup);
            pooledEnemyBulletList.Add(enemyBullet);
            enemyBullet.SetActive(false);
        }
    }
    public GameObject EnableEnemyBullet()
    {
        for(int i = 0; i < pooledEnemyBulletList.Count; i++)
        {
            if (pooledEnemyBulletList[i].activeSelf == false)
            {
                return pooledEnemyBulletList[i];
            }
        }
        return null;
    }
}
