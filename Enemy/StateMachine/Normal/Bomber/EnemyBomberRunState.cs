using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberRunState : EnemyStateMachine
{
    public EnemyBomberRunState(EnemyBomberStateController bomberEnemy) : base(bomberEnemy) { }
    private Vector2 moveDir;
    public override void Start()
    {
        Debug.Log("RunState");
        bomberEnemy.transform.position = new Vector2(bomberEnemy.startPoint.position.x, bomberEnemy.transform.position.y);
    }
    public override void Update()
    {
        if(bomberEnemy.currentEnemyHP <= 0)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberExplodeState(bomberEnemy));
        }
        moveDir = Vector2.MoveTowards(bomberEnemy.transform.position, bomberEnemy.destination.position, bomberEnemy.walkSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(bomberEnemy.transform.position, bomberEnemy.destination.position) < 0.5f)
        {
            Debug.Log("Reached Destination");
            bomberEnemy.gameObject.SetActive(false);
        }
    }
    public override void FixedUpdate()
    {
        bomberEnemy.enemyRB.MovePosition(new Vector2(moveDir.x, bomberEnemy.transform.position.y));
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {

    }
    public override void OnColliderExit(Collision2D pCollider)
    {

    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
        if(eCollider.tag == "Player")
        {
            bomberEnemy.currentEnemyHP = 0;   
            bomberEnemy.player.NotifyPlayerObserver(PlayerAction.Blind);
        }
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {

    }
    public override void Exit()
    {

    }
}
