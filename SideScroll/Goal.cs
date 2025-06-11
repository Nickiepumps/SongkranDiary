using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : GameSubject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            NotifySideScrollGameObserver(SideScrollGameState.WinRunNGun);
        }
    }
}
