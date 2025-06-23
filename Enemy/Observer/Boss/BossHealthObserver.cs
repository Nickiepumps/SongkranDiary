using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthObserver : MonoBehaviour, IBossObserver
{
    [Header("Boss Observer References")]
    [SerializeField] private BossSubject bossSubject;
    [Header("Boss Health Properties")]
    //[SerializeField] private BossScriptableObject bossStats;
    //public int currentBossHP;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] public int bossMaxHP; // Used for game controller to check boss progression
    [Header("Boss Sprite")]
    [SerializeField] private SpriteRenderer bossSpriteRenderer;
    [SerializeField] private Color bossDamagedColor;
    private void OnEnable()
    {
        bossSubject.AddBossObserver(this);
    }
    private void OnDisable()
    {
        bossSubject.RemoveBossObserver(this);
    }
    private void Start()
    {
        //bossMaxHP = bossHealth.bossMaxHP;
        //currentBossHP = bossStats.HP;
    }
    public void OnBossNotify(BossAction action)
    {
        switch (action)
        {
            case (BossAction.Damaged):
                Debug.Log("Boss Damaged");
                //currentBossHP--;
                bossHealth.currentBossHP--;
                StartCoroutine(DamageIndicator());
                return;
        }
    }
    private IEnumerator DamageIndicator()
    {
        bossSpriteRenderer.color = bossDamagedColor;
        yield return new WaitForSeconds(0.1f);
        bossSpriteRenderer.color = Color.white;
    }
}
