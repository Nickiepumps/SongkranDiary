using UnityEngine;
public class SideScroll_IncomingBulletTrigger : SideScroll_TriggerEvent
{
    [SerializeField] private IncomingBulletController IncomingBulletController;
    [SerializeField] private bool startRainingBullet;
    private bool isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isTriggered == false)
        {
            isTriggered = true;
            Trigger_RainingBullet(IncomingBulletController, startRainingBullet);
        }
    }
}
