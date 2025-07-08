using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    [SerializeField] PlayerStateController isoPlayerStateController;
    [SerializeField] PlayerStats playerStats;

    public static PlayerDataHandler instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData != null)
        {
            isoPlayerStateController.transform.position = playerData.playerISOPos;
            playerStats.currentPlayerHP.level = playerData.hpLevel;
            playerStats.currentPlayerUltCharge.level = playerData.ultChargeLevel;
            playerStats.currentPlayerUltAmount.level = playerData.ultAmountLevel;
            playerStats.currentNormalASPD.level = playerData.bulletNormalASPDLevel;
            playerStats.currentWeaponTravelSpeed.level = playerData.bulletNormalTSPDLevel;
            playerStats.currentSprdBulletASPD.level = playerData.bulletSpreadASPD;
            playerStats.currentWeaponSprdCount.level = playerData.bulletSpreadCountLevel;
            playerStats.currentLsrBulletASPD.level = playerData.bulletLaserASPD;
            playerStats.coinAmount = playerData.Coin;
        }
    }
    public void SavePlayerData()
    {
        if (Directory.Exists(Application.dataPath) == false)
        {
            Directory.CreateDirectory(Application.dataPath);
        }

        PlayerData playerData = new PlayerData();
        playerData.playerISOPos = isoPlayerStateController.transform.position;
        playerData.hpLevel = playerStats.currentPlayerHP.level;
        playerData.ultChargeLevel = playerStats.currentPlayerUltCharge.level;
        playerData.ultAmountLevel = playerStats.currentPlayerUltAmount.level;
        playerData.bulletNormalASPDLevel = playerStats.currentNormalASPD.level;
        playerData.bulletNormalTSPDLevel = playerStats.currentWeaponTravelSpeed.level;
        playerData.bulletSpreadASPD = playerStats.currentSprdBulletASPD.level;
        playerData.bulletSpreadCountLevel = playerStats.currentWeaponSprdCount.level;
        playerData.bulletLaserASPD = playerStats.currentLsrBulletASPD.level;
        playerData.Coin = playerStats.coinAmount;

        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.json", playerDataJson);
    }
    public PlayerData LoadPlayerData()
    {
        if (File.Exists(Application.dataPath + "/playerData.json") == false)
        {
            return null;
        }
        string loadedPlayerDataJson = File.ReadAllText(Application.dataPath + "/playerData.json");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(loadedPlayerDataJson);
        return loadedPlayerData;
    }
}
