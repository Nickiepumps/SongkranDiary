using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyObserverController : MonoBehaviour, INormalEnemyObserver
{
    private NormalEnemySubject normalEnemySubject;
    private EnemyBulletPooler enemyBulletPooler;
    private EnemyShooterStateController enemyStats;

    [Header("Bullet Spawner")]
    [SerializeField] private Transform bulletRightSpawn;
    [SerializeField] private Transform bulletLeftSpawn;

    [Header("Enemy Sprite")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    [SerializeField] private Color enemyDamagedColor;
    private void Awake()
    {
        normalEnemySubject = GetComponent<EnemyShooterStateController>();
        enemyStats = GetComponent<EnemyShooterStateController>();
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
                    StartCoroutine(DamageIndicator()); // Enable damage flickering effect
                    enemyStats.currentEnemyHP--;
                }
                return;
            case(EnemyAction.Shoot):
                if(normalEnemySubject.GetComponent<EnemyShooterStateController>().enemySpriteRenderer.flipX == true)
                {
                    EnemyShoot(bulletRightSpawn,Vector2.right, false);
                }
                else
                {
                    EnemyShoot(bulletLeftSpawn, Vector2.left, true);
                }
                return;
            case (EnemyAction.Dead):
                enemySpriteRenderer.color = Color.white;
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
    private IEnumerator DamageIndicator()
    {
        enemySpriteRenderer.color = enemyDamagedColor;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.color = Color.white;
    }
}
