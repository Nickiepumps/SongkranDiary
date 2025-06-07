using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_FireHydrant : MonoBehaviour
{
    [Header("Fire Hydrant images")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject waterSplash;
    [Header("Properties")]
    [SerializeField] private Transform waterPeakPos;
    [SerializeField] private float waterHoldTime = 2f;
    [SerializeField] private float waterTravelSpeed = 1f;
    [SerializeField] private float waterCooldownTime = 3f; // normal cooldown
    [SerializeField] private float waterInitialCooldownTime = 3f; // Cooldown time when first start the game
    private float currentHoldTime, currentCooldownTime;
    private float originalWaterYScale;
    private void Start()
    {
        originalWaterYScale = water.transform.localScale.y;
        currentCooldownTime = waterInitialCooldownTime;
        currentHoldTime = waterHoldTime;
    }
    private void Update()
    {
        currentCooldownTime -= Time.deltaTime;
        if(currentCooldownTime <= 0 && currentHoldTime >= 0)
        {
            WaterShoot();
        }
        if(currentHoldTime <= 0)
        {
            // Reset water scale and water splash position and then deactivate
            WaterStop();
        }
    }
    private void WaterShoot()
    {
        currentHoldTime -= Time.deltaTime; // Shooting the water for 'HoldTime' amount
        water.SetActive(true);
        // Scale the water Y axis to match the peak point y position
        if (water.transform.localScale.y <= waterPeakPos.localPosition.y)
        {
            water.transform.localScale += new Vector3(0, waterTravelSpeed * Time.deltaTime, 0);
        }
        // Active the watersplash effect when the water reaches the peak point
        if (water.transform.localScale.y >= waterPeakPos.localPosition.y)
        {
            waterSplash.SetActive(true);
            waterSplash.transform.localPosition = waterPeakPos.localPosition;
        }
    }
    private void WaterStop()
    {
        // Scale the water Y axis to match the peak point y position
        if (water.transform.localScale.y >= originalWaterYScale)
        {
            water.transform.localScale -= new Vector3(0, waterTravelSpeed * Time.deltaTime, 0);
            waterSplash.transform.localPosition = new Vector2(waterSplash.transform.localPosition.x, water.transform.localScale.y);
        }
        // Active the watersplash effect when the water reaches the peak point
        if (water.transform.localScale.y <= originalWaterYScale)
        {
            water.SetActive(false);
            waterSplash.SetActive(false);
            // Reset timers
            currentCooldownTime = waterCooldownTime;
            currentHoldTime = waterHoldTime;
        }
    }
}
