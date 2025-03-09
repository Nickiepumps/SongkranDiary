using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyStateMachine
{
    public EnemyDeadState(EnemyStateController enemy) : base(enemy) { }
    public override void Start()
    {
        enemy.isDead = true;
        enemy.NotifyNormalEnemy(EnemyAction.Dead);
        enemy.gameObject.SetActive(false);
    }
    public override void Update()
    {
        if(enemy.currentEnemyHP > 0)
        {
            enemy.EnemyStateTransition(new EnemyRunState(enemy));
        }
    }
    public override void FixedUpdate()
    {
        
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
