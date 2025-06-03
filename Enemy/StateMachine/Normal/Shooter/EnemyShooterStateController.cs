using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterStateController : NormalEnemySubject
{
    [Header("Player Reference")]
    public PlayerSideScrollStateController player;

    [Header("Enemy State Machine")]
    private EnemyStateMachine enemyCurrentState;

    [Header("Enemy General Properties")]
    [SerializeField] private NormalEnemySO enemyStats;
    public Rigidbody2D enemyRB;
    public NormalEnemyType normalEnemyType;
    public int currentEnemyHP;
    public float walkSpeed;
    public float jumpForce;
    public float enemyASPD;
    public int damage;
    public SpriteRenderer enemySpriteRenderer;
    public Transform destination;
    public Transform startPoint;
    public float distanceFromPlayer;

    [Header("Animator")]
    //public Animator shooterEnemyAnimator;

    // Hide from inspector
    public bool isDead = false;
    public bool isOnGround = true;
    private void OnEnable()
    {
        enemySpriteRenderer.sprite = enemyStats.normalSprite;
        normalEnemyType = enemyStats.NormalEnemyType;
        currentEnemyHP = enemyStats.hp;
        walkSpeed = enemyStats.movementSpeed;
        enemyASPD = enemyStats.aspd;
        damage = enemyStats.damage;
    }
    private void Start()
    {
        player = GameObject.Find("Player_SideScroll").GetComponent<PlayerSideScrollStateController>();
        EnemyStateTransition(new EnemyShooterRunState(this));
    }
    private void Update()
    {
        enemyCurrentState.Update();
    }
    private void FixedUpdate()
    {
        enemyCurrentState.FixedUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("PlayerBullet"):
                NotifyNormalEnemy(EnemyAction.Damaged);
                break;
        }
        enemyCurrentState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyCurrentState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Side_Floor")
        {
            isOnGround = true;
        }
        enemyCurrentState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Side_Floor")
        {
            isOnGround = false;
        }
        enemyCurrentState.OnColliderExit(collision);
    }
    public void EnemyStateTransition(EnemyStateMachine newEnemyState)
    {
        enemyCurrentState = newEnemyState;
        enemyCurrentState.Start();
    }
}
