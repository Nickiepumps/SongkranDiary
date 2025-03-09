using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserverController : MonoBehaviour, IGameObserver, IPlayerObserver, IShootingObserver
{
    [SerializeField] private GameType gameType;

    [Header("GameMode Type")]
    [SerializeField] private bool isISO; // Isometric mode
    [SerializeField] private bool isSideScroll; // Side-Scroll mode

    [Header("Player's StateMachine (Isometric level)")]
    [SerializeField] private PlayerStateController playerStateController;

    [Header("Player's StateMachine (SideScroll level)")]
    [SerializeField] private PlayerSideScrollStateController playerSideScrollStateController;

    [Header("Player Observer Subject")]
    [SerializeField] private PlayerSubject playerSubject;

    [Header("Game Observer Subject")]
    [SerializeField] private GameSubject gameSubject;
    [SerializeField] private GameSubject sidescrollGameSubject;

    [Header("Shooting Observer Subject")]
    [SerializeField] private ShootingSubject shootingSubject;

    [Space]
    [Space]
    [Header("Shooting Related References")]
    [Header("BulletShooting Reference")]
    [SerializeField] private BulletShooting bulletShooting;

    [Header("Bullet Pooler Referecne")]
    [SerializeField] private BulletPooler bulletPooler;

    [Space]
    [Space]
    [Header("UI Related References")]
    [SerializeField] private PlayerHealthDisplay healthDisplay;
    private void OnEnable()
    {
        gameSubject = GameObject.Find("GameUIController").GetComponent<GameSubject>();
        gameSubject.AddGameObserver(this);
        gameSubject.AddSideScrollGameObserver(this);
        playerSubject.AddPlayerObserver(this);
        if(isSideScroll == true)
        {
            shootingSubject.AddShootingObserver(this);
        }
    }
    private void OnDisable()
    {
        gameSubject.RemoveGameObserver(this);
        gameSubject.RemoveSideScrollGameObserver(this);
        playerSubject.RemovePlayerObserver(this);
        if (isSideScroll == true)
        {
            shootingSubject.RemoveShootingObserver(this);
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                Debug.Log("Transition from interact to idle");
                playerStateController.PlayerStateTransition(new PlayerIdleState(playerStateController));
                return;
            case (IsometricGameState.Upgrade):
                return;
            case (IsometricGameState.Paused):
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {

    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        if (isISO == true)
        {
            switch (playerAction)
            {
                case (PlayerAction.Idle):
                    return;
            }
        }
        else
        {
            switch (playerAction)
            {
                case (PlayerAction.Damaged):
                    Debug.Log("Player Damaged");
                    if(playerSideScrollStateController.playerCurrentHP > 0 && playerSideScrollStateController.isDamaged == false)
                    {
                        healthDisplay.DecreaseHealth(playerSideScrollStateController.playerCurrentHP);
                        playerSideScrollStateController.playerCurrentHP--;
                        playerSideScrollStateController.isDamaged = true;
                    }
                    return;
                case(PlayerAction.Dead):
                    gameSubject.NotifySideScrollGameObserver(SideScrollGameState.Lose);
                    return;
                case (PlayerAction.win):
                    return;
            }
        }
    }
    public void OnShootingNotify(ShootingAction shootingAction)
    {
        switch (shootingAction)
        {
            case (ShootingAction.aimright):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                return;
            case (ShootingAction.aim45topright):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                return;
            case (ShootingAction.aimleft):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                return;
            case (ShootingAction.aim45topleft):
                playerSubject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                return;
        }
    }
}
