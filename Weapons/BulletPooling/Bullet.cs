using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletType bulletType; // Bullet type
    public float travelSpeed;
    public SpriteRenderer bulletSprite; // Bullet's sprite
    public Vector2 bulletDirection;
    public Transform laserBoundary;
    private BulletPooler bulletPooler;
    private BoxCollider2D bulletCollider;

    private void OnEnable()
    {
        if(bulletCollider == null && bulletPooler == null)
        {
            bulletCollider = GetComponent<BoxCollider2D>();
            bulletPooler = GameObject.Find("BulletPooler").GetComponent<BulletPooler>();
        }
        if (bulletType == BulletType.laser)
        {
            StartCoroutine(LaserBullet());
        }
    }
    private void OnDisable()
    {
        if(bulletPooler == null)
        {
            bulletPooler = GameObject.Find("BulletPooler").GetComponent<BulletPooler>();
        }
        bulletSprite.sprite = bulletPooler.currentBulletSprite; // Set the bullet sprite to match the current bullet type 
        bulletType = bulletPooler.currentBulletType; // Set the bullet behavior to match the current bullet type
        //transform.localScale = bulletPooler.currentLocalTransform; // Reset bullet localScale (Laser to other bullet type)
    }
    private void Start()
    {
        bulletCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(bulletType == BulletType.normalBullet)
        {
            NormalBullet();
        }
        else if(bulletType == BulletType.spreadBullet)
        {
            SpreadBullet();
        }
    }
    private void NormalBullet()
    {
        bulletCollider.enabled = true;
        Vector2 bulletPosition = transform.position;
        bulletPosition += bulletDirection * travelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
    }
    private void SpreadBullet()
    {
        bulletCollider.enabled = true;
        Vector2 bulletPosition = transform.position;
        bulletPosition += bulletDirection * travelSpeed * Time.deltaTime;
        transform.position = bulletPosition;
    }
    private IEnumerator LaserBullet()
    {
        bulletCollider.enabled = false; // Disable bullet collider
        float laserDist = Vector2.Distance(transform.position, laserBoundary.position); // Check distance from laser boundary to bullet spawn
        RaycastHit2D[] laserHit = Physics2D.RaycastAll(transform.position, laserBoundary.position);
        bulletSprite.size = new Vector2(laserDist, 1); // Scale the object in local space to match the laser distance
        foreach(RaycastHit2D raycastHit in laserHit)
        {
            if (raycastHit == true && raycastHit.collider.gameObject.tag == "Enemy")
            {
                if(raycastHit.transform.GetComponent<NormalEnemyObserverController>() != null)
                {
                    raycastHit.transform.GetComponent<NormalEnemyObserverController>().OnNormalEnemyNotify(EnemyAction.Damaged);
                }
                else
                {
                    raycastHit.transform.GetComponent<BossHealthObserver>().OnBossNotify(BossAction.Damaged);
                }
                
            }
        }
        Vector2 bulletPosition = transform.position;
        Debug.DrawLine(transform.position, laserBoundary.position, Color.red, 10);
        yield return new WaitForSeconds(0.1f); // Appear only for 0.1 secs
        laserHit = null; // Clear all the hit object
        gameObject.SetActive(false); // Disable bullet
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "B_Boundary")
        {
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<NormalEnemyObserverController>() != null)
            {
                collision.gameObject.GetComponent<NormalEnemyObserverController>().OnNormalEnemyNotify(EnemyAction.Damaged);
            }
            else
            {
                collision.gameObject.GetComponent<BossHealthObserver>().OnBossNotify(BossAction.Damaged);
            }
            // Play destroyed bullet anim
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "EnemyBullet")
        {
            // Play destroyed bullet anim
            gameObject.SetActive(false);
        }
    }
}
