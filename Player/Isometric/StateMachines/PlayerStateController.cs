using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateController : PlayerSubject
{
    public Rigidbody2D playerRB;
    public Vector2 playerVelocity;
    private PlayerState currentState;
    public float walkSpeed = 5;
    public bool playerTalking = false;
    public Collider2D currentColHit;
    private void Start()
    {
        PlayerStateTransition(new PlayerIdleState(this));
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        currentState.Update();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnEnterTrigger(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnExitTrigger(collision);
    }
    public void PlayerStateTransition(PlayerState newState)
    {
        currentState = newState;
        currentState.Start();
    }
}
