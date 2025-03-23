using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float travelSpeed;
    public Vector2 bulletDirection;
    private Rigidbody2D enemyBulletRB;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
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
        if(collision.tag == "Player")
        {
            // Play bullet splash anim
            gameObject.SetActive(false);
        }
        else if(collision.tag == "B_Boundary")
        {
            gameObject.SetActive(false);
        }
        else if(collision.tag == "PlayerBullet")
        {
            // Play destroyed bullet anim
            gameObject.SetActive(false);
        }
    }
}
