using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletShooting : ShootingSubject
{
    [Header("Weapon Data Reference")]
    public PlayerStats weaponData;
    // Current bullet stats
    private float xDir;
    private float currentASPD;
    private float aspd;
    private float currentTravelSpeed;
    private float travelSpeed;
    // Current bullet type
    private bool normalBullet = true;
    private bool sprdBullet = false;
    private bool laserBullet = false;

    [Header("Bullet Pooler Reference")]
    [SerializeField] private BulletPooler bulletPooler;

    [Header("AimPivot")]
    [SerializeField] private Transform aimPivot;

    [Header("Normal Bullet Spawner")]
    [SerializeField] private Transform spawnDirection_Right;
    [SerializeField] private Transform spawnDirection_Left;

    [Header("Spread Bullet Spawner")]
    [SerializeField] private List<Transform> sprdSpawnDirection_Right;
    [SerializeField] private List<Transform> sprdSpawnDirection_Left;

    [Header("Laser Bullet Spawner")]
    [SerializeField] private Transform laserSpawnDirection_Right;
    [SerializeField] private Transform laserSpawnDirection_Left;

    [Header("Player HUD Bullet Display")]
    [SerializeField] private Image sprdBulletKeyIcon;
    [SerializeField] private Image laserBulletKeyIcon;
    [SerializeField] private Image sprdBulletKeyIconBG;
    [SerializeField] private Image laserBulletKeyIconBG;

    [SerializeField] private SpriteRenderer playerSpriteDirection; // Player's sprite facing direction

    [SerializeField] private GameObject muzzleFlash;
    
    [HideInInspector] public float switchCoolDown = 2f;
    [HideInInspector] public float currentBulletSwitchCoolDownTimer;
    [HideInInspector] public bool coolDownStatus = false;
    private PlayerSideScrollStateController sidescrollPlayer;
    private void Awake()
    {
        sidescrollPlayer = GetComponent<PlayerSideScrollStateController>();
    }
    private void Start()
    {
        InvokeRepeating("CheckAimAngle", 0, 0.15f); // Aiming angle check

        currentASPD = weaponData.currentNormalASPD.aspd;
        currentTravelSpeed = weaponData.currentWeaponTravelSpeed.aspd;
        currentBulletSwitchCoolDownTimer = switchCoolDown;
        aspd = currentASPD;
    }
    private void Update()
    {
        #region Shooting
        // Shooting
        if(sidescrollPlayer.isGameStart == true)
        {
            ShootingControl();
            UpdateBulletDisplay();
        }
        #endregion
    }
    private void ShootingControl()
    {
        if (Input.GetKeyDown(KeyCode.Q) && coolDownStatus == false && sprdBullet == false)
        {
            StartCoroutine(SwitchCoolDown());
            currentASPD = weaponData.currentSprdBulletASPD.aspd;
            aspd = currentASPD;
            bulletPooler.SwitchBulletType(BulletType.spreadBullet);
            SwitchBulletSpawnerType(BulletType.spreadBullet);
            NotifyShootingObserver(ShootingAction.switchtospread);
            normalBullet = false;
            sprdBullet = true;
            laserBullet = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && coolDownStatus == false && sprdBullet == true)
        {
            StartCoroutine(SwitchCoolDown());
            currentASPD = weaponData.currentNormalASPD.aspd;
            currentTravelSpeed = weaponData.currentWeaponTravelSpeed.aspd;
            aspd = currentASPD;
            bulletPooler.SwitchBulletType(BulletType.normalBullet);
            SwitchBulletSpawnerType(BulletType.normalBullet);
            NotifyShootingObserver(ShootingAction.switchtonormal);
            normalBullet = true;
            sprdBullet = false;
            laserBullet = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && coolDownStatus == false && laserBullet == false)
        {
            StartCoroutine(SwitchCoolDown());
            currentASPD = weaponData.currentLsrBulletASPD.aspd;
            aspd = currentASPD;
            bulletPooler.SwitchBulletType(BulletType.laser);
            SwitchBulletSpawnerType(BulletType.laser);
            NotifyShootingObserver(ShootingAction.switchtolaser);
            normalBullet = false;
            sprdBullet = false;
            laserBullet = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && coolDownStatus == false && laserBullet == true)
        {
            StartCoroutine(SwitchCoolDown());
            currentASPD = weaponData.currentNormalASPD.aspd;
            aspd = currentASPD;
            bulletPooler.SwitchBulletType(BulletType.normalBullet);
            SwitchBulletSpawnerType(BulletType.normalBullet);
            NotifyShootingObserver(ShootingAction.switchtonormal);
            normalBullet = true;
            sprdBullet = false;
            laserBullet = false;
        }
        if (Input.GetMouseButton(0))
        {
            muzzleFlash.SetActive(true);
            aspd -= Time.deltaTime;
            if (aspd <= 0)
            {
                if (normalBullet == true)
                {
                    ShootingNormalBullet();
                }
                else if (sprdBullet == true)
                {
                    ShootSpreadBullet();
                }
                else
                {
                    ShootLaserBullet();
                }
                aspd = currentASPD;
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aimPivot.localPosition = new Vector3(aimPivot.localPosition.x, aimPivot.localPosition.y - 0.497f, 0);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            aimPivot.localPosition = Vector3.zero;
        }
    }
    private void ShootingNormalBullet()
    {
        spawnDirection_Right.gameObject.GetComponent<PlayerGun>().ShootBullet(BulletType.normalBullet);
    }
    private void ShootSpreadBullet()
    {
        for (int i = 0; i < weaponData.currentWeaponSprdCount.spreadCount; i++)
        {
            sprdSpawnDirection_Right[i].gameObject.GetComponent<PlayerGun>().ShootBullet(BulletType.spreadBullet);
        }
    }
    private void ShootLaserBullet()
    {
        laserSpawnDirection_Right.gameObject.GetComponent<PlayerGun>().ShootBullet(BulletType.laser);
    }
    public void SwitchBulletSpawnerType(BulletType newBulletSpawnerType)
    {
        if(newBulletSpawnerType == BulletType.normalBullet)
        {
            spawnDirection_Right.gameObject.SetActive(true);
            spawnDirection_Left.gameObject.SetActive(true);
            laserSpawnDirection_Right.gameObject.SetActive(false);
            laserSpawnDirection_Left.gameObject.SetActive(false);
            for (int i = 0; i < weaponData.currentWeaponSprdCount.spreadCount; i++)
            {
                sprdSpawnDirection_Right[i].gameObject.SetActive(false);
            }
        }
        else if(newBulletSpawnerType == BulletType.spreadBullet)
        {
            spawnDirection_Right.gameObject.SetActive(false);
            spawnDirection_Left.gameObject.SetActive(false);
            laserSpawnDirection_Right.gameObject.SetActive(false);
            laserSpawnDirection_Left.gameObject.SetActive(false);
            for (int i = 0; i < weaponData.currentWeaponSprdCount.spreadCount; i++)
            {
                sprdSpawnDirection_Right[i].gameObject.SetActive(true);
            }
        }
        else
        {
            spawnDirection_Right.gameObject.SetActive(false);
            spawnDirection_Left.gameObject.SetActive(false);
            laserSpawnDirection_Right.gameObject.SetActive(true);
            laserSpawnDirection_Left.gameObject.SetActive(true);
            for (int i = 0; i < weaponData.currentWeaponSprdCount.spreadCount; i++)
            {
                sprdSpawnDirection_Right[i].gameObject.SetActive(false);
            }
        }
    }
    private float AimAngle()
    {
        // Calculate angle of current mouse position base on player's origin
        Vector2 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = MathF.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        return angle;
    }
    private void CheckAimAngle()
    {
        if (AimAngle() > -180 && AimAngle() <= -150)
        {
            NotifyShootingObserver(ShootingAction.aimleft);
        }
        else if (AimAngle() > -150 && AimAngle() <= -120)
        {
            NotifyShootingObserver(ShootingAction.aim45downleft);
        }
        else if (AimAngle() > -120 && AimAngle() <= -60)
        {
            NotifyShootingObserver(ShootingAction.aimdown);
        }
        else if (AimAngle() > -60 && AimAngle() <= -30)
        {
            NotifyShootingObserver(ShootingAction.aim45downright);
        }
        else if (AimAngle() > -30 && AimAngle() <= 30)
        {
            NotifyShootingObserver(ShootingAction.aimright);
        }
        else if (AimAngle() > 30 && AimAngle() <= 60)
        {
            NotifyShootingObserver(ShootingAction.aim45topright);
        }
        else if (AimAngle() > 60 && AimAngle() <= 120)
        {
            NotifyShootingObserver(ShootingAction.aimtop);
        }
        else if (AimAngle() > 120 && AimAngle() <= 150)
        {
            NotifyShootingObserver(ShootingAction.aim45topleft);
        }
        else if (AimAngle() > 150 && AimAngle() <= 180)
        {
            NotifyShootingObserver(ShootingAction.aimleft);
        }
    }
    private IEnumerator SwitchCoolDown()
    {
        coolDownStatus = true;
        yield return new WaitForSeconds(switchCoolDown);
        coolDownStatus = false;
    }
    private void UpdateBulletDisplay()
    {
        if(coolDownStatus == true)
        {
            currentBulletSwitchCoolDownTimer -= Time.deltaTime;
            sprdBulletKeyIcon.fillAmount = 1 - (currentBulletSwitchCoolDownTimer / switchCoolDown);
            laserBulletKeyIcon.fillAmount = 1 - (currentBulletSwitchCoolDownTimer / switchCoolDown);
            if (currentBulletSwitchCoolDownTimer <= 0)
            {
                currentBulletSwitchCoolDownTimer = switchCoolDown;
            }
        }
        else
        {
            sprdBulletKeyIcon.fillAmount = 1;
            laserBulletKeyIcon.fillAmount = 1;
        }
    }
}
