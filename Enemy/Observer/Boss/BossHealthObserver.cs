using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthObserver : MonoBehaviour, IBossObserver
{
    private BossHealth bossHP;
    [Header("Boss Sprite")]
    [SerializeField] private SpriteRenderer bossSpriteRenderer;
    [SerializeField] private Color bossDamagedColor;
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
