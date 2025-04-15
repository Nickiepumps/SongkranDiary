using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BucketKid_BossObserverController : MonoBehaviour, IBossObserver, IGameObserver
{
    [Header("Observer References")]
    private BossSubject bossSubject;
    private BucketKid_BossStateController bucketKidStateController;
    [SerializeField] private GameSubject gameUISubject;
    [SerializeField] private GameSubject sideScrollIntroSubject;
    private void Awake()
    {
        bossSubject = GetComponent<BucketKid_BossStateController>();
        bucketKidStateController = GetComponent<BucketKid_BossStateController>();
    }
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
        gameUISubject.AddGameObserver(this);
        gameUISubject.AddSideScrollGameObserver(this);
        sideScrollIntroSubject.AddSideScrollGameObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
        gameUISubject.RemoveGameObserver(this);
        gameUISubject.RemoveSideScrollGameObserver(this);
        sideScrollIntroSubject.RemoveSideScrollGameObserver(this);
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
        switch (sidescrollGameState)
        {
            case(SideScrollGameState.Play):
                return;
            case (SideScrollGameState.StartRound):
                bucketKidStateController.isGameStart = true;
                return;
            case (SideScrollGameState.EndRound):
                return;
            case (SideScrollGameState.Paused):
                return;
        }
    }
}
