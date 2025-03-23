using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float travelSpeed;
    public Vector2 bulletDirection;
    private Rigidbody2D enemyBulletRB;

    [Header("Bullet Animator")]
    public Animator bulletAnimator;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        bulletAnimator.SetBool("isHit", false);   
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "PlayerBullet")
        {
            StartCoroutine(DeactivateBullet()); // Play bullet splash anim
        }
        else if(collision.tag == "B_Boundary")
        {
            gameObject.SetActive(false);
        }
    }
    // Play splash animation and deactivate bullet
    private IEnumerator DeactivateBullet()
    {
        bulletAnimator.SetBool("isHit", true);
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
}
