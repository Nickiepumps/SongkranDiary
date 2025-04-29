using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneFlyState : EnemyStateMachine
{
    public EnemyDroneFlyState(EnemyDroneStateController droneEnemy) : base(droneEnemy) { }
    private Vector2 flyDir;
    public override void Start()
    {
        droneEnemy.droneEnemyAnimator.SetBool("isFly", true);
        droneEnemy.droneEnemyAnimator.SetBool("isExplode", false);
    }
    public override void Update()
    {
        flyDir = Vector2.MoveTowards(droneEnemy.transform.position, droneEnemy.destination.position, droneEnemy.flySpeed * Time.fixedDeltaTime);
        RaycastHit2D hit = Physics2D.Raycast(droneEnemy.transform.position, Vector2.down, 10f, LayerMask.GetMask("Player"));
        if(hit == true && droneEnemy.isBombDropped == false)
        {
            droneEnemy.EnemyStateTransition(new EnemyDroneDropBombState(droneEnemy));
        }
        if (Vector2.Distance(droneEnemy.transform.position, droneEnemy.destination.position) < 0.5f)
        {
            droneEnemy.isBombDropped = false;
            droneEnemy.gameObject.SetActive(false);
        }
        if(droneEnemy.currentEnemyHP <= 0)
        {
            droneEnemy.EnemyStateTransition(new EnemyDroneExplodetate(droneEnemy));
        }
    }
    public override void FixedUpdate()
    {
        droneEnemy.enemyRB.MovePosition(flyDir);
    }
    public override void OnColliderEnter(Collision2D pCollider)
    {
    }
    public override void OnColliderExit(Collision2D pCollider)
    {
    }
    public override void OnTriggerEnter(Collider2D eCollider)
    {
    }
    public override void OnTriggerExit(Collider2D eCollider)
    {
    }
    public override void Exit()
    {
    }
}
