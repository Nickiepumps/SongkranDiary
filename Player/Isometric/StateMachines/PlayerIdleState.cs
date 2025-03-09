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
        float xDir = Input.GetAxis("Horizontal");
        float yDir = Input.GetAxis("Vertical");
        if (xDir != 0 || yDir != 0)
        {
            player.PlayerStateTransition(new PlayerWalkState(player));
        }
        if (Input.GetKeyDown(KeyCode.E) && player.currentColHit != null)
        {
            player.PlayerStateTransition(new PlayerInteractState(player));
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
        player.currentColHit = pCollision;
    }

    public override void OnExitTrigger(Collider2D pCollision)
    {
        player.currentColHit = null;
    }
}
