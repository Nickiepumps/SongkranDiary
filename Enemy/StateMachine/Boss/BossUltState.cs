using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossUltState : BossStateMachine
{
    public BossUltState(BossStateController boss) : base(boss) { }
    private float prepareTime = 1f;
    private float currentTime;
    //private Transform startPos;
    private Transform rightPos;
    private Vector2 moveDir;
    //float verticalShift;
    //float period;
    float moveSpeed;
    public override void Start()
    {
        Debug.Log("Boss Ult State");
        // Change animation from idle to ult
        boss.bossSpriteRenderer.sprite = boss.bossScriptableObject.ultimateSprite;
        boss.normalCollider.enabled = false;
        boss.ultCollider.enabled = true;
        boss.normalHitBox.enabled = false;
        boss.ultHitBox.enabled = true;
        currentTime = prepareTime;

        // Bounce back to original position
        rightPos = boss.destination;
        moveSpeed = boss.bossScriptableObject.bossUltMovementSpeed;
        //verticalShift = boss.transform.position.y;
        //period = (Mathf.PI) / Vector2.Distance(boss.originalPos.position, boss.destination.position);
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && boss.bossScriptableObject.bossType == BossType.FatKid)
        {
            if (moveDir.x == rightPos.position.x)
            {
                boss.destination = boss.originalPos;
                moveSpeed = 15;
                boss.isJump = true;
                if (boss.isJump)
                {
                    boss.NotifyBoss(BossAction.Jump);
                    boss.isJump = false;
                }
            }
            if(moveDir.x == boss.destination.position.x)
            {
                boss.destination = rightPos;
                boss.BossStateTransition(new BossIdleState(boss));
            }
            moveDir = Vector2.MoveTowards(boss.transform.position, boss.destination.position, moveSpeed * Time.fixedDeltaTime);
        }
    }
    public override void FixedUpdate()
    {
        // Move from left to right
        /*Vector2 pos = boss.transform.position;
        float sin = -3 * Mathf.Sin(0.25f * pos.x);
        pos.y = -sin + verticalShift;*/
        if(currentTime <= 0 && boss.bossScriptableObject.bossType == BossType.FatKid)
        {
            //boss.transform.position = new Vector2(moveDir.x, pos.y);
            boss.transform.position = moveDir;
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
