using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketKid_BossIdleState : BossStateMachine
{
    public BucketKid_BossIdleState(BucketKid_BossStateController bucketKidBoss) : base(bucketKidBoss) { }
    public override void Start()
    {

    }
    public override void Update()
    {
        /*if(bucketKidBoss.isGameStart == true)
        {
            if (bucketKidBoss.bossHP.currentBossHP <= 0)
            {
                bucketKidBoss.BossStateTransition(new BucketKid_BossDieState(bucketKidBoss));
            }
        } Uncomment this when using BossHealth script*/
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
