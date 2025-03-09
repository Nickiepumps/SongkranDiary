using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Status", menuName = "Player Status")]
public class PlayerStatSO : ScriptableObject
{
    public PlayerUpgradeType upgradeType;
    public int level;
    public int hpPoint;
    public int ultChargeTime;
}
public enum PlayerUpgradeType
{
    playerHP,
    playerUlt,
}
