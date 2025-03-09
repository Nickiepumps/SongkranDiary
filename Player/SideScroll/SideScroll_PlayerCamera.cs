using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform minCamDistX, minCamDistY, maxCamDistX, maxCamDistY;
    private Camera playerCam;
    private void Start()
    {
        playerCam = Camera.main;
    }
    private void FixedUpdate()
    {
        playerCam.transform.position = Vector3.Lerp(playerCam.transform.position,
            new Vector3(playerTarget.position.x, playerTarget.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Follow player smoothly
        playerCam.transform.position = new Vector3(Mathf.Clamp(playerCam.transform.position.x, minCamDistX.transform.position.x, maxCamDistX.transform.position.x),
            Mathf.Clamp(playerCam.transform.position.y, minCamDistY.transform.position.y, maxCamDistY.transform.position.y),
            playerCam.transform.position.z); // Camera focus on the player
    }
}
