using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour, IPlayerObserver
{
    [SerializeField] private PlayerSubject sideScrollPlayerObserver;
    [SerializeField] private PlayerStats playerHealthStat;
    [SerializeField] private Sprite healthIcon;
    [SerializeField] private Image healthUI;
    [SerializeField] private Transform playerHealthGroup;
    [SerializeField] private Image playerEmotionDisplay;
    [SerializeField] private List<Sprite> playerEmotionIconLists;
    private List<Image> healthIconList = new List<Image>();
    private void OnEnable()
    {
        sideScrollPlayerObserver.AddPlayerObserver(this);   
    }
    private void OnDisable()
    {
        sideScrollPlayerObserver.RemovePlayerObserver(this);
    }
    private void Start()
    {
        int healthPoint = playerHealthStat.currentPlayerHP.hpPoint;
        UpdateEmotionIcon(healthPoint, healthPoint);
        for(int i = 0; i < healthPoint; i++)
        {
            Image hp = Instantiate(healthUI, playerHealthGroup);
            healthUI.rectTransform.sizeDelta = new Vector2(48f, 48f);
            healthUI.sprite = healthIcon;
            healthIconList.Add(hp);
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Damaged):
                UpdateEmotionIcon(sideScrollPlayerObserver.GetComponent<PlayerSideScrollStateController>().playerMaxHP,
                    sideScrollPlayerObserver.GetComponent<PlayerSideScrollStateController>().playerCurrentHP);
                return;
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
    public void UpdateEmotionIcon(float maxHP, float currentHP)
    {
        float healthPercentage = (currentHP / maxHP) * 100;
        healthPercentage = Mathf.RoundToInt(healthPercentage); // Round the decimal to the nearest int value
        if (healthPercentage <= 34)
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[2]; // Angry
        }
        else if(healthPercentage <= 67)
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[1]; // Nervous
        }
        else
        {
            playerEmotionDisplay.sprite = playerEmotionIconLists[0]; // Happy
        }
    }
}
