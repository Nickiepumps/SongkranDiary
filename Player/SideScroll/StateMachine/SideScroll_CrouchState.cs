using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_CrouchState : PlayerSideScrollStateMachine
{
    public SideScroll_CrouchState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    private bool isOnPlatform = false;
    public override void Start()
    {
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Crouch);
        playerSideScroll.playerAnimator.SetBool("Crouch", true);
        playerSideScroll.playerCollider.enabled = true;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerCrouchColliderOffset;
        playerSideScroll.playerCollider.size = playerSideScroll.playerCrouchColliderSize;
    }
    public override void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetAxisRaw("Horizontal") == 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetAxisRaw("Horizontal") != 0)
        {
            playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if(playerSideScroll.currentCollider.usedByEffector == false)
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
            }
            else
            {
                playerSideScroll.playerCollider.excludeLayers = playerSideScroll.platformDropdownLayerMaskExclude;
                playerSideScroll.currentCollider = null;
                playerSideScroll.isPlayerOnGround = false;
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_IdleState(playerSideScroll));
            }
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
            Vector2 normal = pCollider.GetContact(0).normal;
            if (normal.y <= 1 && normal.y > -1 && normal.y != 0)
            {
                playerSideScroll.currentCollider = pCollider.collider;
            }
            playerSideScroll.isFallen = false;
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {
       
    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void Exit()
    {

    }
}
