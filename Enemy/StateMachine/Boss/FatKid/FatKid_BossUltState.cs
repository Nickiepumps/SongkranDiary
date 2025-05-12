using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FatKid_BossUltState : BossStateMachine
{
    public FatKid_BossUltState(FatKid_BossStateController fatKidBoss) : base(fatKidBoss) { }
    private float prepareTime = 1f;
    private float currentTime;
    private Transform rightPos;
    private Vector2 moveDir;
    float moveSpeed;
    public override void Start()
    {
        Debug.Log("Boss Ult State");
        // Change animation from idle to ult
        fatKidBoss.bossSpriteRenderer.sprite = fatKidBoss.bossScriptableObject.ultimateSprite;
        fatKidBoss.normalCollider.enabled = false;
        fatKidBoss.ultCollider.enabled = true;
        fatKidBoss.normalHitBox.enabled = false;
        fatKidBoss.ultHitBox.enabled = true;
        currentTime = prepareTime;

        // Bounce back to original position
        rightPos = fatKidBoss.destination;
        moveSpeed = fatKidBoss.bossScriptableObject.bossUltMovementSpeed;
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (moveDir.x == rightPos.position.x)
            {
                fatKidBoss.destination = fatKidBoss.originalPos;
                moveSpeed = 15;
                fatKidBoss.isJump = true;
                if (fatKidBoss.isJump)
                {
                    fatKidBoss.NotifyBoss(BossAction.Jump);
                    fatKidBoss.isJump = false;
                }
            }
            if(moveDir.x == fatKidBoss.destination.position.x)
            {
                fatKidBoss.destination = rightPos;
                fatKidBoss.BossStateTransition(new FatKid_BossIdleState(fatKidBoss));
            }
            moveDir = Vector2.MoveTowards(fatKidBoss.transform.position, fatKidBoss.destination.position, moveSpeed * Time.fixedDeltaTime);
        }
    }
    public override void FixedUpdate()
    {
        // Move from left to right
        if(currentTime <= 0 && fatKidBoss.bossScriptableObject.bossName == BossList.FatKid)
        {
            //boss.transform.position = new Vector2(moveDir.x, pos.y);
            fatKidBoss.transform.position = moveDir;
        }

        // Transition back to Idle State
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
    private void NextDestination(Transform nextDestination)
    {

    }
}
