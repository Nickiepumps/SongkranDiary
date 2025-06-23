using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneStateController : NormalEnemySubject
{
    [Header("Player Reference")]
    public PlayerSideScrollStateController player;

    [Header("Enemy State Machine")]
    private EnemyStateMachine enemyCurrentState;

    [Header("Enemy General Properties")]
    [SerializeField] private NormalEnemySO enemyStats;
    public Rigidbody2D enemyRB;
    public NormalEnemyType normalEnemyType;
    public Animator droneEnemyAnimator;
    public int currentEnemyHP;
    public float flySpeed;
    public int damage;
    public SpriteRenderer enemySpriteRenderer;
    public Transform startPoint;
    public float distanceFromPlayer;

    [Header("Drone Bomb Properties")]
    public GameObject droneBombGameObject;

    // Hide from inspector
    private Camera cam;
    public bool isDead = false;
    public int enemyHP;
    public bool isBombDropped = false;
    private void OnEnable()
    {
        if(startPoint != null)
        {
            transform.position = startPoint.position;
        }
        isBombDropped = false;
        currentEnemyHP = enemyStats.hp;
        droneBombGameObject.SetActive(true);
        EnemyStateTransition(new EnemyDroneFlyState(this));
    }
    private void Start()
    {
        cam = Camera.main;
        normalEnemyType = enemyStats.NormalEnemyType;
        flySpeed = enemyStats.movementSpeed;
        damage = enemyStats.damage;
        enemySpriteRenderer.sprite = enemyStats.normalSprite;
        player = GameObject.Find("Player_SideScroll").GetComponent<PlayerSideScrollStateController>();
    }
    private void Update()
    {
        enemyCurrentState.Update();
        Vector2 worldToViewportPoint = cam.WorldToViewportPoint(transform.position);
        if(worldToViewportPoint.x > 1.2f || worldToViewportPoint.x < -0.2f)
        {
            gameObject.SetActive(false);
        }
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
                return;
        }
        enemyCurrentState.OnTriggerEnter(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyCurrentState.OnTriggerExit(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyCurrentState.OnColliderEnter(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        enemyCurrentState.OnColliderExit(collision);
    }
    public void EnemyStateTransition(EnemyStateMachine newEnemyState)
    {
        enemyCurrentState = newEnemyState;
        enemyCurrentState.Start();
    }
}
