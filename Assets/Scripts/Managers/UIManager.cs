using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


/// <summary>
/// handles all UI changes
/// </summary>
public class UIManager : MonoBehaviour
{

    [Inject]
    PlayerStats playerStats;
    [Inject]
    GameManager gameManager;

    //ingame Stats
    [SerializeField]
    private Slider levelBar;
    [SerializeField]
    private Text level;
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Text kills;
    [SerializeField]
    private Text bulletsCount;
    [SerializeField]
    private Text powerUpMessage;
    

    //Screens
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject levelUpScreen;
   

    //gameover stats
    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Text enemiesKilled;
    [SerializeField]
    private Text timeSurvived;
    [SerializeField]
    private Text levelReached;


    public void Start()
    {
        UpdateKillCount(0);
        UpdateBulletCount(playerStats.weaponController.CurrentNumberOfProjectiles);
        UpdateHealthBar(1);
        UpdateExpBar(0);
        UpdateLevel(1);
    }

    private bool CheckForGameOver()
    {
        return gameManager.isGameOver;
    }


    /// <summary>
    /// updates UI for kills
    /// </summary>
    /// <param name="count"></param>
    public void UpdateKillCount(int count)
    {
        if (CheckForGameOver())
        {
            return;
        }
        kills.text = count.ToString();
    }


    /// <summary>
    /// updates UI for bullets
    /// </summary>
    /// <param name="count"></param>
    public void UpdateBulletCount(int value)
    {
        if (CheckForGameOver())
        {
            return;
        }
        bulletsCount.text = value.ToString();
    }

    /// <summary>
    /// updates UI for health
    /// </summary>
    /// <param name="count"></param>
    public void UpdateHealthBar(float value)
    {
        if (CheckForGameOver())
        {
            return;
        }
        healthBar.value = value;
    }

    /// <summary>
    /// updates UI for exp
    /// </summary>
    /// <param name="count"></param>
    public void UpdateExpBar(float value)
    {
        if (CheckForGameOver())
        {
            return;
        }
        levelBar.value = value;
    }
    /// <summary>
    /// updates the ingame level count
    /// </summary>
    /// <param name="newLevel"></param>
    public void UpdateLevel(int newLevel)
    {
        if (CheckForGameOver())
        {
            return;
        }
        level.text = "Lv. " + newLevel;
    }

    /// <summary>
    /// displays stats screen at the end of the game
    /// </summary>
    /// <param name="playerStats"></param>
    /// <param name="timeElapsed"></param>
    public void DisplayEndgameScreen(PlayerStats playerStats, float timeElapsed)
    {
        playerName.text = playerStats.playerData.CharacterName;
        enemiesKilled.text = playerStats.enemiesKilled.ToString();
        levelReached.text = playerStats.level.ToString();
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        timeSurvived.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        gameOverScreen.SetActive(true);

    }


    /// <summary>
    /// shows the level up power up info text
    /// </summary>
    /// <param name="message"></param>
    public void ShowLevelUpScreen(string message)
    {
        if (CheckForGameOver())
        {
            return;
        }
        powerUpMessage.text = message;
        levelUpScreen.GetComponent<Animator>().SetTrigger("Show");
        
    }
}
