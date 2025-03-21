using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUltimateController : MonoBehaviour
{
    private bool ultReady = false;
    private PlayerStats playerStats;
    private int ultAmount;
    private float ultChargeTime;
    [SerializeField] private Transform spawnDirection_Right;
    private float currentTime;
    private int currentUltReady = 0;

    [Header("Player HUD")]
    [SerializeField] private Image ultChargeIcon;
    [SerializeField] private TMP_Text ultAmountText;
    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        ultChargeTime = playerStats.maxPlayerUltCharge.ultChargeTime;
        currentTime = ultChargeTime;
        ultAmount = playerStats.maxPlayerUltAmount.ultCount;
    }
    private void Update()
    {
        if (ultReady == false || currentUltReady < 3)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && currentUltReady < 3)
            {
                currentUltReady++;
                currentTime = ultChargeTime;
                ultReady = true;
                Debug.Log("Ult Ready " + currentUltReady);
            }
            if (currentUltReady == 3)
            {
                currentUltReady = Mathf.Clamp(currentUltReady, 0, 3);
                ultAmountText.text = currentUltReady.ToString();
                Debug.Log("Ult Max");
            }
        }
        if(currentUltReady <= 0)
        {
            ultReady = false;
        }

        if (Input.GetMouseButtonDown(2) && ultReady == true)
        {
            currentUltReady--;
            Debug.Log(currentUltReady + " Ult Left");
            ThrowProjectile();
        }
        UpdateUltDisplay();
    }
    private void ThrowProjectile()
    {
        spawnDirection_Right.gameObject.GetComponent<PlayerGun>().ShootUltimate();
    }
    private void UpdateUltDisplay()
    {
        if(currentUltReady < 3)
        {
            ultChargeIcon.fillAmount = 1 - (currentTime / ultChargeTime);
        }
        ultAmountText.text = currentUltReady.ToString();
    }
}
