using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatKid_BossIdleState : BossStateMachine
{
    public FatKid_BossIdleState(FatKid_BossStateController fatKidBoss) : base(fatKidBoss) { }
    private float currentIdleTime;
    private float currentUltTime;
    public override void Start()
    {
        Debug.Log("Boss Idle");
        fatKidBoss.bossSpriteRenderer.sprite = fatKidBoss.bossScriptableObject.idleSprite;
        currentUltTime = fatKidBoss.bossScriptableObject.ultCooldown;
        fatKidBoss.normalCollider.enabled = true;
        fatKidBoss.ultCollider.enabled = false;
        fatKidBoss.normalHitBox.enabled = true;
        fatKidBoss.ultHitBox.enabled = false;
        fatKidBoss.isJump = false;

        if (fatKidBoss.startInitIdle == false)
        {
            currentIdleTime = fatKidBoss.bossScriptableObject.initialIdleTime;
            fatKidBoss.startInitIdle = true;
        }
        else
        {
            currentIdleTime = fatKidBoss.bossScriptableObject.idleTime;
        }
        
    }
    public override void Update()
    {
        if(fatKidBoss.bossShooting == false && fatKidBoss.bossUlt == false)
        {
            currentIdleTime -= Time.deltaTime;
            currentUltTime -= Time.deltaTime;
            if (currentIdleTime <= 0)
            {
                fatKidBoss.NotifyBoss(BossAction.Shoot);
                currentIdleTime = fatKidBoss.bossScriptableObject.idleTime;
            }
            if(currentUltTime <= 0)
            {
                fatKidBoss.NotifyBoss(BossAction.Ult);
                currentUltTime = fatKidBoss.bossScriptableObject.ultCooldown;
                fatKidBoss.BossStateTransition(new FatKid_BossUltState(fatKidBoss));
            }
            if(fatKidBoss.bossHP.currentBossHP <= 0)
            {
                fatKidBoss.BossStateTransition(new FatKid_BossDieState(fatKidBoss));
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
