using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_RunState : PlayerSideScrollStateMachine
{
    public SideScroll_RunState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {

        //playerSideScroll.playerHandAnimator.SetBool("Idle", false);
        //playerSideScroll.playerHeadAnimator.SetBool("Idle", false);
        //playerSideScroll.playerHeadAnimator.SetBool("Run", true);
        //playerSideScroll.playerHandAnimator.SetBool("Run", true);
        playerSideScroll.playerAnimator.SetBool("Idle", false);
        playerSideScroll.playerAnimator.SetBool("Run", true);
        //playerSideScroll.playerAnimator.SetBool("TestRun", true);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        if (playerSideScroll.isPlayerOnGround == false)
        {
            playerSideScroll.playerAnimator.SetBool("Jump", true);
        }
        playerSideScroll.playerCollider.size = new Vector2(playerSideScroll.playerCollider.size.x, playerSideScroll.playerStandColliderSizeY);
        playerSideScroll.playerCollider.offset = new Vector2(playerSideScroll.playerCollider.offset.x, 0);
    }
    public override void Update()
    {
        playerSideScroll.xDir = Input.GetAxisRaw("Horizontal") * playerSideScroll.walkSpeed;
        if (playerSideScroll.xDir < 0 && playerSideScroll.GetComponentInChildren<SpriteRenderer>().flipX == false)
        {
            // To Do: Change anim to Runback Left
        }
        else if (playerSideScroll.xDir > 0 && playerSideScroll.GetComponentInChildren<SpriteRenderer>().flipX == true)
        {
            // To Do: Change anim to Runback Right
        }

        if (Input.GetAxis("Horizontal") == 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        if (Input.GetKeyDown(KeyCode.Space) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
        }
        if (Input.GetKeyDown(KeyCode.S) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_CrouchState(playerSideScroll));
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
        }
    }
    public override void FixedUpdate()
    {
        Vector2 moveDir = new Vector2(playerSideScroll.xDir, playerSideScroll.playerRB.velocity.y);
        playerSideScroll.playerRB.velocity = moveDir;
    }
    public override void OntriggerEnter(Collider2D pCollider)
    {
        if (pCollider.gameObject.tag == "EnemyHitBox" || pCollider.gameObject.tag == "EnemyBullet")
        {
            if(playerSideScroll.isDamaged == false)
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
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.isPlayerOnGround = true;
            playerSideScroll.currentCollidername = pCollider;
            playerSideScroll.playerAnimator.SetBool("Jump", false);
        }
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.currentCollidername = null;
        }
    }
    public override void Exit()
    {

    }
}
