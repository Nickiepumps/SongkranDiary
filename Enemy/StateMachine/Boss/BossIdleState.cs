using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossStateMachine
{
    public BossIdleState(BossStateController boss) : base(boss) { }
    private float currentIdleTime;
    private float currentUltTime;
    public override void Start()
    {
        Debug.Log("Boss Idle");
        boss.bossSpriteRenderer.sprite = boss.bossScriptableObject.idleSprite;
        currentUltTime = boss.bossScriptableObject.ultCooldown;
        boss.normalCollider.enabled = true;
        boss.ultCollider.enabled = false;
        boss.normalHitBox.enabled = true;
        boss.ultHitBox.enabled = false;
        boss.isJump = false;

        if (boss.startInitIdle == false)
        {
            currentIdleTime = boss.bossScriptableObject.initialIdleTime;
            boss.startInitIdle = true;
        }
        else
        {
            currentIdleTime = boss.bossScriptableObject.idleTime;
        }
        
    }
    public override void Update()
    {
        if(boss.bossShooting == false && boss.bossUlt == false)
        {
            currentIdleTime -= Time.deltaTime;
            currentUltTime -= Time.deltaTime;
            if (currentIdleTime <= 0)
            {
                boss.NotifyBoss(BossAction.Shoot);
                currentIdleTime = boss.bossScriptableObject.idleTime;
            }
            if(currentUltTime <= 0)
            {
                boss.NotifyBoss(BossAction.Ult);
                currentUltTime = boss.bossScriptableObject.ultCooldown;
                boss.BossStateTransition(new BossUltState(boss));
            }
            if(boss.currentBossHP <= 0)
            {
                boss.BossStateTransition(new BossDieState(boss));
            }
        }
    }
    public override void FixedUpdate()
    {

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
