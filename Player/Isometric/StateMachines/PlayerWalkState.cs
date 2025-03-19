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
        #region Walk Animation Blend tree
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1) // Up
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 0f);
            player.ISOAnimator.SetFloat("PosY", 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1) // Up Right
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", 11f);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1) // Up Left
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", 11f);
        }
        else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1) // Down
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 0f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == -1) // Down Right
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1) // Down Left
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", -1f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 0) // Right
        {
            player.ISOPlayerSpriteRenderer.flipX = false;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0) // Left
        {
            player.ISOPlayerSpriteRenderer.flipX = true;
            player.ISOAnimator.SetFloat("PosX", 1f);
            player.ISOAnimator.SetFloat("PosY", 0f);
        }
        #endregion
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
