using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BucketKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    [SerializeField] private GameSubject gameUISubject;
    private void Awake()
    {
        bossSubject = GetComponent<BucketKid_BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameUISubject.AddGameObserver(this);
        gameUISubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameUISubject.RemoveGameObserver(this);
        gameUISubject.RemoveSideScrollGameObserver(this);
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Idle):
                return;
            case (BossAction.Shoot):
                return;
            case (BossAction.Jump):
                return;
            case (BossAction.Ult):
                return;
            case(BossAction.Damaged):
                return;
            case (BossAction.Die):
                gameUISubject.NotifySideScrollGameObserver(SideScrollGameState.Win); // Why double notify?
                return;
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        
    }
}
