using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyStateMachine
{
    public EnemyRunState(EnemyStateController enemy) : base(enemy) { }
    private Vector2 moveDir;
    private float currentASPD;
    private Transform newStart, newDestination;
    public override void Start()
    {
        enemy.currentEnemyHP = enemy.enemyHP;
        currentASPD = enemy.enemyASPD;
        enemy.transform.position = new Vector2(enemy.startPoint.position.x, enemy.transform.position.y);
    }
    public override void Update()
    {
        if(enemy.currentEnemyHP <= 0)
        {
            enemy.EnemyStateTransition(new EnemyDeadState(enemy));
        }
        if(enemy.normalEnemyType == NormalEnemyType.Shooter)
        {
            moveDir = Vector2.MoveTowards(enemy.transform.position, enemy.destination.position, enemy.walkSpeed * Time.fixedDeltaTime);

            currentASPD -= Time.deltaTime;
            if (currentASPD <= 0)
            {
                enemy.NotifyNormalEnemy(EnemyAction.Shoot);
                currentASPD = enemy.enemyASPD;
            }
        }
        else if(enemy.normalEnemyType == NormalEnemyType.Bomber)
        {
            moveDir = Vector2.MoveTowards(enemy.transform.position, enemy.destination.position, enemy.walkSpeed * Time.fixedDeltaTime);
        }

        if(Vector2.Distance(enemy.transform.position, enemy.destination.position) < 0.5f)
        {
            Debug.Log("Reached Destination");
            enemy.gameObject.SetActive(false);
        }
    }
    public override void FixedUpdate()
    {
        enemy.enemyRB.MovePosition(new Vector2(moveDir.x, enemy.transform.position.y));
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
        
    }

    public override void OnTriggerExit(Collider2D eCollider)
    {
        
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
        
    }

    public override void OnColliderExit(Collision2D pCollider)
    {
        
    }
    public override void Exit()
    {
        
    }
}
