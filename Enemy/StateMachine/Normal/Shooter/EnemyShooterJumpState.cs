using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterJumpState : EnemyStateMachine
{
    public EnemyShooterJumpState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    private float prepareTime = 1f;
    private float currentTime;
    private bool isJumping = false;
    Vector2 moveDir;
    public override void Start()
    {
        currentTime = prepareTime;
        //shooterEnemy.shooterEnemyAnimator.SetBool("isJump", true); Uncomment this when animation is ready
    }
    public override void Update()
    {
        currentTime -= Time.deltaTime;
        if (shooterEnemy.currentEnemyHP <= 0)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterDeadState(shooterEnemy));
        }
        if (currentTime <= 0 && isJumping == false)
        {
            isJumping = true;
            shooterEnemy.enemyRB.AddForce(Vector2.up * shooterEnemy.jumpForce, ForceMode2D.Impulse);
        }
        if(isJumping == true)
        {
            moveDir = Vector2.MoveTowards(shooterEnemy.transform.position, new Vector2(shooterEnemy.destination.position.x, shooterEnemy.transform.position.y),
                6 * Time.fixedDeltaTime);
        }
    }
    public override void FixedUpdate()
    {
        if(isJumping == true)
        {
            shooterEnemy.transform.position = moveDir;
        }
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {

    }
    public override void OnTriggerExit(Collider2D eCollider)
    {
        if(eCollider.tag == "EnemyJump")
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterRunState(shooterEnemy));
        }
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
