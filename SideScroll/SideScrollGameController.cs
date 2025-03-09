using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideScrollGameController : MonoBehaviour
{
    [Header("Side Scroll Player Properties")]
    [SerializeField] private PlayerSideScrollStateController sidescrollPlayer;

    [Header("Side Scroll Game Properties")]
    [SerializeField] Transform startPos;
    [SerializeField] Transform goalPos;
    [SerializeField] BossStateController boss;
    private float timer;
    public int minute;
    public int second;
    public string currentTime;
    public int coinCounter;
    private void Update()
    {
        if(sidescrollPlayer.isDead == false && sidescrollPlayer.isWin == false)
        {
            CountTime();
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
        float result;
        // Find World distance between player's pos and goal's pos
        float totalDistance = Vector2.Distance(startPos.position, goalPos.position);
        float currentDistance = Vector2.Distance(sidescrollPlayer.transform.position, startPos.position);
        float worldDistantPercentage = (currentDistance / totalDistance) * 100;
        result = (worldDistantPercentage/100) * distanceUI.rectTransform.sizeDelta.x;
        return result;
    }
    public float CheckBossProgression(Image distanceUI)
    {
        float result;
        // Find percentage of boss hp and convert it into a progress bar result
        float bossCurrentHealth = boss.currentBossHP;
        float healthPercentage = (bossCurrentHealth / boss.bossMaxHP) * 100;
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
