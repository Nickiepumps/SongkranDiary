using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle_FireHydrant : MonoBehaviour
{
    [Header("Fire Hydrant GameObject")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterSplash;
    [SerializeField] private Animator waterAnimator;
    [SerializeField] private BoxCollider2D waterTrigger;

    [Header("Properties")]
    public bool isShootingIndefinitely; // Is true, The water will continue shooting forever until the player shoot the switch
    public GameObject waterStopper; // Use this when The player need to shoot the lever to stop the hydrant from shooting the water
    [SerializeField] private Transform waterPeakPos;
    //[SerializeField] private float waterHoldTime = 2f;
    //[SerializeField] private float waterTravelSpeed = 1f;
    public float waterCooldownTime = 3f; // normal cooldown
    [SerializeField] private float waterInitialCooldownTime = 3f; // Cooldown time when first start the game
    private float currentHoldTime, currentCooldownTime;
    private float originalWaterYScale;
    private bool isShoot = false;
    private void OnEnable()
    {
        waterAnimator.enabled = true;
        currentCooldownTime = waterCooldownTime;
    }
    private void OnDisable()
    {
        waterAnimator.enabled = false;
    }
    private void Start()
    {
        //originalWaterYScale = water.transform.localScale.y;
        waterTrigger.enabled = false;
        currentCooldownTime = waterCooldownTime;
        //currentHoldTime = waterHoldTime;
        waterAnimator.SetBool("WaterIdle", true);
    }
    private void Update()
    {
        if(isShootingIndefinitely == false)
        {
            currentCooldownTime -= Time.deltaTime;
            if (currentCooldownTime <= 1 && currentCooldownTime > 0)
            {
                waterAnimator.SetBool("WaterWarning", true);
                waterAnimator.SetBool("WaterIdle", false);
                waterAnimator.SetBool("WaterShoot", false);
            }
            if(currentCooldownTime <= 0 && isShoot == false)
            {
                StartCoroutine(WaterShootAnim());
            }
        }
        else
        {
            //WaterShoot();
        }
    }
    private IEnumerator WaterShootAnim()
    {
        isShoot = true;
        waterTrigger.enabled = true;
        waterAnimator.SetBool("WaterShoot", true);
        waterAnimator.SetBool("WaterIdle", false);
        waterAnimator.SetBool("WaterWarning", false);
        yield return new WaitForSeconds(0.83f);
        isShoot = false;
        waterTrigger.enabled = false;
        waterAnimator.SetBool("WaterIdle", true);
        waterAnimator.SetBool("WaterWarning", false);
        waterAnimator.SetBool("WaterShoot", false);
        currentCooldownTime = waterCooldownTime;
    }
    private void WaterShoot()
    {
        /*water.SetActive(true);
        // Scale the water Y axis to match the peak point y position
        if (water.transform.localScale.y < waterPeakPos.localPosition.y)
        {
            water.transform.localScale += new Vector3(0, waterTravelSpeed * Time.deltaTime, 0);
        }
        else
        {
            water.transform.localScale = new Vector3(water.transform.localScale.x, waterPeakPos.localPosition.y, water.transform.localScale.z);
        }

        // Active the watersplash effect when the water reaches the peak point
        if (water.transform.localScale.y >= waterPeakPos.localPosition.y)
        {
            if (isShootingIndefinitely == false)
            {
                currentHoldTime -= Time.deltaTime; // Shooting the water for 'HoldTime' amount
            }
            waterSplash.SetActive(true);
            waterSplash.transform.localPosition = waterPeakPos.localPosition;
        }*/
    }
    private void WaterStop()
    {
        /*// Scale the water Y axis to match the peak point y position
        if (water.transform.localScale.y >= originalWaterYScale)
        {
            water.transform.localScale -= new Vector3(0, waterTravelSpeed * Time.deltaTime, 0);
            waterSplash.transform.localPosition = new Vector2(waterSplash.transform.localPosition.x, water.transform.localScale.y);
        }
        if (isShootingIndefinitely == false)
        {
            if (water.transform.localScale.y <= originalWaterYScale)
            {
                water.SetActive(false);
                waterSplash.SetActive(false);
                // Reset timers
                currentCooldownTime = waterCooldownTime;
                currentHoldTime = waterHoldTime;
            }
        }*/
    }
}
