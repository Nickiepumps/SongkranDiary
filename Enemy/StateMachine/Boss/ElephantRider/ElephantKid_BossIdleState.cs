using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElephantKid_BossIdleState : BossStateMachine
{
    public ElephantKid_BossIdleState(ElephantKid_BossStateController elephantKidBoss) : base (elephantKidBoss){}
    private float currentIdleTime;
    public override void Start()
    {
        // Play Elephant kid idle anim
        currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
    }
    public override void Update()
    {
        currentIdleTime -= Time.deltaTime;
        if (currentIdleTime <= 0)
        {
            //elephantKidBoss.BossStateTransition(new ElephantKid_NormalAttack1State(elephantKidBoss));
            currentIdleTime = elephantKidBoss.bossScriptableObject.idleTime;
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
