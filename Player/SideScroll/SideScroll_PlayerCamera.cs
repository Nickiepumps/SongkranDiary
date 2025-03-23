using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll_PlayerCamera : MonoBehaviour, IPlayerObserver
{
    [Header("Observer Reference")]
    [SerializeField] private PlayerSubject playerSubject;

    [Header("Camera Target Properties")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform minCamDistX, minCamDistY, maxCamDistX, maxCamDistY;

    [Header("Camera Shake Properties")]
    [SerializeField] private Transform cameraParent;
    [SerializeField] private float minShake;
    [SerializeField] private float maxShake;
    private Camera playerCam;
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

    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch(playerAction)
        {
            case (PlayerAction.Damaged):
                StopAllCoroutines();
                StartCoroutine(CameraShake(1f, minShake, maxShake));
                return;
        }
    }
    private IEnumerator CameraShake(float shakeDuration, float minShake, float maxShake)
    {
        float elapsed = 0f;
        float currentMagnitude = 1f;
        while (elapsed < shakeDuration)
        {
            float x = (Random.Range(minShake, maxShake)) * currentMagnitude;
            float y = (Random.Range(minShake, maxShake)) * currentMagnitude;
            cameraParent.localPosition = new Vector3(x, y, cameraParent.localPosition.z);
            elapsed += Time.deltaTime;
            currentMagnitude = (1 - (elapsed / shakeDuration)) * (1 - (elapsed / shakeDuration));
            yield return null;
        }
        cameraParent.localPosition = new Vector3(0,0,cameraParent.localPosition.z);
    }
}
