using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideScrollGameController : GameSubject
{
    [Header("Side Scroll Player Properties")]
    [SerializeField] private PlayerSideScrollStateController sidescrollPlayer;

    [Header("Side Scroll Game Properties")]
    // Run n Gun mode
    [SerializeField] Transform startPos; // Start pos in the world
    [SerializeField] Transform goalPos; // Goal pos in the world
    // Boss mode
    [SerializeField] BossList bossName;
    [SerializeField] BossHealth bossHP;

    [HideInInspector] public bool isPaused = false;
    private GameObject bossController;
    private float timer;
    private int minute;
    private int second;
    public string currentTime;
    private int coinCounter;
    private void Update()
    {
        if(sidescrollPlayer.isDead == false && sidescrollPlayer.isWin == false)
        {
            CountTime();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            NotifySideScrollGameObserver(SideScrollGameState.Paused);
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            NotifySideScrollGameObserver(SideScrollGameState.Play);
            isPaused = false;
        }
    }
    public void AddCoin()
    {
        coinCounter++;
    }
    public int UpdatePlayerHPCount()
    {
        int healthCounter;
        healthCounter = sidescrollPlayer.playerCurrentHP;
        return healthCounter;
    }
    public float CheckGoalDistant(Image distanceUI)
    {
        // Find World distance Percentage between player's pos and goal's pos
        float result;
        float totalDistance = Vector2.Distance(startPos.position, goalPos.position); // World total distance
        float currentDistance = Vector2.Distance(sidescrollPlayer.transform.position, startPos.position); // World current distance
        float worldDistantPercentage = (currentDistance / totalDistance) * 100; // Convert the world distance values to percentage
        result = (worldDistantPercentage/100) * distanceUI.rectTransform.sizeDelta.x; // Using the worldDistantPercentage to calculate the percentage of UI progress bar width
        return result; // Output the position value as a result
    }
    public float CheckBossProgression(Image distanceUI)
    {
        // Find percentage of boss hp and convert it into a progress bar result
        float result;
        float bossCurrentHealth = bossHP.currentBossHP;
        float healthPercentage = (bossCurrentHealth / bossHP.bossMaxHP) * 100;
        result = (healthPercentage / 100) * distanceUI.rectTransform.sizeDelta.x;
        return distanceUI.rectTransform.sizeDelta.x - result;
    }
    private void CountTime()
    {
        timer += Time.deltaTime;
        minute = Mathf.FloorToInt(timer / 60);
        second = Mathf.FloorToInt(timer - (minute * 60));
        currentTime = string.Format("{0:00}:{1:00}", minute, second); // Timer format
    }
    public string Result()
    {
        // To Do: requirement are different depending on type of gamemode
        string grade;
        if(sidescrollPlayer.playerCurrentHP >= 3 && minute <= 1)
        {
            grade = "A";
        }
        else
        {
            grade = "B";
        }
        return grade;
    }
}
