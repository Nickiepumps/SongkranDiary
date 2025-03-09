using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum LandmarkType
{
    RunNGun,
    Boss
}

public class LevelLandMarkController : MonoBehaviour, IGameObserver, IPlayerObserver
{
    [Header("Observer References")]
    [SerializeField] private PlayerSubject playerSubject; // Player
    [SerializeField] private GameSubject isometricGameSubject; // Isometric Game subject To Do: find gameSubject object in the scene and reference it when turned into prefab

    [Header("Scene Controller Reference")]
    [SerializeField] private SceneController sceneController;
    
    [Header("Level Postcard and Notif References")]
    //[SerializeField] private GameObject levelPostcardCanvas; // Level Postcard Canvas
    [SerializeField] private PostcardWindow levelPostcard; // Level Postcard
    [SerializeField] private Button enterlevelBtn; // Level Postcard
    [SerializeField] private GameObject interactNotif; // Interact notification popup
    private GameObject player; // Player to Landmark trigger colliding status

    [Header("Landmark Properties")]
    [SerializeField] private LandmarkType landmarkType;
    [SerializeField] private string landmarkName;
    [SerializeField] private string sceneName;

    private void OnEnable()
    {
        isometricGameSubject.AddGameObserver(this);
        playerSubject.AddPlayerObserver(this);
    }
    private void OnDisable()
    {
        isometricGameSubject.RemoveGameObserver(this);
        playerSubject.RemovePlayerObserver(this);
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
    }
    public void OnPlayerNotify(PlayerAction playerAction)
    {
        switch (playerAction)
        {
            case (PlayerAction.Idle):
                if (player != null)
                {
                    interactNotif.SetActive(true);
                }
                else
                {
                    interactNotif.SetActive(false);
                }
                return;
            case (PlayerAction.Walk):
                if (player != null)
                {
                    interactNotif.SetActive(true);
                }
                else
                {
                    interactNotif.SetActive(false);
                }
                return;
            case (PlayerAction.Stop):
                interactNotif.SetActive(false);
                if (player != null)
                {
                    if(landmarkType == LandmarkType.RunNGun)
                    {
                        levelPostcard.levelTypeText.text = "Run n Gun";
                        enterlevelBtn.onClick.RemoveAllListeners();
                        enterlevelBtn.onClick.AddListener(ChangeScene);

                    }
                    else if(landmarkType == LandmarkType.Boss)
                    {
                        levelPostcard.levelTypeText.text = "Boss";
                        enterlevelBtn.onClick.RemoveAllListeners();
                        enterlevelBtn.onClick.AddListener(ChangeScene);
                    }
                    levelPostcard.postcardNameText.text = landmarkName;
                    levelPostcard.sceneName = sceneName;
                    levelPostcard.gameObject.SetActive(true);
                }
                return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
    private void ChangeScene()
    {
        if (landmarkType == LandmarkType.RunNGun)
        {
            sceneController.ChangeScene("Level1_Run");
        }
        else if(landmarkType == LandmarkType.Boss)
        {
            sceneController.ChangeScene("Level1_Boss");
        }
        
    }
}
