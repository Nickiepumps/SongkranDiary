using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour, IPlayerObserver
{
    [SerializeField] private PlayerSubject player; // Player's observer subject
    private Camera playerCam; // Player's camera
    [SerializeField] private Transform playerTarget; // Player's transform 
    [SerializeField] private float maxCamDistX, maxCamDistY, minCamDistX, minCamDistY; // Min/Max camera distance to follow player and not out of bound
    [SerializeField] private float followSpeed = 3f; // Camera follow speed
    private float camZoomInSize = 1.5f;
    private float camZoomOutSize = 3f;
    private bool zoomIn, zoomOut;
    private void OnEnable()
    {
        playerCam = Camera.main;
        player.AddPlayerObserver(this);
    }
    private void OnDisable()
    {
        player.RemovePlayerObserver(this);
    }
    private void FixedUpdate()
    {
        if (zoomIn)
        {
            Transform npcTransform = player.GetComponent<PlayerStateController>().currentColHit.gameObject.transform;
            playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, camZoomInSize, 3f * Time.fixedDeltaTime);
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, new Vector3(npcTransform.position.x,
                npcTransform.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Camera focus on the NPC
        }
        else if (zoomOut)
        {
            playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, camZoomOutSize, 3f * Time.fixedDeltaTime);
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position,
            new Vector3(playerTarget.position.x, playerTarget.position.y, playerCam.transform.position.z), followSpeed * Time.deltaTime); // Follow player smoothly
            /*playerCam.transform.position = new Vector3(Mathf.Clamp(playerCam.transform.position.x, minCamDistX, maxCamDistX), Mathf.Clamp(playerCam.transform.position.y, minCamDistY, maxCamDistY),
                playerCam.transform.position.z); // Camera focus on the player*/
        }
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case(PlayerAction.Idle):
                zoomIn = false;
                zoomOut = true;
                return;
            case (PlayerAction.Talk):
                zoomOut = false;
                zoomIn = true;
                return;
            case (PlayerAction.Walk):
                zoomIn = false;
                zoomOut = true;
                return;
        }
    }
}
