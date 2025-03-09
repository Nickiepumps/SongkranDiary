using UnityEngine;

public abstract class BossStateMachine
{
    protected BossStateController boss;
    public BossStateMachine(BossStateController boss)
    {
        this.boss = boss;
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
