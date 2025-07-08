using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SideScroll_RunState : PlayerSideScrollStateMachine
{
    public SideScroll_RunState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    Vector2 moveDir;
    bool isRamp = false;
    Vector2 rampNormal;
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Run);
        playerSideScroll.playerAnimator.SetBool("Idle", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        playerSideScroll.playerAnimator.SetBool("Run", true);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        if (playerSideScroll.isPlayerOnGround == false)
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
            playerSideScroll.playerCollider.size = playerSideScroll.playerJumpColliderSize;
            playerSideScroll.playerCollider.offset = playerSideScroll.playerJumpColliderOffset;
        }
        else
        {
            playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
            playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
        }
        if(playerSideScroll.currentCollider != null)
        {
            playerSideScroll.isDash = false;
        }
        else
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
    }
    public override void Update()
    {
        playerSideScroll.xDir = Input.GetAxisRaw("Horizontal") * playerSideScroll.currentWalkSpeed;
        if (Input.GetAxisRaw("Horizontal") == 0 || playerSideScroll.playerBulletShooting.isAim == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        if(Input.GetKeyDown(KeyCode.I) && playerSideScroll.isPlayerOnGround == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
        }
        else if(Input.GetKeyDown(KeyCode.Space) && playerSideScroll.isPlayerOnGround == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerSideScroll.isDash == false)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DashState(playerSideScroll));
        }
        if (Input.GetKeyDown(KeyCode.S) && playerSideScroll.isPlayerOnGround == true)
        {
            if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
            }
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }

        if(playerSideScroll.isWinRunNGun == true)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_WinRunNGunState(playerSideScroll));
        }
    }
    public override void FixedUpdate()
    {
        if (playerSideScroll.playerRB.velocity.y > 0 && playerSideScroll.isJump == true)
        {
            playerSideScroll.playerRB.velocity += playerSideScroll.gravityVelocity * playerSideScroll.jumpMultiplier * Time.deltaTime;
        }
        if (playerSideScroll.playerRB.velocity.y < 0)
        {
            playerSideScroll.playerRB.velocity -= playerSideScroll.gravityVelocity * playerSideScroll.fallMultiplier * Time.deltaTime;
        }
        if(isRamp == false)
        {
            moveDir = new Vector2(playerSideScroll.xDir, playerSideScroll.playerRB.velocity.y);
            playerSideScroll.playerRB.velocity = moveDir;
        }
        else
        {
            playerSideScroll.playerRB.velocity = moveDir;
        }
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        
    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if(normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                if (playerSideScroll.isPlayerHighFall == true)
                {
                    playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[0];
                    playerSideScroll.playerMovementAudioSource.Play();
                }
                playerSideScroll.isPlayerOnGround = true;
                playerSideScroll.currentCollider = pCollider.collider;
                playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
                playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
                playerSideScroll.playerAnimator.SetBool("Jump", false);
            }
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if(normal != Vector2.right && normal.x > 0)
            {
                isRamp = true;
                // Calculate the speed when running uphil using ramp's x normal then add the remaining speed of player's normal speed
                float rampSpeed = (normal.x * playerSideScroll.xDir) + (playerSideScroll.walkSpeed - (normal.x * playerSideScroll.xDir));
                Debug.Log(rampSpeed);
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    moveDir = new Vector2(rampSpeed, -normal.y);
                }
                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    moveDir = new Vector2(-rampSpeed, normal.y);
                }
            }
            else if (normal != Vector2.left && normal.x < 0)
            {
                isRamp = true;
                // Calculate the speed when running uphil using ramp's x normal then add the remaining speed of player's normal speed
                float rampSpeed = (-normal.x * playerSideScroll.xDir) + (playerSideScroll.walkSpeed - (-normal.x * playerSideScroll.xDir));
                Debug.Log(rampSpeed);
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    moveDir = new Vector2(rampSpeed, normal.y);
                }
                else if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    moveDir = new Vector2(-rampSpeed, -normal.y);
                }
            }
            else
            {
                isRamp = false;
            }

            if (normal == Vector2.left)
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                {
                    playerSideScroll.currentWalkSpeed = 0f;
                }
                else
                {
                    playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
                }
            }
            else if (normal == Vector2.right)
            {
                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    playerSideScroll.currentWalkSpeed = 0f;
                }
                else
                {
                    playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
                }
            }
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            isRamp = false;
            playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
            if(pCollider.collider == playerSideScroll.currentCollider)
            {
                playerSideScroll.isPlayerOnGround = false;
                playerSideScroll.currentCollider = null;
            }
        }
    }
    public override void Exit()
    {
        
    }
}
