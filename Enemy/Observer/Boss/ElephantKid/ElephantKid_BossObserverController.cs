using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    private ElephantKid_BossStateController elephantKidStateController;
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sideScrollGameSubject;
    [SerializeField] private GameSubject sideScrollIntroGameSubject;

    [Header("Player Reference")]
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Boss BulletPooler Reference")]
    [SerializeField] private EnemyBulletPooler enemyBulletPooler;

    [Header("Kid bullet spawner")]
    [SerializeField] private Transform kidBulletSpawner;
    private void Awake()
    {
        bossSubject = GetComponent<ElephantKid_BossStateController>();
        elephantKidStateController = GetComponent<ElephantKid_BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameUISubject.AddGameObserver(this);
        sideScrollGameSubject.AddSideScrollGameObserver(this);
        sideScrollIntroGameSubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameUISubject.RemoveGameObserver(this);
        sideScrollGameSubject.RemoveSideScrollGameObserver(this);
        sideScrollIntroGameSubject.RemoveSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Idle):
                return;
            case (BossAction.Shoot):
                StartCoroutine(Boss_KidShoot());
                return;
            case (BossAction.Ult):
                return;
            case (BossAction.Ult2):
                return;
            case(BossAction.Damaged):
                return;
            case(BossAction.Heal):
                return;
            case (BossAction.Die):
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState) { }

    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case(SideScrollGameState.StartRound):
                elephantKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.Play):
                elephantKidStateController.isGameStart = true;
                return;
            case(SideScrollGameState.Paused):
                elephantKidStateController.isGameStart = false;
                return;
        }
    }
    private IEnumerator Boss_KidShoot()
    {
        // Play boss throwing balloon anim
        elephantKidStateController.isKidAttack = true;
        Boss_KidShootBulletSpawner(kidBulletSpawner);
        yield return new WaitForSeconds(0.5f);
        elephantKidStateController.isKidAttack = false;
    }
    private void Boss_KidShootBulletSpawner(Transform spawnPos)
    {
        GameObject enemyBullet = enemyBulletPooler.EnableEnemyBullet();
        if (enemyBullet != null)
        {
            Vector3 lookDirection = playerSideScrollStateController.transform.position - spawnPos.position;
            float rotAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
            rotAngle = Random.Range(rotAngle - 5, rotAngle + 5);
            spawnPos.transform.localRotation = Quaternion.Euler(0, 0, rotAngle);
            enemyBullet.GetComponent<EnemyBullet>().bulletDirection = lookDirection;
            enemyBullet.transform.position = spawnPos.position;
            enemyBullet.transform.rotation = spawnPos.rotation;
            enemyBullet.SetActive(true);
        }
    }
}
