using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    [SerializeField] private GameSubject gameSubject;

    [Header("Boss BulletPooler Reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;
    [Header("Bullet Spawn Points")]
    [SerializeField] private Transform bulletLowerSpawn;
    [SerializeField] private Transform bulletMiddleSpawn;
    [SerializeField] private Transform bulletTopSpawn;

    private void Awake()
    {
        bossSubject = GetComponent<BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameSubject.AddGameObserver(this);
        gameSubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameSubject.RemoveGameObserver(this);
        gameSubject.RemoveSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case(BossAction.Idle):
                return;
            case (BossAction.Shoot):
                bossSubject.GetComponent<BossStateController>().bossShooting = true;
                if (RandomPattern() <= 5)
                {
                    StartCoroutine(ShootPattern(1));
                }
                else
                {
                    StartCoroutine(ShootPattern(2));
                }
                return;
            case (BossAction.Jump):
                Debug.Log("Boss Jumped");
                bossSubject.GetComponent<BossStateController>().bossRB.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
                return;
            case (BossAction.Ult):
                Debug.Log("Boss Ult Observer");
                return;
            case (BossAction.Damaged):
                Debug.Log("Boss Damaged");
                bossSubject.GetComponent<BossStateController>().currentBossHP--;
                return;
            case (BossAction.Die):
                gameSubject.NotifySideScrollGameObserver(SideScrollGameState.Win);
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        
    }

    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        
    }
    private int RandomPattern()
    {
        int shootValue = Random.Range(0, 10);
        return shootValue;
    }
    private IEnumerator ShootPattern(int patternValue)
    {
        if (patternValue == 1)
        {
            Debug.Log("Boss Shoot Pattern 1");
            BossShoot(bulletLowerSpawn, Vector2.left);

        }
        else
        {
            Debug.Log("Boss Shoot Pattern 2");
            BossShoot(bulletMiddleSpawn, Vector2.left);
        }
        yield return null;
        bossSubject.GetComponent<BossStateController>().bossShooting = false;
    }
    private void BossShoot(Transform spawnPos, Vector2 bulletDirection)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = bulletDirection;
            enemyBullet.SetActive(true);
        }
    }
}
