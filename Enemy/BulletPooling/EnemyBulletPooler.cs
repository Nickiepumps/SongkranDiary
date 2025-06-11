using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooler : MonoBehaviour
{
    [Header("Enemy Bullet Prefab")]
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject incomingBulletPrefab;

    [Header("Pooling Property")]
    [SerializeField] private int amountTopool;
    [SerializeField] private int incomingBulletamountTopool;
    [SerializeField] private Transform pooledEnemyBulletGroup; // Prevent too many enemy bullet gameObjects appear in Hierachy
    [SerializeField] private Transform pooledIncomingBulletGroup;
    private List<GameObject> pooledEnemyBulletList = new List<GameObject>();
    private List<GameObject> pooledIncomingBulletList = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < amountTopool; i++)
        {
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, pooledEnemyBulletGroup);
            pooledEnemyBulletList.Add(enemyBullet);
            enemyBullet.SetActive(false);
        }
        for(int i = 0; i < incomingBulletamountTopool; i++)
        {
            GameObject incomingBullet = Instantiate(incomingBulletPrefab, pooledIncomingBulletGroup);
            pooledIncomingBulletList.Add(incomingBullet);
            incomingBullet.SetActive(false);
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
    public GameObject EnableIncomingBullet()
    {
        for(int i = 0; i < pooledIncomingBulletList.Count; i++)
        {
            if (pooledIncomingBulletList[i].activeSelf == false)
            {
                return pooledIncomingBulletList[i];
            }
        }
        return null;
    }
}
