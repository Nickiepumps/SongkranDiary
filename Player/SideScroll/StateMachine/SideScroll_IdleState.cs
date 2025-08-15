using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_IdleState : PlayerSideScrollStateMachine
{
    public SideScroll_IdleState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Side_Idle);
        playerSideScroll.playerAnimator.SetBool("Idle", true);
        playerSideScroll.playerAnimator.SetBool("Run", false);
        playerSideScroll.playerAnimator.SetBool("Dash", false);
        if (playerSideScroll.isPlayerOnGround == false)
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
        playerSideScroll.playerAnimator.SetBool("Crouch", false);

        if(playerSideScroll.currentCollider != null)
        {
            playerSideScroll.isDash = false;
        }
        else
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
        playerSideScroll.currentWalkSpeed = playerSideScroll.walkSpeed;
        playerSideScroll.playerCollider.sharedMaterial = null;
        playerSideScroll.playerRB.sharedMaterial = null;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
        playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
    }
    public override void Update()
    {
        if (playerSideScroll.isGameStart == true)
        {
            playerSideScroll.xDir = Input.GetAxisRaw("Horizontal") * playerSideScroll.walkSpeed;
            if (playerSideScroll.playerRB.velocity.y < 0)
            {
                playerSideScroll.playerRB.velocity -= playerSideScroll.gravityVelocity * playerSideScroll.fallMultiplier * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Horizontal") != 0) // Change to Run state
            {
                if (playerSideScroll.playerBulletShooting.isAim == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
                }
            }
            if (Input.GetKeyDown(KeyCode.Z) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
            {
                if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
                }
            }
            if (Input.GetKey(KeyCode.DownArrow) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
            {
                if (playerSideScroll.GetComponent<BulletAiming>().isAimUp == false)
                {
                    playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) && playerSideScroll.isDash == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DashState(playerSideScroll));
            }
            if (playerSideScroll.playerCurrentHP <= 0)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
            }
        }
    }
    public override void FixedUpdate()
    {
        
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        
    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor" || pCollider.gameObject.tag == "Side_Interactable")
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if(normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                if (playerSideScroll.isPlayerHighFall == true)
                {
                    playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[0];
                    playerSideScroll.playerMovementAudioSource.Play();
                }
                if (pCollider.collider.usedByEffector == false)
                {
                    playerSideScroll.playerCollider.excludeLayers = playerSideScroll.floorLayerMaskExclude;
                }
                playerSideScroll.isFallen = false;
                playerSideScroll.isDash = false;
                playerSideScroll.isPlayerOnGround = true;
                playerSideScroll.playerCollider.enabled = true;
                playerSideScroll.currentCollider = pCollider.collider;
                playerSideScroll.playerAnimator.SetBool("Jump", false);
            }
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor" && pCollider.collider.usedByEffector == false)
        {
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal.x != -1 && normal.x != 1 )
            {
                playerSideScroll.playerRB.velocity = new Vector2(playerSideScroll.playerRB.velocity.x - (normal.x * 0.8f), playerSideScroll.playerRB.velocity.y);
            }
        }
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor" && pCollider.collider == playerSideScroll.currentCollider)
        {
            playerSideScroll.isPlayerOnGround = false;
            playerSideScroll.currentCollider = null;
        }
    }
    public override void Exit()
    {

    }
}
