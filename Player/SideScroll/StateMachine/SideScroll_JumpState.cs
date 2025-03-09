using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_JumpState : PlayerSideScrollStateMachine
{
    public SideScroll_JumpState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.playerAnimator.SetBool("Jump", true);
        playerSideScroll.playerCollider.size = new Vector2(playerSideScroll.playerCollider.size.x, playerSideScroll.playerStandColliderSizeY);
        playerSideScroll.playerRB.AddForce(Vector2.up * playerSideScroll.jumpForce, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        if (playerSideScroll.xDir != 0 || Input.GetAxisRaw("Horizontal") != 0) // Change to Run state
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }
        if (playerSideScroll.playerCurrentHP <= 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_DeadState(playerSideScroll));
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
        if (pCollider.gameObject.tag == "Side_Floor")
        {
            playerSideScroll.currentCollidername = pCollider;
            playerSideScroll.isPlayerOnGround = true;
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        playerSideScroll.currentCollidername = null;
    }
    public override void Exit()
    {

    }
}
