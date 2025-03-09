using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStats playerHealthStat;
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Image healthUI;
    [SerializeField] private Transform playerHealthGroup;
    private List<Image> healthIconList = new List<Image>();
    private void Start()
    {
        int healthPoint = playerHealthStat.currentPlayerHP.hpPoint;
        for(int i = 0; i < healthPoint; i++)
        {
            Image hp = Instantiate(healthUI, playerHealthGroup);
            healthUI.rectTransform.sizeDelta = new Vector2(48f, 48f);
            healthUI.sprite = healthIcon;
            healthIconList.Add(hp);
        }
    }
    public void IncreaseHealth(int currentHP)
    {
        healthIconList[currentHP - 1].gameObject.SetActive(true);
    }
    public void DecreaseHealth(int currentHP)
    {
        healthIconList[currentHP - 1].gameObject.SetActive(false);
    }
}
