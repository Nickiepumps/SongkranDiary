using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : MonoBehaviour, IGameObserver
{
    [Header("Temp PlayerDataSO")]
    [SerializeField] private TempPlayerDataSave tempDataSO;

    [Header("Observer Reference")]
    [SerializeField] private GameSubject gameUIControllerSubject;
    private GameUIController gameUIController;

    [Header("Player's Stat Reference")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Upgrade Bar Images")]
    [SerializeField] private Image p_HP_UpgradeBar;
    [SerializeField] private Image p_Ult_UpgradeBar;
    [SerializeField] private Image bl_norm_ASPD_UpgradeBar;
    [SerializeField] private Image bl_norm_bulletSPD_UpgradeBar;
    [SerializeField] private Image bl_Sprd_SprdCount_UpgradeBar;
    [SerializeField] private Image bl_Sprd_ASPD_UpgradeBar;
    [SerializeField] private Image bl_Lsr_ASPD_UpgradeBar;

    [Header("UpgradeBar Sprites Variants")]
    [SerializeField] private List<Sprite> p_HP_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> p_Ult_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_norm_ASPD_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List <Sprite> bl_norm_bulletSPD_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_Sprd_SprdCount_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_Sprd_ASPD_UpgradeBarLevel = new List<Sprite>();
    [SerializeField] private List<Sprite> bl_Lsr_ASPD_UpgradeBarLevel = new List<Sprite>();

    [Header("Button Sprites")]
    [SerializeField] private Sprite[] btn_ConfirmUpgrade;

    [Header("Button Objects")]
    [SerializeField] private GameObject p_HP_UpgradeBtn;
    [SerializeField] private GameObject p_Ult_UpgradeBarBtn;
    [SerializeField] private GameObject bl_norm_ASPD_UpgradeBtn;
    [SerializeField] private GameObject bl_norm_bulletSPD_UpgradeBtn;
    [SerializeField] private GameObject bl_Sprd_SprdCount_UpgradeBtn;
    [SerializeField] private GameObject bl_Sprd_ASPD_UpgradeBtn;
    [SerializeField] private GameObject bl_Lsr_ASPD_UpgradeBtn;
    [SerializeField] private GameObject confirmUpgradeBtn;

    [Header("Player Stats Scriptable Objects")]
    [SerializeField] private List<PlayerStatSO> playerHPLists = new List<PlayerStatSO>();
    [SerializeField] private List<PlayerStatSO> playerUltChargeLists = new List<PlayerStatSO>();

    [Space]
    [Header("Weapon Stats Scriptable Objects")]
    [Header("Normal Bullet Stats")]
    [SerializeField] private List<WeaponSO> normalBulletASPDLists = new List<WeaponSO>();
    [SerializeField] private List<WeaponSO> normalBulletTASPDLists = new List<WeaponSO>();
    [Header("Spread Bullet Stats")]
    [SerializeField] private List<WeaponSO> spreadBulletASPDLists = new List<WeaponSO>();
    [SerializeField] private List<WeaponSO> spreadBulletCountLists = new List<WeaponSO>();
    [Header("Laser Bullet Stats")]
    [SerializeField] private List<WeaponSO> laserBulletASPDLists = new List<WeaponSO>();

    private List<PlayerStatSO> allSelectedPlayerStatLists = new List<PlayerStatSO>(); // All selected player's abilities waiting for upgrade
    private List<WeaponSO> allSelectedWeaponStatLists = new List<WeaponSO>(); // All selected weapons' abilities waiting for upgrade
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {

    }

    private void Awake()
    {
        gameUIController = gameUIControllerSubject.GetComponent<GameUIController>();
    }
    private void OnEnable()
    {
        gameUIControllerSubject.AddGameObserver(this);
    }
    private void OnDisable()
    {
        gameUIControllerSubject.RemoveGameObserver(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check current player's HP/Ult stat
            if(playerStats.currentPlayerHP.level != 3)
            {
                p_HP_UpgradeBtn.SetActive(true);
            }
            if(playerStats.currentPlayerUltCharge.level != 3)
            {
                p_Ult_UpgradeBarBtn.SetActive(true);
            }
            gameObject.SetActive(false);
            gameUIControllerSubject.NotifyGameObserver(IsometricGameState.Play);
        }
    }
    public void AddPlayerAbility(PlayerStatSO playerStatType)
    {
        if(playerStatType.upgradeType == PlayerUpgradeType.playerHP)
        {
            int minLevel = -1;
            int currentLevel = playerStats.currentPlayerHP.level;
            allSelectedPlayerStatLists.Add(playerHPLists[minLevel + currentLevel]);
            p_HP_UpgradeBar.sprite = p_HP_UpgradeBarLevel[minLevel + currentLevel];
            p_HP_UpgradeBtn.SetActive(false);
            Debug.Log("Add player HP level" + playerHPLists[minLevel + currentLevel].level);
        }
        if (playerStatType.upgradeType == PlayerUpgradeType.playerUlt)
        {
            int minLevel = -1;
            int currentLevel = playerStats.currentPlayerUltCharge.level;
            allSelectedPlayerStatLists.Add(playerUltChargeLists[minLevel + currentLevel]);
            p_Ult_UpgradeBar.sprite = p_Ult_UpgradeBarLevel[minLevel + currentLevel];
            p_Ult_UpgradeBarBtn.SetActive(false);
            Debug.Log("Add player Ult level" + playerUltChargeLists[minLevel + currentLevel].level);
        }
        confirmUpgradeBtn.SetActive(true);
    }
    public void AddWeaponAbility(WeaponSO weaponStat)
    {
        if(weaponStat.bulletType == BulletType.normalBullet)
        {
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletNormASPD)
            {
                int minLevel = -1;
                int currentLevel = playerStats.currentNormalASPD.level;
                allSelectedWeaponStatLists.Add(normalBulletASPDLists[minLevel + currentLevel]);
                bl_norm_ASPD_UpgradeBar.sprite = bl_norm_ASPD_UpgradeBarLevel[minLevel + currentLevel];
                bl_norm_ASPD_UpgradeBtn.SetActive(false);
            }
            if (weaponStat.upgradeType == WeaponUpgradeType.bulletNormSpeed)
            {
                int minLevel = -1;
                int currentLevel = playerStats.currentWeaponTravelSpeed.level;
                allSelectedWeaponStatLists.Add(normalBulletTASPDLists[minLevel + currentLevel]);
                bl_norm_bulletSPD_UpgradeBar.sprite = bl_norm_bulletSPD_UpgradeBarLevel[minLevel + currentLevel];
                bl_norm_bulletSPD_UpgradeBtn.SetActive(false);
            }
        }
        if(weaponStat.bulletType == BulletType.spreadBullet)
        {
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletSprdSprdCount)
            {
                int minLevel = -1;
                int currentLevel = playerStats.currentWeaponSprdCount.level;
                allSelectedWeaponStatLists.Add(spreadBulletCountLists[minLevel + currentLevel]);
                bl_Sprd_SprdCount_UpgradeBar.sprite = bl_Sprd_SprdCount_UpgradeBarLevel[minLevel + currentLevel];
                bl_Sprd_SprdCount_UpgradeBtn.SetActive(false);
            }
            if(weaponStat.upgradeType == WeaponUpgradeType.bulletSprdASPD)
            {
                int minLevel = -1;
                int currentLevel = playerStats.currentSprdBulletASPD.level;
                allSelectedWeaponStatLists.Add(spreadBulletASPDLists[minLevel + currentLevel]);
                bl_Sprd_ASPD_UpgradeBar.sprite = bl_Sprd_ASPD_UpgradeBarLevel[minLevel + currentLevel];
                bl_Sprd_ASPD_UpgradeBtn.SetActive(false);
            }
        }
        if(weaponStat.bulletType == BulletType.laser)
        {
            if (weaponStat.upgradeType == WeaponUpgradeType.bulletLsrASPD)
            {
                int minLevel = -1;
                int currentLevel = playerStats.currentLsrBulletASPD.level;
                allSelectedWeaponStatLists.Add(laserBulletASPDLists[minLevel + currentLevel]);
                bl_Lsr_ASPD_UpgradeBar.sprite = bl_Lsr_ASPD_UpgradeBarLevel[minLevel + currentLevel];
                bl_Lsr_ASPD_UpgradeBtn.SetActive(false);
            }
        }
        confirmUpgradeBtn.SetActive(true);
    }
    public void ConfirmUpgrade()
    {
        foreach(var playerConfirmStats in allSelectedPlayerStatLists)
        {
            if(playerConfirmStats.upgradeType == PlayerUpgradeType.playerHP)
            {
                playerStats.currentPlayerHP = playerConfirmStats;
                tempDataSO.currentPlayerHP = playerConfirmStats;
            }
            if(playerConfirmStats.upgradeType == PlayerUpgradeType.playerUlt)
            {
                playerStats.currentPlayerUltCharge = playerConfirmStats;
                tempDataSO.currentPlayerUltCharge = playerConfirmStats;
            }
        }
        foreach(var weaponConfirmStats in allSelectedWeaponStatLists)
        {
            if(weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletNormASPD)
            {
                playerStats.currentNormalASPD = weaponConfirmStats;
                tempDataSO.currentNormalASPD = weaponConfirmStats;
            }
            if(weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletNormSpeed)
            {
                playerStats.currentWeaponTravelSpeed = weaponConfirmStats;
                tempDataSO.currentWeaponTravelSpeed = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletSprdSprdCount)
            {
                playerStats.currentWeaponSprdCount = weaponConfirmStats;
                tempDataSO.currentWeaponSprdCount = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletSprdASPD)
            {
                playerStats.currentSprdBulletASPD = weaponConfirmStats;
                tempDataSO.currentSprdBulletASPD = weaponConfirmStats;
            }
            if (weaponConfirmStats.upgradeType == WeaponUpgradeType.bulletLsrASPD)
            {
                playerStats.currentLsrBulletASPD = weaponConfirmStats;
                tempDataSO.currentLsrBulletASPD = weaponConfirmStats;
            }
        }
        gameUIController.currentNPC.isPlayerUpgrade = true;
        confirmUpgradeBtn.SetActive(false);
    }
}
