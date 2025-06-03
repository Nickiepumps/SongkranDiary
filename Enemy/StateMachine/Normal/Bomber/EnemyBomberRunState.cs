using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberRunState : EnemyStateMachine
{
    public EnemyBomberRunState(EnemyBomberStateController bomberEnemy) : base(bomberEnemy) { }
    private Vector2 moveDir;
    public override void Start()
    {
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
            bomberEnemy.gameObject.SetActive(false);
        }
    }
    public override void FixedUpdate()
    {
        bomberEnemy.transform.position = moveDir;
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
        }
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {

    }
    public override void Exit()
    {

    }
}
