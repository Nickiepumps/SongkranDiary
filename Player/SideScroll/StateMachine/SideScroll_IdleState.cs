using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_IdleState : PlayerSideScrollStateMachine
{
    public SideScroll_IdleState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.isCrouch = false;
        //playerSideScroll.playerHandAnimator.SetBool("Idle", true);
        //playerSideScroll.playerHeadAnimator.SetBool("Idle", true);
        //playerSideScroll.playerHeadAnimator.SetBool("Run", false);
        //playerSideScroll.playerHandAnimator.SetBool("Run", false);
        playerSideScroll.playerAnimator.SetBool("Idle", true);
        playerSideScroll.playerAnimator.SetBool("Run", false);
        //playerSideScroll.playerAnimator.SetBool("TestRun", false);
        playerSideScroll.playerAnimator.SetBool("Jump", false);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        playerSideScroll.playerCollider.size = new Vector2(playerSideScroll.playerCollider.size.x, playerSideScroll.playerStandColliderSizeY);
        playerSideScroll.playerCollider.offset = new Vector2(playerSideScroll.playerCollider.offset.x, 0);
    }
    public override void Update()
    {
        playerSideScroll.xDir = Input.GetAxisRaw("Horizontal") * playerSideScroll.walkSpeed;
        if (playerSideScroll.xDir != 0 ) // Change to Run state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
        }
        if (Input.GetKeyDown(KeyCode.S) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
        }
        if(playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }
    }
    public override void FixedUpdate()
    {
        
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        if (pCollider.gameObject.tag == "EnemyHitBox" || pCollider.gameObject.tag == "EnemyBullet")
        {
            if (playerSideScroll.isDamaged == false)
            {
                playerSideScroll.NotifyPlayerObserver(PlayerAction.Damaged);
            }
        }
    }
    public override void OntriggerExit(Collider2D pCollider)
    {

    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.isPlayerOnGround = true;
            playerSideScroll.currentCollidername = pCollider;
        }
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        if(pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.currentCollidername = null;
        }
    }
    public override void Exit()
    {

    }
}
