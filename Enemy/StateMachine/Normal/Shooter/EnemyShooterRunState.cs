using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterRunState : EnemyStateMachine
{
    public EnemyShooterRunState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    private Vector2 moveDir;
    private float currentASPD;
    public override void Start()
    {
        currentASPD = shooterEnemy.enemyASPD;
    }
    public override void Update()
    {
        /*if (shooterEnemy.isOnGround == true)
        {
            shooterEnemy.shooterEnemyAnimator.SetBool("isRun", true);
            shooterEnemy.shooterEnemyAnimator.SetBool("isJump", false);
        }*/ // Uncomment this when animation is ready

        if (shooterEnemy.currentEnemyHP <= 0)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterDeadState(shooterEnemy));
        }
        moveDir = Vector2.MoveTowards(shooterEnemy.transform.position, new Vector2(shooterEnemy.destination.position.x, shooterEnemy.transform.position.y),
            shooterEnemy.walkSpeed * Time.fixedDeltaTime);
        currentASPD -= Time.deltaTime;
        if (currentASPD <= 0)
        {
            shooterEnemy.NotifyNormalEnemy(EnemyAction.Shoot);
            currentASPD = shooterEnemy.enemyASPD;
        }
        if (Vector2.Distance(shooterEnemy.transform.position, shooterEnemy.destination.position) < 0.5f)
        {
            Debug.Log("Reached Destination");
            shooterEnemy.gameObject.SetActive(false);
        }
    }
    public override void FixedUpdate()
    {
        shooterEnemy.transform.position = moveDir;
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
        if(eCollider.tag == "E_Boundary")
        {
            shooterEnemy.gameObject.SetActive(false);
        }
        if(eCollider.tag == "EnemyJump" && shooterEnemy.isOnGround == true)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterJumpState(shooterEnemy));
        }
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
