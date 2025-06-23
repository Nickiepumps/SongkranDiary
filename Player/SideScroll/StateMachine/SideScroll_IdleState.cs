using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_IdleState : PlayerSideScrollStateMachine
{
    public SideScroll_IdleState(PlayerSideScrollStateController playerSideScroll) : base(playerSideScroll) { }
    public override void Start()
    {
        //playerSideScroll.isCrouch = false;
        //playerSideScroll.playerHandAnimator.SetBool("Idle", true);
        //playerSideScroll.playerHeadAnimator.SetBool("Idle", true);
        //playerSideScroll.playerHeadAnimator.SetBool("Run", false);
        //playerSideScroll.playerHandAnimator.SetBool("Run", false);
        //playerSideScroll.playerAnimator.SetBool("TestRun", false);
        playerSideScroll.NotifyPlayerObserver(PlayerAction.Side_Idle);
        playerSideScroll.playerAnimator.SetBool("Idle", true);
        playerSideScroll.playerAnimator.SetBool("Run", false);
        playerSideScroll.playerAnimator.SetBool("Jump", false);
        playerSideScroll.playerAnimator.SetBool("Crouch", false);
        playerSideScroll.playerCollider.size = playerSideScroll.playerStandColliderSize;
        playerSideScroll.playerCollider.offset = playerSideScroll.playerStandColliderOffset;
    }
    public override void Update()
    {
        if (playerSideScroll.isGameStart == true)
        {
            playerSideScroll.xDir = Input.GetAxisRaw("Horizontal") * playerSideScroll.walkSpeed;
            if (playerSideScroll.xDir != 0) // Change to Run state
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_RunState(playerSideScroll));
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
            {
                playerSideScroll.PlayerSideScrollStateTransition(new SideScroll_JumpState(playerSideScroll));
            }
            if (Input.GetKey(KeyCode.S) && playerSideScroll.isPlayerOnGround == true) // Change to Jump state
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
        if(pCollider.gameObject.tag == "Side_Floor")
        {
            if (playerSideScroll.isPlayerHighFall == true)
            {
                playerSideScroll.playerMovementAudioSource.clip = playerSideScroll.playerAudioClipArr[0];
                playerSideScroll.playerMovementAudioSource.Play();
            }
            playerSideScroll.isPlayerOnGround = true;
            playerSideScroll.currentCollidername = pCollider;
        }
    }
    public override void OnColliderStay(Collision2D pCollider)
    {

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
