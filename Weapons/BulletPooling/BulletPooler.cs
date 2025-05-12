using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] private int bulletAmount; // Amount of bullet to pool
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab
    [SerializeField] private int ultAmount; // Maximum amount of player's ultimate
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPosition; // Bullet spawn position
    //public WeaponSO bulletData;
    public List<GameObject> pooledbullets = new List<GameObject>(); // List for storing spawned bullet gameObjects
    private Dictionary<BulletType, Sprite> bulletSpriteDict = new Dictionary<BulletType, Sprite>(); // Bullet sprite dictionary
    [SerializeField] private Sprite[] bulletSpriteArray;
    //public List<Sprite> bulletTypeSpritesList;
    public List<GameObject> pooledUltLists = new List<GameObject>(); // List for storing spawned ultimate gameObjects

    [Header("Bullet Animator Controller")]
    [SerializeField] private AnimatorController normalBulletAnimatorController;
    [SerializeField] private AnimatorController spreadBulletAnimatorController;
    [SerializeField] private AnimatorController laserBulletAnimatorController;

    [HideInInspector]
    public AnimatorController currentAnimator;
    public Sprite currentBulletSprite;
    public BulletType currentBulletType;
    public Vector2 currentLocalTransform;
    private void Awake()
    {
        bulletSpriteDict.Add(BulletType.normalBullet, bulletSpriteArray[0]);
        bulletSpriteDict.Add(BulletType.spreadBullet, bulletSpriteArray[1]);
        bulletSpriteDict.Add(BulletType.laser, bulletSpriteArray[2]);
    }
    private void Start()
    {
        currentBulletSprite = bulletSpriteArray[0];
        currentBulletType = BulletType.normalBullet;
        currentAnimator = normalBulletAnimatorController;
        currentLocalTransform = bulletPrefab.transform.localScale;
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition.position, Quaternion.identity);
            pooledbullets.Add(bullet);
            bullet.SetActive(false);
        }
        for(int i = 0; i < ultAmount; i++)
        {
            GameObject ultBullet = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity);
            pooledUltLists.Add(ultBullet);
            ultBullet.SetActive(false);
        }
    }
    public GameObject EnableBullet()
    {
        for(int i = 0;i < pooledbullets.Count; i++)
        {
            if (pooledbullets[i].activeSelf == false)
            {
                return pooledbullets[i];
            }
        }
        return null;
    }
    public GameObject EnableUltimate()
    {
        for (int i = 0; i < pooledUltLists.Count; i++)
        {
            if (pooledUltLists[i].activeSelf == false)
            {
                return pooledUltLists[i];
            }
        }
        return null;
    }
    public void SwitchBulletType(BulletType newBulletType)
    {
        currentBulletType = newBulletType;
        bulletSpriteDict.TryGetValue(currentBulletType, out currentBulletSprite);
        switch (newBulletType)
        {
            case BulletType.normalBullet:
                currentAnimator = normalBulletAnimatorController;
                break;
            case BulletType.spreadBullet:
                currentAnimator = spreadBulletAnimatorController;
                break;
            case BulletType.laser:
                currentAnimator = laserBulletAnimatorController;
                break;
        }
        for (int i = 0; i < pooledbullets.Count; i++)
        {
            if(pooledbullets[i].gameObject.activeSelf == false)
            {
                pooledbullets[i].GetComponent<Bullet>().bulletType = newBulletType;
                pooledbullets[i].GetComponent<Bullet>().bulletSprite.sprite = currentBulletSprite;
                pooledbullets[i].GetComponent<Bullet>().bulletSprite.drawMode = SpriteDrawMode.Simple;
                pooledbullets[i].GetComponent<Bullet>().transform.GetChild(0).transform.localScale = Vector2.one;
            }
            if (pooledbullets[i].GetComponent<Bullet>().bulletType == BulletType.normalBullet)
            {
                pooledbullets[i].GetComponent<Bullet>().bulletAnimator.runtimeAnimatorController = normalBulletAnimatorController;
            }
            else if(pooledbullets[i].GetComponent<Bullet>().bulletType == BulletType.spreadBullet)
            {
                pooledbullets[i].GetComponent<Bullet>().bulletAnimator.runtimeAnimatorController = spreadBulletAnimatorController;
            }
            else if(pooledbullets[i].GetComponent<Bullet>().bulletType == BulletType.laser)
            {
                pooledbullets[i].GetComponent<Bullet>().bulletAnimator.runtimeAnimatorController = laserBulletAnimatorController;
            }
        }
    }
}
