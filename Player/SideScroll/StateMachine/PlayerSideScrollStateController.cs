using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideScrollStateController : PlayerSubject
{
    [Header("Player Component References")]
    public Rigidbody2D playerRB;
    public BoxCollider2D playerCollider;
    public SpriteRenderer playerSpriteRenderer;
    public Vector2 playerVelocity;
    private PlayerSideScrollStateMachine currentState;
    public BulletShooting playerBulletShooting;
    
    [Header("Player Animator Reference")]
    public Animator playerAnimator;

    [Header("Player Audio Reference")]
    public AudioSource playerStatusAudioSource;
    public AudioSource playerMovementAudioSource;
    public AudioClip[] playerAudioClipArr;

    [Header("Player Stats")]
    [SerializeField] private PlayerStats currentPlayerStats;
    public int playerCurrentHP;
    public int playerMaxHP;
    public int playerUltAmount = 0;
    public int playerMaxUltChargeTime;
    public int playerCurrentUltChargeTime = 0;

    [Header("Side-Scroll Player Properties")]
    public float walkSpeed = 5; // Player's walk speed
    public float dashSpeed = 10f; // Player's dash speed
    public float jumpForce = 5f; // Player's jump force. Uses at the start of the jump
    public float jumpMultiplier = 1.5f; // Player jumping multiplier. Uses after the jump if the player is holding jump button
    public float fallMultiplier = 1.5f; // Player falling multiplier.
    const float damageImmunityTime = 3f; // Player's invulnerability time, start countdown when player hit by enemy
    public Vector2 playerStandColliderSize; // Player's collider width and height values when standing
    public Vector2 playerStandColliderOffset; // Player's collider X and Y position offset values when standing
    public Vector2 playerCrouchColliderSize; // Player's collider width and height values when crouching
    public Vector2 playerCrouchColliderOffset; // Player's collider X and Y position offset values when crouching
    public Vector2 playerJumpColliderSize; // Player's collider X and Y position offset values when jumping
    public Vector2 playerJumpColliderOffset; // Player's collider X and Y position offset values when jumping
    public bool isPlayerOnGround = true; // Standing on the ground status
    public bool isPlayerHighFall = false;
    public bool isCrouch; // Crouching status
    public bool isJump; // Jumping status
    public bool isDash; // Dashing status
    public bool isShoot; // Shooting status
    public bool isDamaged = false; // Damaging status
    public bool isDead = false; // Dead status
    public bool isWinBoss = false; // win boss stage status
    public bool isWinRunNGun = false; // win runNgun stage status

    // Hide in inspector
    public float currentWalkSpeed;
    public Vector2 currentVelocity;
    public Vector2 gravityVelocity;
    public Collider2D currentCollider;
    public string currentColliderName;
    public float currentJumpButtonHoldingTime;
    private float currentImmunityTime;
    private float currentAirborneTime;
    public float xDir;
    public float currentASPD;
    public bool isGameStart = false;
    private float aspd;
    private float spriteFlashingTime = 0.2f;
    private float spriteFlashingTimer;
    private bool spriteActive = false;
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        playerStandColliderSize = new Vector2(playerCollider.size.x, playerCollider.size.y);
        playerStandColliderOffset = new Vector2(playerCollider.offset.x, playerCollider.offset.y);
        playerCrouchColliderSize = new Vector2(playerStandColliderSize.x, 1f);
        playerCrouchColliderOffset = new Vector2(playerStandColliderOffset.x, -0.33f);

        currentWalkSpeed = walkSpeed;
        gravityVelocity = -Physics2D.gravity;
        currentASPD = currentPlayerStats.currentNormalASPD.aspd;
        aspd = currentASPD;
        playerMaxHP = currentPlayerStats.currentPlayerHP.hpPoint;
        playerCurrentHP = playerMaxHP;
        playerMaxUltChargeTime = currentPlayerStats.currentPlayerUltCharge.ultChargeTime;
        currentImmunityTime = damageImmunityTime;
        spriteFlashingTimer = 0;
        PlayerSideScrollStateTransition(new SideScroll_IdleState(this));
    }
    private void Update()
    {
        currentState.Update();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.I) && currentJumpButtonHoldingTime < 1)
        {
            isJump = true;
            if(currentJumpButtonHoldingTime < 1)
            {
                currentJumpButtonHoldingTime += Time.fixedDeltaTime;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.I) || currentJumpButtonHoldingTime >= 1)
        {
            currentJumpButtonHoldingTime = 0;
            isJump = false;
        }

        // Invulnability timer
        if (isDamaged == true)
        {
            spriteFlashingTimer -= Time.deltaTime;
            currentImmunityTime -= Time.deltaTime;
            // Sprite flashing invulnaibilty 
            if (spriteFlashingTimer <= 0)
            {
                if (spriteActive == true)
                {
                    playerSpriteRenderer.enabled = true;
                    spriteFlashingTimer = spriteFlashingTime;
                    spriteActive = false;
                }
                else
                {
                    playerSpriteRenderer.enabled = false;
                    spriteFlashingTimer = spriteFlashingTime;
                    spriteActive = true;
                }
            }
            if (currentImmunityTime <= 0)
            {
                currentImmunityTime = damageImmunityTime;
                playerSpriteRenderer.enabled = true;
                isDamaged = false;
            }
        }
        if (isPlayerOnGround == false)
        {
            currentAirborneTime += Time.deltaTime;
            if (currentAirborneTime >= 0.5f)
            {
                isPlayerHighFall = true;
            }
        }
        else
        {
            isPlayerHighFall = false;
            currentAirborneTime = 0;
        }
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged == false)
        {
            switch (collision.tag)
            {
                case ("EnemyHitBox"): // For Player to collide with
                    NotifyPlayerObserver(PlayerAction.Damaged);
                    return;
                case ("EnemyBullet"):
                    NotifyPlayerObserver(PlayerAction.Damaged);
                    return;
                case ("DamageObstacle"):
                    NotifyPlayerObserver(PlayerAction.Damaged);
                    return;
                case ("BlindHitBox"):
                    NotifyPlayerObserver(PlayerAction.Blind);
                    return;
            }
        }
        switch (collision.tag)
        {
            case ("HealBullet"):
                NotifyPlayerObserver(PlayerAction.Heal);
                return;
            case ("E_Boundary"):
                playerCurrentHP = 0;
                return;
        }
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        currentState.OnColliderStay(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentState.OnColliderExit(collision);
    }
    public void PlayerSideScrollStateTransition(PlayerSideScrollStateMachine newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }   
        currentState = newState;
        currentState.Start();
    }
}
