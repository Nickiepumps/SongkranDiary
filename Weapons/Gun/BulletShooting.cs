using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletShooting : ShootingSubject, IPlayerObserver
{
    [Header("Player Observer Reference")]
    [SerializeField] private PlayerSubject playerSubject;

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

    [Header("Gun Audio Reference")]
    [SerializeField] private AudioSource gunAudioPlayer;
    [SerializeField] private AudioClip[] bulletAudioClipArr;
    
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
    
    [HideInInspector] public int currentBulletArrayIndex = 0; // Current index in bullet array start from 0-2 
    [HideInInspector] public float switchCoolDown = 2f;
    [HideInInspector] public float currentBulletSwitchCoolDownTimer;
    [HideInInspector] public bool coolDownStatus = false;
    private float angle;
    public bool isAim = false;
    public bool isCrouch = false;
    public bool isRun = false;
    public bool isShoot = false;
    private PlayerSideScrollStateController sidescrollPlayer;
    private void Awake()
    {
        sidescrollPlayer = GetComponent<PlayerSideScrollStateController>();
    }
    private void OnEnable()
    {
        playerSubject.AddPlayerObserver(this);
    }
    private void OnDisable()
    {
        playerSubject.RemovePlayerObserver(this);
    }
    private void Start()
    {
        //InvokeRepeating("CheckAimAngle", 0, 0.15f); // Aiming angle check

        currentASPD = weaponData.currentNormalASPD.aspd;
        currentTravelSpeed = weaponData.currentWeaponTravelSpeed.aspd;
        currentBulletSwitchCoolDownTimer = switchCoolDown;
        aspd = currentASPD;

        gunAudioPlayer.clip = bulletAudioClipArr[0];
    }
    private void Update()
    {
        #region Shooting
        // Shooting
        if(sidescrollPlayer.isGameStart == true)
        {
            CheckAimAngle();
            ShootingControl();
            UpdateBulletDisplay();
        }
        #endregion
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Side_Idle):
                isRun = false;
                isCrouch = false;
                break;
            case (PlayerAction.Run):
                isRun = true;
                isCrouch = false;
                break;
            case(PlayerAction.Crouch):
                isRun = false;
                isCrouch = true;
                break;
            case (PlayerAction.Jump):
                isRun = false;
                isCrouch = false;
                break;
        }
    }
    private void ShootingControl()
    {
        if (Input.GetKeyDown(KeyCode.E) && coolDownStatus == false)
        {
            currentBulletArrayIndex++;
            if (currentBulletArrayIndex > 2)
            {
                currentBulletArrayIndex = 0;
            }
            StartCoroutine(SwitchCoolDown());
            gunAudioPlayer.clip = bulletAudioClipArr[currentBulletArrayIndex];
            switch (currentBulletArrayIndex)
            {
                case 0:
                    currentASPD = weaponData.currentNormalASPD.aspd;
                    currentTravelSpeed = weaponData.currentWeaponTravelSpeed.aspd;
                    bulletPooler.SwitchBulletType(BulletType.normalBullet);
                    SwitchBulletSpawnerType(BulletType.normalBullet);
                    NotifyShootingObserver(ShootingAction.switchtonormal);
                    normalBullet = true;
                    sprdBullet = false;
                    laserBullet = false;
                    break;
                case 1:
                    currentASPD = weaponData.currentSprdBulletASPD.aspd;
                    bulletPooler.SwitchBulletType(BulletType.spreadBullet);
                    SwitchBulletSpawnerType(BulletType.spreadBullet);
                    NotifyShootingObserver(ShootingAction.switchtospread);
                    normalBullet = false;
                    sprdBullet = true;
                    laserBullet = false;
                    break;
                case 2:
                    currentASPD = weaponData.currentLsrBulletASPD.aspd;
                    bulletPooler.SwitchBulletType(BulletType.laser);
                    SwitchBulletSpawnerType(BulletType.laser);
                    NotifyShootingObserver(ShootingAction.switchtolaser);
                    normalBullet = false;
                    sprdBullet = false;
                    laserBullet = true;
                    break;
            }
            
            aspd = currentASPD;
        }
        /*if (Input.GetKeyDown(KeyCode.Q) && coolDownStatus == false && sprdBullet == false)
        {
            StartCoroutine(SwitchCoolDown());
            gunAudioPlayer.clip = bulletAudioClipArr[1];
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
            gunAudioPlayer.clip = bulletAudioClipArr[0];
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
            gunAudioPlayer.clip = bulletAudioClipArr[2];
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
            gunAudioPlayer.clip = bulletAudioClipArr[0];
            currentASPD = weaponData.currentNormalASPD.aspd;
            aspd = currentASPD;
            bulletPooler.SwitchBulletType(BulletType.normalBullet);
            SwitchBulletSpawnerType(BulletType.normalBullet);
            NotifyShootingObserver(ShootingAction.switchtonormal);
            normalBullet = true;
            sprdBullet = false;
            laserBullet = false;
        }*/
        if (Input.GetKey(KeyCode.L))
        {
            isAim = true;
        }
        else
        {
            isAim = false;
        }
        if (Input.GetKey(KeyCode.J))
        {
            isShoot = true;
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
            isShoot = false;
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
        gunAudioPlayer.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        gunAudioPlayer.Play();
    }
    private void ShootSpreadBullet()
    {
        for (int i = 0; i < weaponData.currentWeaponSprdCount.spreadCount; i++)
        {
            sprdSpawnDirection_Right[i].gameObject.GetComponent<PlayerGun>().ShootBullet(BulletType.spreadBullet);
        }
        gunAudioPlayer.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        gunAudioPlayer.Play();
    }
    private void ShootLaserBullet()
    {
        laserSpawnDirection_Right.gameObject.GetComponent<PlayerGun>().ShootBullet(BulletType.laser);
        gunAudioPlayer.pitch = UnityEngine.Random.Range(0.8f, 1.3f);
        gunAudioPlayer.Play();
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
        // Mouse Aiming
        // Calculate angle of current mouse position base on player's origin
        /*Vector2 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = MathF.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        return angle;*/

        // Keyboard Aiming
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            angle = 45;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            angle = 135;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            angle = 90;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            angle = 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            angle = 180;
        }
        else
        {
            if (playerSpriteDirection.flipX == false)
            {
                angle = 0;
            }
            else
            {
                angle = 180;
            }
        }
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
            /*if(isCrouch == false)
            {
                NotifyShootingObserver(ShootingAction.aim45downleft);
            }*/
        }
        else if (AimAngle() > -120 && AimAngle() <= -60)
        {
            //NotifyShootingObserver(ShootingAction.aimdown);
        }
        else if (AimAngle() > -60 && AimAngle() <= -30)
        {
            /*if(isCrouch == false)
            {
                NotifyShootingObserver(ShootingAction.aim45downright);
            }*/
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
            if(isCrouch == false && isRun == false)
            {
                NotifyShootingObserver(ShootingAction.aimtop);
            }
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
