using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Delete this variable when finished prototype
    [SerializeField] private TempPlayerDataSave tempData;

    // To do: Use this script for saving data
    [Header("Player Stats")]
    public PlayerStatSO maxPlayerHP;
    public PlayerStatSO currentPlayerHP;
    public PlayerStatSO maxPlayerUltCharge;
    public PlayerStatSO currentPlayerUltCharge;
    public PlayerStatSO currentPlayerUltAmount;

    [Space]
    [Header("Weapon Stats")]
    [Header("Normal Bullet")]
    public WeaponSO currentNormalASPD;
    public WeaponSO maxNormalASPD;
    public WeaponSO currentWeaponTravelSpeed;
    public WeaponSO maxWeaponTravelSpeed;

    [Space]
    [Header("Spread Bullet")]
    public WeaponSO currentSprdBulletASPD;
    public WeaponSO maxSprdBulletASPD;
    public WeaponSO currentWeaponSprdCount;
    public WeaponSO maxWeaponSprdCount;

    [Space]
    [Header("Laser Bullet")]
    public WeaponSO currentLsrBulletASPD;
    public WeaponSO maxLsrBulletASPD;

    [Space]
    [Header("Ultimate")]
    public WeaponSO maxPlayerUltAmount;
    public WeaponSO maxUltimateTravelSpeed;

    [Space]
    [Header("Coin")]
    public int coinAmount;

    private void Start()
    {
        if(tempData != null)
        {
            currentPlayerHP = tempData.currentPlayerHP;
            currentPlayerUltCharge = tempData.currentPlayerUltCharge;
            currentNormalASPD = tempData.currentNormalASPD;
            currentWeaponTravelSpeed = tempData.currentWeaponTravelSpeed;
            currentSprdBulletASPD = tempData.currentSprdBulletASPD;
            currentWeaponSprdCount = tempData.currentWeaponSprdCount;
            currentLsrBulletASPD = tempData.currentLsrBulletASPD;
            coinAmount = tempData.coinAmount;
        }
    }
}
