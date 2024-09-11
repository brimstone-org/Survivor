using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{

    //enum for the state machine of the game states
    public enum GameState
    {
        Gameplay,
        GameOver
    }

    public GameState state;

    public bool isGameOver = false;

    //references
    [Inject]
    private PlayerStats playerStats;
    [Inject]
    private UIManager uiManager; 

    //stopwatch logic
    float stopwatchTime; //time elapsed for gameplay

    //powerups
    [SerializeField]
    private List<PowerUpSO> powerUps;



    private void Update()
    {
        switch (state)
        {
            case GameState.Gameplay:
                UpdateStopwatch();
                
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Debug.Log("Game over!!!!");
                    DisplayEndgameScreen();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// change the current state
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(GameState newState)
    {
        state = newState;
    }


    /// <summary>
    /// called upon player death
    /// </summary>
    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }


    /// <summary>
    /// called at the end of the game to show the end game stats
    /// </summary>
    private void DisplayEndgameScreen()
    {
        uiManager.DisplayEndgameScreen(playerStats, stopwatchTime);
    }

    /// <summary>
    /// loads main menu scene
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// called in update during gameplay
    /// </summary>
    private void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;
    }

    /// <summary>
    /// called when current experience surpasses the current level cap and applies a random power up
    /// </summary>
    public void LevelUp()
    {
        PowerUpSO newPowerUp = powerUps[Random.Range(0, powerUps.Count)];
        switch (newPowerUp.TypeOfPowerUp)
        {
            case PowerUpSO.PowerUpType.MaxHealth:
                playerStats.ApplyPowerUp(newPowerUp);
                uiManager.ShowLevelUpScreen("Max health increased by " + newPowerUp.Amount);
                break;
            case PowerUpSO.PowerUpType.Speed:
                playerStats.ApplyPowerUp(newPowerUp);
                uiManager.ShowLevelUpScreen("Speed increased by " + newPowerUp.Amount);
                break;
            case PowerUpSO.PowerUpType.Damage:
                playerStats.weaponController.ApplyPowerUp(newPowerUp);
                uiManager.ShowLevelUpScreen("Weapon damage increased by " + newPowerUp.Amount);
                break;
            case PowerUpSO.PowerUpType.FireRate:
                playerStats.weaponController.ApplyPowerUp(newPowerUp);
                uiManager.ShowLevelUpScreen("Fire reate increased by " + newPowerUp.Amount);
                break;
            case PowerUpSO.PowerUpType.PoisonDamage:
                playerStats.weaponController.ApplyPowerUp(newPowerUp);
                uiManager.ShowLevelUpScreen("Enemies now take poison damage");
                break;
            default:
                break;
        }
       
    }
}
