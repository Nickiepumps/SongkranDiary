using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BossDieState : BossStateMachine
{
    public BossDieState(BossStateController boss) : base(boss) { }

    public override void Start()
    {
        boss.isDead = true;
        boss.NotifyBoss(BossAction.Die);
        boss.gameObject.SetActive(false);
    }
    public override void Update()
    {
        
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
