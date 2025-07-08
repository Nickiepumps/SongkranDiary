using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateController player) : base(player) { }
    public override void Start()
    {
        player.playerTalking = false;
        player.playerVelocity = Vector2.zero;
        player.playerRB.velocity = player.playerVelocity;
    }

    public override void Update()
    {
        player.NotifyPlayerObserver(PlayerAction.Idle);
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");
        if (xDir != 0 || yDir != 0)
        {
            player.PlayerStateTransition(new PlayerWalkState(player));
        }
        if (Input.GetKeyDown(KeyCode.E) && player.currentColHit != null)
        {
            player.PlayerStateTransition(new PlayerInteractState(player));
        }

        // Idle Animation Blend tree
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)
        {
            player.ISOAnimator.SetFloat("PosX", 0);
            player.ISOAnimator.SetFloat("PosY", 1);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)
        {
            player.ISOAnimator.SetFloat("PosX", 0);
            player.ISOAnimator.SetFloat("PosY", -1);
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void Exit()
    {
        
    }
    public override void OnEnterTrigger(Collider2D pCollision)
    {
        if(player.currentColHit == null)
        {
            player.currentColHit = pCollision;
        }
    }

    public override void OnExitTrigger(Collider2D pCollision)
    {
        
    }
}
