using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossHealthObserver))]
public class BossHealth : MonoBehaviour
{
    [SerializeField] private BossScriptableObject bossStats;
    [HideInInspector] public int bossMaxHP;
    public int currentBossHP;
    void Start()
    {
        bossMaxHP = bossStats.HP;
        currentBossHP = bossMaxHP;
    }
}
