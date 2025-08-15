using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantKid_NormalAttack1State : BossStateMachine
{
    public ElephantKid_NormalAttack1State(ElephantKid_BossStateController elephantKidBoss) : base(elephantKidBoss) { }
    private float currentAttackTime;
    private int attackCount = 0;
    public override void Start()
    {
        // Play Idle anim when attack timing > 0
        currentAttackTime = elephantKidBoss.bossScriptableObject.aspd;
    }
    public override void Update()
    {
        currentAttackTime -= Time.deltaTime;
        if (currentAttackTime <= 0 && attackCount < 3)
        {
            elephantKidBoss.NotifyBoss(BossAction.Shoot);
            currentAttackTime = 0.3f;
            attackCount++;
        }
        else if(attackCount >= 3)
        {
            elephantKidBoss.BossStateTransition(new ElephantKid_BossIdleState(elephantKidBoss));
        }
        if (elephantKidBoss.bossHP.currentBossHP <= elephantKidBoss.bossHP.bossMaxHP - ((elephantKidBoss.bossHP.bossMaxHP * 25) / 100) * elephantKidBoss.healCount
            && elephantKidBoss.healCount < 4)
        {
            elephantKidBoss.healCount++;
            Debug.Log("Boss has been damaged 25%");
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
