using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltimateController : MonoBehaviour
{
    private bool ultReady = false;
    [SerializeField] private int ultAmount = 3; // To Do: Change to WeaponSO later
    [SerializeField] private float ultChargeTime = 5f; // To Do: Change to WeaponSO later
    [SerializeField] private Transform spawnDirection_Right;
    private float currentTime;
    private int currentUltReady = 0;
    private void Start()
    {
        currentTime = ultChargeTime;
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
                currentUltReady = 3;
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
    }
    private void ThrowProjectile()
    {
        spawnDirection_Right.gameObject.GetComponent<PlayerGun>().ShootUltimate();
    }
}
