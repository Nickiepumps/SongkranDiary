using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Movable : Platform
{
    [Header("Movable Platform Properties")]
    [SerializeField] private float platformSpeed = 1f;
    [SerializeField] private Transform[] platformMovePath;
    private Rigidbody2D platformRB;
    private int platformDestination = 1;
    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 direction = Vector2.MoveTowards(transform.position, platformMovePath[platformDestination].position, platformSpeed * Time.fixedDeltaTime);
        platformRB.MovePosition(direction);
        if(transform.position == platformMovePath[1].position)
        {
            platformDestination = 0;
        }
        else if(transform.position == platformMovePath[0].position)
        {
            platformDestination = 1;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // Check each platform's contact point and play the animation when the player stepped on top of the platform
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                // -Y value is the top side of the platform
                if (contactPoint.normal.y < -0.5f)
                {
                    platformAnim.Play();
                    break;
                }
            }
        }
    }
}
