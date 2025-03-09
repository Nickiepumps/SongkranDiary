using UnityEngine;

public class EnemyIdleState : EnemyStateMachine
{
    public EnemyIdleState(EnemyStateController enemy) : base(enemy) { }
    private float currentASPD;
    public override void Start()
    {
        Debug.Log("Enemy Idle State");
        currentASPD = enemy.enemyASPD;
    }
    public override void Update()
    {
        Vector2 enemyFacingDirection = enemy.transform.position - enemy.player.transform.position;
        enemy.distanceFromPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);
        Debug.DrawLine(enemy.transform.position, enemy.player.transform.position, Color.red);
        if(enemy.distanceFromPlayer > 3)
        {
            enemy.EnemyStateTransition(new EnemyRunState(enemy));
        }
        if (enemyFacingDirection.x < 0)
        {
            enemy.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            enemy.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        currentASPD -= Time.deltaTime;
        if(currentASPD <= 0)
        {
            enemy.NotifyNormalEnemy(EnemyAction.Shoot);
            currentASPD = enemy.enemyASPD;
        }
    }
    public override void FixedUpdate()
    {

    }
    public override void Exit()
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
}
