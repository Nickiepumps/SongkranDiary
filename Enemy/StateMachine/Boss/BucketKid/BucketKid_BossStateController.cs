using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BucketKid_BossObserverController))]
public class BucketKid_BossStateController : BossSubject
{
    private BossStateMachine currentBossState;
    [Header("Boss Properties")]
    public BossHealthObserver bossHP;
    public SpriteRenderer bossSpriteRenderer;
    public Rigidbody2D bossRB;
    public CircleCollider2D ultCollider;
    public BoxCollider2D normalCollider;
    public CircleCollider2D ultHitBox;
    public BoxCollider2D normalHitBox;

    // Hide in inspector
    public bool isDead = false;
    private void Start()
    {
        BossStateTransition(new BucketKid_BossIdleState(this));
    }
    private void Update()
    {
        currentBossState.Update();
    }
    private void FixedUpdate()
    {
        currentBossState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            NotifyBoss(BossAction.Damaged);
        }
        currentBossState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentBossState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentBossState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        currentBossState.OnColliderExit(collision);
    }
    public void BossStateTransition(BossStateMachine newBossState)
    {
        currentBossState = newBossState;
        currentBossState.Start();
    }
}
