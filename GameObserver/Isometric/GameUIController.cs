using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour, IGameObserver
{
    [Header("Level Type")]
    [SerializeField] private LevelType levelType;

    [Header("Observer References")]
    [SerializeField] private GameSubject gameControllerSubject; // Game controller subject
    [SerializeField] private GameSubject gameSubject; // Game subject
    [SerializeField] private GameSubject goalSubject; // Game subject

    [Header("Game Controller Reference")]
    [SerializeField] private SideScrollGameController sidescrollGameController;

    [Header("Enemy Spawn Controller Reference")]
    [SerializeField] private NormalEnemySpawnerController enemySpawnController;
    [Space]
    [Space]
    [Header("Windows UI References")]
    [Header("Upgrade Window")]
    [SerializeField] private GameObject upgradeWindow; // Player upgrade window
    [Header("Scoreboard Window")]
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private GameObject winScoreBoard;
    [SerializeField] private GameObject loseScoreBoard;
    [SerializeField] private Image scoreBoardDistantUI;
    [SerializeField] private Image currentProgreesionIcon;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text gradeText;
    [Space]
    [Header("Pause Window")]
    [SerializeField] private GameObject pauseWindow; // Pause window
    private void OnEnable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameSubject.AddGameObserver(this);
        }
        else if(levelType == LevelType.RunNGunLevel)
        {
            gameSubject.AddGameObserver(this);
            gameSubject.AddSideScrollGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            goalSubject.AddSideScrollGameObserver(this);
        }
        else
        {
            gameSubject.AddGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            gameSubject.AddSideScrollGameObserver(this);
        }
    }
    private void OnDisable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameSubject.RemoveGameObserver(this);
            gameControllerSubject.RemoveGameObserver(this);
        }
        else if (levelType == LevelType.RunNGunLevel)
        {
            gameSubject.RemoveGameObserver(this);
            gameSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
            goalSubject.RemoveSideScrollGameObserver(this);
        }
        else
        {
            gameSubject.RemoveGameObserver(this);
            gameSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                upgradeWindow.SetActive(false);
                return;
            case (IsometricGameState.Paused):
                Debug.Log("Game Paused UI");
                return;
            case (IsometricGameState.Upgrade):
                Debug.Log("Upgrade state from GameObserver");
                upgradeWindow.SetActive(true);
                return;
        }
    }
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState)
    {
        switch (sidescrollGameState)
        {
            case(SideScrollGameState.Play):
                pauseWindow.SetActive(false);
                Time.timeScale = 1;
                return;
            case (SideScrollGameState.Paused):
                pauseWindow.SetActive(true);
                pauseWindow.GetComponent<PauseWindow>().DisplayCurrentStatus(levelType);
                Time.timeScale = 0;
                return;
            case (SideScrollGameState.Win):
                // Show win scoreboard
                scoreBoard.SetActive(true);
                winScoreBoard.SetActive(true);
                loseScoreBoard.SetActive(false);
                timerText.text = sidescrollGameController.currentTime;
                hpText.text = sidescrollGameController.UpdatePlayerHPCount().ToString();
                gradeText.text = sidescrollGameController.Result();
                if(levelType == LevelType.RunNGunLevel)
                {
                    enemySpawnController.enabled = false;
                }
                return;
            case (SideScrollGameState.Lose):
                // show lose scoreboard
                scoreBoard.SetActive(true);
                winScoreBoard.SetActive(false);
                if(levelType == LevelType.RunNGunLevel)
                {
                    currentProgreesionIcon.rectTransform.anchoredPosition = new Vector2(sidescrollGameController.CheckGoalDistant(scoreBoardDistantUI),
                        currentProgreesionIcon.rectTransform.anchoredPosition.y);
                    enemySpawnController.enabled = false;
                }
                else if(levelType == LevelType.BossLevel)
                {
                    currentProgreesionIcon.rectTransform.anchoredPosition = new Vector2(sidescrollGameController.CheckBossProgression(scoreBoardDistantUI),
                        currentProgreesionIcon.rectTransform.anchoredPosition.y);
                }
                loseScoreBoard.SetActive(true);
                return;
        }
    }
}
