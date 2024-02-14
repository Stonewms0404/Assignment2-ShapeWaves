using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathMenu : MonoBehaviour
{
    public static event Action<bool> _GameLost;

    public enum GameType { LIVES, TIMED };
    [SerializeField] private GameType gameMode;
    private int currentScore;
    private int currentTimeSurvived;

    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private LevelLoader levelLoader;

    private void OnEnable()
    {
        Scoreboard._GameOver += PlayerLostLives;
        Scoreboard._GameOverTimer += PlayerLostTimer;
    }
    private void OnDisable()
    {
        Scoreboard._GameOver -= PlayerLostLives;
        Scoreboard._GameOverTimer -= PlayerLostTimer;
    }

    private void Start()
    {
        deathMenuUI.SetActive(false);

        if (gameMode == GameType.LIVES)
        {
            levelLoader.SetSceneType(LevelLoader.SceneType.LIVES);
        }
        else if (gameMode == GameType.TIMED)
        {
            levelLoader.SetSceneType(LevelLoader.SceneType.TIMED);
        }
    }

    public void PlayerLostLives(int playerScore)
    {
        _GameLost(true);
        deathMenuUI.SetActive(true);
        currentScore = playerScore;
    }
    public void PlayerLostTimer(int playerScore, int timeSurvived)
    {
        _GameLost(true);
        deathMenuUI.SetActive(true);
        currentScore = playerScore;
        currentTimeSurvived = timeSurvived;
    }

    public void LoadMenu()
    {
        levelLoader.GoToScene(0);
    }

    public void Reload()
    {
        switch(gameMode)
        {
            case GameType.LIVES:
                levelLoader.GoToScene(1);
                break;
            case GameType.TIMED:
                levelLoader.GoToScene(2);
                break;
            default:
                levelLoader.GoToScene(0);
                break;
        }
        Time.timeScale = 1f;
    }
}
