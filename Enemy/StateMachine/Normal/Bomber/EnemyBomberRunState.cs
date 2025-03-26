using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberRunState : EnemyStateMachine
{
    public EnemyBomberRunState(EnemyBomberStateController bomberEnemy) : base(bomberEnemy) { }
    private Vector2 moveDir;
    public override void Start()
    {
        bomberEnemy.currentEnemyHP = bomberEnemy.enemyHP;
        bomberEnemy.transform.position = new Vector2(bomberEnemy.startPoint.position.x, bomberEnemy.transform.position.y);
    }
    public override void Update()
    {
        if(bomberEnemy.currentEnemyHP <= 0)
        {
            bomberEnemy.EnemyStateTransition(new EnemyBomberExplodeState(bomberEnemy));
        }
        moveDir = Vector2.MoveTowards(bomberEnemy.transform.position, bomberEnemy.destination.position, bomberEnemy.walkSpeed * Time.fixedDeltaTime);
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

    }
    public override void OnTriggerExit(Collider2D eCollider)
    {

    }
    public override void Exit()
    {

    }
}
