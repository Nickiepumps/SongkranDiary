using UnityEngine;

public abstract class EnemyStateMachine
{
    protected EnemyStateController enemy;
    public EnemyStateMachine(EnemyStateController enemy)
    {
        this.enemy = enemy;
    }
    public abstract void Start();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void OnTriggerEnter(Collider2D eCollider);
    public abstract void OnTriggerExit(Collider2D eCollider);
    public abstract void OnColliderEnter(Collision2D pCollider);
    public abstract void OnColliderExit(Collision2D pCollider);
    public abstract void Exit();
}
