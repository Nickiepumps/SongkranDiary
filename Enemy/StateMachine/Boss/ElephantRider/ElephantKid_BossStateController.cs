using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ElephantKid_BossStateController : BossSubject
{
    private BossStateMachine currentBossState;
    [Header("Boss Scriptable Object")]
    public BossScriptableObject bossScriptableObject;

    [Header("Boss Animator")]
    public Animator bossAnimator;

    [Header("Boss Properties")]
    public BossHealth bossHP;
    public SpriteRenderer bossSpriteRenderer;
    public Rigidbody2D bossRB;
    public BoxCollider2D normalCollider;
    public BoxCollider2D normalHitBox;

    // Hide in inspector
    public bool isDead = false;
    public bool isBossInvulnerable = false;
    public bool isKidAttack = false;
    public bool isElephantAttack = false;
    public bool isGameStart = false;
    public int normalAttackPattern = 0;
    public int healCount = 0;
    public bool bossUlt1 = false;
    public bool bossUlt2 = false;
    private void Start()
    {
        BossStateTransition(new ElephantKid_BossIdleState(this));
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
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                if (isDead == false && isBossInvulnerable == false)
                {
                    NotifyBoss(BossAction.Damaged);
                }
                return;
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
    public void BossStateTransition(BossStateMachine newState)
    {
        currentBossState = newState;
        currentBossState.Start();
    }
    public IEnumerator StartBossHealAnimation()
    {
        // Play boss heal anim
        yield return new WaitForSeconds(0.5f);
        NotifyBoss(BossAction.Heal);
    }
}
