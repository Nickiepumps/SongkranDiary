using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_CrouchState : PlayerSideScrollStateMachine
{
    public SideScroll_CrouchState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        playerSideScroll.playerAnimator.SetBool("Crouch", true);
        playerSideScroll.isCrouch = true;
        playerSideScroll.playerCollider.size = playerSideScroll.playerCrouchColliderSize;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerCrouchColliderOffset;
    }
    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) && Input.GetAxisRaw("Horizontal") == 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        else if (Input.GetKeyUp(KeyCode.S) && Input.GetAxisRaw("Horizontal") != 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
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

    }

    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void Exit()
    {

    }
}
