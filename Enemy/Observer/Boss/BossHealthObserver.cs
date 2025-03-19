using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthObserver : MonoBehaviour, IBossObserver
{
    private BossHealth bossHP;
    private void Awake()
    {
        bossHP = GetComponent<BossHealth>();
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Damaged):
                Debug.Log("Boss Damaged");
                bossHP.currentBossHP--;
                return;
        }
    }
}
