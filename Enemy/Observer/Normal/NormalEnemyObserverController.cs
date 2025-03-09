using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyObserverController : MonoBehaviour, INormalEnemyObserver
{
    private NormalEnemySubject normalEnemySubject;
    private EnemyBulletPooler enemyBulletPooler;
    private EnemyStateController enemyStats;

    [SerializeField] private Transform bulletRightSpawn;
    [SerializeField] private Transform bulletLeftSpawn;
    private void Awake()
    {
        normalEnemySubject = GetComponent<NormalEnemySubject>();
        enemyStats = GetComponent<EnemyStateController>();
    }
    private void Start()
    {
        enemyBulletPooler = GameObject.Find("EnemyBulletPooler").GetComponent<EnemyBulletPooler>();
    }
    private void OnEnable()
    {
        normalEnemySubject.AddNormalEnemyObserver(this);
    }
    private void OnDisable()
    {
        normalEnemySubject.RemoveNormalEnemyObserver(this);
    }
    public void OnNormalEnemyNotify(EnemyAction action)
    {
        switch (action)
        {
            case(EnemyAction.Damaged):
                Debug.Log("Notify Hit");
                if(enemyStats.currentEnemyHP > 0)
                {
                    enemyStats.currentEnemyHP--;
                }
                else
                {
                    
                }
                return;
            case(EnemyAction.Shoot):
                if(normalEnemySubject.GetComponent<EnemyStateController>().enemySpriteRenderer.flipX == true)
                {
                    EnemyShoot(bulletRightSpawn,Vector2.right, false);
                }
                else
                {
                    EnemyShoot(bulletLeftSpawn, Vector2.left, true);
                }
                return;
            case (EnemyAction.Explode):
                Debug.Log("Enemy Exploded");
                EnemyExplode();
                return;
            case (EnemyAction.Dead):
                Debug.Log("Enemy Dead");
                return;
        }
    }
    private void EnemyShoot(Transform spawnPos, Vector2 bulletDirection, bool isLeft)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if(enemyBullet != null)
        {
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = bulletDirection;
            if(isLeft == true)
            {
                enemyBullet.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            else
            {
                enemyBullet.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            enemyBullet.SetActive(true);
        }
    }
    private void EnemyExplode()
    {

    }
}
