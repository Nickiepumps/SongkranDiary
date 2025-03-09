using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideScrollStateController : PlayerSubject
{
    [Header("Player Component References")]
    public Rigidbody2D playerRB;
    public BoxCollider2D playerCollider;
    public Vector2 playerVelocity;
    private PlayerSideScrollStateMachine currentState;
    
    [Header("Player Animator Reference")]
    public Animator playerAnimator;

    [Header("Player Stats")]
    [SerializeField] private PlayerStats currentPlayerStats;
    public int playerCurrentHP;
    public int playerMaxHP;
    public int playerUltAmount = 0;
    public int playerMaxUltChargeTime;
    public int playerCurrentUltChargeTime = 0;

    [Header("Side-Scroll Player Properties")]
    public float walkSpeed = 5; // Player's walk speed
    public float jumpForce = 5f; // Player's jump force
    const float damageImmunityTime = 3f; // Player's immunity time, start countdown when player hit by enemy
    public float playerStandColliderSizeY = 0.7f;
    public float playerCrouchColliderSizeY = 0.39f;
    public float playerCrouchColliderOffsetY = -0.14f;
    public bool isPlayerOnGround; // Standing on the ground status
    public bool isCrouch;
    public bool isDamaged = false;
    public bool isDead = false;
    public bool isWin = false;

    public Vector2 currentVelocity;
    public Collision2D currentCollidername;
    private float currentImmunityTime;
    public float xDir;
    public float currentASPD;
    private float aspd;
    private void Start()
    {
        PlayerSideScrollStateTransition(new SideScroll_IdleState(this));
        playerRB = GetComponent<Rigidbody2D>();
        currentASPD = currentPlayerStats.currentNormalASPD.aspd;
        aspd = currentASPD;
        playerMaxHP = currentPlayerStats.currentPlayerHP.hpPoint;
        playerCurrentHP = playerMaxHP;
        playerMaxUltChargeTime = currentPlayerStats.currentPlayerUltCharge.ultChargeTime;
        currentImmunityTime = damageImmunityTime;
    }
    private void Update()
    {
        currentState.Update();
        if(isDamaged == true)
        {
            currentImmunityTime -= Time.deltaTime;
            if(currentImmunityTime <= 0)
            {
                currentImmunityTime = damageImmunityTime;
                isDamaged = false;
            }
        }
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OntriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OntriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentState.OnColliderExit(collision);
    }
    public void PlayerSideScrollStateTransition(PlayerSideScrollStateMachine newState)
    {
        currentState = newState;
        currentState.Start();
    }
}
