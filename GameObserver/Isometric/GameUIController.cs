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
    [SerializeField] private GameSubject gameUIControllerSubject; // Game UI Controller subject
    [SerializeField] private GameSubject goalSubject; // Run n Gun destination subject
    [SerializeField] private GameSubject sideScrollIntroSubject; // Side scroll intro/outro subject

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
    [Space]
    [Header("Side Scroll Outro Window")]
    [SerializeField] private SideScrollIntro sideScrollIntroWindow;

    public NPCDialogController currentNPC; // Current NPC the player is talking to, Upgrade Window need to ref this variable to update player upgrade status
    private void OnEnable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameUIControllerSubject.AddGameObserver(this);
            //gameControllerSubject.AddGameObserver(this);
        }
        else if(levelType == LevelType.RunNGunLevel)
        {
            gameUIControllerSubject.AddGameObserver(this);
            gameUIControllerSubject.AddSideScrollGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            goalSubject.AddSideScrollGameObserver(this);
            sideScrollIntroSubject.AddSideScrollGameObserver(this);
        }
        else
        {
            gameUIControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddGameObserver(this);
            gameControllerSubject.AddSideScrollGameObserver(this);
            gameUIControllerSubject.AddSideScrollGameObserver(this);
            sideScrollIntroSubject.AddSideScrollGameObserver(this);
        }
    }
    private void OnDisable()
    {
        if (levelType == LevelType.IsoLevel)
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            //gameControllerSubject.RemoveGameObserver(this);
        }
        else if (levelType == LevelType.RunNGunLevel)
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            gameUIControllerSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
            goalSubject.RemoveSideScrollGameObserver(this);
            sideScrollIntroSubject.RemoveSideScrollGameObserver(this);
        }
        else
        {
            gameUIControllerSubject.RemoveGameObserver(this);
            gameUIControllerSubject.RemoveSideScrollGameObserver(this);
            gameControllerSubject.RemoveSideScrollGameObserver(this);
            sideScrollIntroSubject.RemoveSideScrollGameObserver(this);
        }
    }
    public void OnGameNotify(IsometricGameState isoGameState)
    {
        switch (isoGameState)
        {
            case (IsometricGameState.Play):
                currentNPC = null;
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
                Time.timeScale = 1; // To Do: Find a better way to pause the game
                return;
            case (SideScrollGameState.StartRound):
                return;
            case (SideScrollGameState.EndRound):
                return;
            case (SideScrollGameState.Paused):
                pauseWindow.SetActive(true);
                pauseWindow.GetComponent<PauseWindow>().DisplayCurrentStatus(levelType);
                Time.timeScale = 0; // To Do: Find a better way to pause the game
                return;
            case (SideScrollGameState.Win):
                Debug.Log("Test");
                if(levelType == LevelType.BossLevel)
                {
                    StartCoroutine(BossKnockOut());
                }
                else if (levelType == LevelType.RunNGunLevel)
                {
                    StartCoroutine(ReachGoal());
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
    private IEnumerator ReachGoal()
    {
        // Show win scoreboard
        enemySpawnController.enabled = false;
        scoreBoard.SetActive(true);
        winScoreBoard.SetActive(true);
        loseScoreBoard.SetActive(false);
        timerText.text = sidescrollGameController.currentTime;
        hpText.text = sidescrollGameController.UpdatePlayerHPCount().ToString();
        gradeText.text = sidescrollGameController.Result();
        yield return null;
    }
    private IEnumerator BossKnockOut()
    {
        sideScrollIntroWindow.knouckOutGroup.SetActive(true);
        StartCoroutine(sideScrollIntroWindow.StartKnockOutWipeTransition());
        yield return new WaitUntil(() => sideScrollIntroWindow.finishCoroutine == true);
        // Show win scoreboard
        scoreBoard.SetActive(true);
        winScoreBoard.SetActive(true);
        loseScoreBoard.SetActive(false);
        timerText.text = sidescrollGameController.currentTime;
        hpText.text = sidescrollGameController.UpdatePlayerHPCount().ToString();
        gradeText.text = sidescrollGameController.Result();
        yield return null;
    }
}
