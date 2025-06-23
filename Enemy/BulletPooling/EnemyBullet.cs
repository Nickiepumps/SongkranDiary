using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float travelSpeed;
    public Vector2 bulletDirection;
    [SerializeField] private Collider2D damageCollider;
    [SerializeField] private Collider2D healCollider;
    public bool isHealBullet = false;
    public bool isIncomingBullet = false;
    private Rigidbody2D enemyBulletRB;

    [Header("Bullet Sprites")]
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private Sprite healSprite;

    [Header("Bullet Animator")]
    public Animator bulletAnimator;
    private void OnEnable()
    {
        if(isHealBullet == false)
        {
            damageCollider.enabled = true;
            healCollider.enabled = false;
            bulletAnimator.SetBool("isHit", false);
            bulletAnimator.SetBool("isHeal", false);
        }
        else
        {
            damageCollider.enabled = false;
            healCollider.enabled = true;
            bulletAnimator.SetBool("isHit", false);
            bulletAnimator.SetBool("isHeal", true);
        }
    }
    private void OnDisable()
    {
        isHealBullet = false;
        isIncomingBullet = false;
        damageCollider.enabled = true;
        healCollider.enabled = false;
        bulletAnimator.SetBool("isHit", false);   
        bulletAnimator.SetBool("isHeal", false);   
    }
    private void Start()
    {
        enemyBulletRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 bulletPosition = transform.position;
        bulletPosition += bulletDirection * travelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
        Vector2 camToViewportPoint = Camera.main.WorldToViewportPoint(bulletPosition);
        if(isIncomingBullet == false)
        {
            if (camToViewportPoint.x <= 0 || camToViewportPoint.x >= 1.1f || camToViewportPoint.y <= 0 || camToViewportPoint.y >= 1.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "B_Boundary" && isIncomingBullet == true)
        {
            gameObject.SetActive(false);
        }
        if(collision.tag == "Player" || collision.tag == "PlayerBullet")
        {
            StartCoroutine(DeactivateBullet()); // Play bullet splash anim
        }
    }
    // Play splash animation and deactivate bullet
    private IEnumerator DeactivateBullet()
    {
        if(bulletAnimator.GetBool("isHeal") == true)
        {
            bulletAnimator.SetBool("isHit", true);
            bulletAnimator.SetBool("isHeal", true);
        }
        else
        {
            bulletAnimator.SetBool("isHit", true);
            bulletAnimator.SetBool("isHeal", false);
        }
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}
