using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterRunState : EnemyStateMachine
{
    public EnemyShooterRunState(EnemyShooterStateController shooterEnemy) : base(shooterEnemy) { }
    private Vector2 moveDir;
    private float currentASPD;
    private Transform newStart, newDestination;
    public override void Start()
    {
        shooterEnemy.currentEnemyHP = shooterEnemy.enemyHP;
        currentASPD = shooterEnemy.enemyASPD;
        shooterEnemy.transform.position = new Vector2(shooterEnemy.startPoint.position.x, shooterEnemy.transform.position.y);
    }
    public override void Update()
    {
        if(shooterEnemy.currentEnemyHP <= 0)
        {
            shooterEnemy.EnemyStateTransition(new EnemyShooterDeadState(shooterEnemy));
        }
        if(shooterEnemy.normalEnemyType == NormalEnemyType.Shooter)
        {
            moveDir = Vector2.MoveTowards(shooterEnemy.transform.position, shooterEnemy.destination.position, shooterEnemy.walkSpeed * Time.fixedDeltaTime);

            currentASPD -= Time.deltaTime;
            if (currentASPD <= 0)
            {
                shooterEnemy.NotifyNormalEnemy(EnemyAction.Shoot);
                currentASPD = shooterEnemy.enemyASPD;
            }
        }
        else if(shooterEnemy.normalEnemyType == NormalEnemyType.Bomber)
        {
            moveDir = Vector2.MoveTowards(shooterEnemy.transform.position, shooterEnemy.destination.position, shooterEnemy.walkSpeed * Time.fixedDeltaTime);
        }

        if(Vector2.Distance(shooterEnemy.transform.position, shooterEnemy.destination.position) < 0.5f)
        {
            Debug.Log("Reached Destination");
            shooterEnemy.gameObject.SetActive(false);
        }
    }
    public override void FixedUpdate()
    {
        shooterEnemy.enemyRB.MovePosition(new Vector2(moveDir.x, shooterEnemy.transform.position.y));
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
