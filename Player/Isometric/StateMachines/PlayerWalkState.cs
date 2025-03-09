using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerStateController player) : base(player) { }
    private float xDir, yDir;
    public override void Start()
    {
        
    }
    public override void FixedUpdate()
    {
        Vector2 moveDir = new Vector2(xDir, yDir);
        moveDir = Vector2.ClampMagnitude(moveDir, player.walkSpeed);
        player.playerRB.velocity = moveDir;
    }

    public override void Update()
    {
        player.NotifyPlayerObserver(PlayerAction.Walk);
        xDir = Input.GetAxisRaw("Horizontal") * player.walkSpeed;
        yDir = Input.GetAxisRaw("Vertical") * player.walkSpeed;

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            player.PlayerStateTransition(new PlayerIdleState(player));
        }
        if (Input.GetKeyDown(KeyCode.E) && player.currentColHit != null)
        {
            player.PlayerStateTransition(new PlayerInteractState(player));
        }
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
