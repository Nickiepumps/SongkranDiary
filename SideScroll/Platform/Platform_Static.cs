using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Static : Platform
{
    [SerializeField] private bool isStable = true; // False if the platform is not stable
    [SerializeField] private float holdingTime = 3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // Check each platform's contact point and play the animation when the player stepped on top of the platform
            foreach (ContactPoint2D contactPoint in collision.contacts)
            {
                // -Y value is the top side of the platform
                if (contactPoint.normal.y < -0.5f && isStable == true)
                {
                    platformAnim.Play();
                    break;
                }
                else if(contactPoint.normal.y < -0.5f && isStable == false)
                {
                    // Play platform shaking anim
                    StartCoroutine(PlatformFalling());
                }
            }
        }
    }
    private IEnumerator PlatformFalling()
    {
        platformAnim.Play("Platform_Shaking");
        yield return new WaitForSeconds(holdingTime);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().AddForce(Vector2.down);
        platformAnim.Stop();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
