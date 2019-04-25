using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameObject gameRoot;

    private void Awake()
    {
        if (Instance != null)
        {
            throw new Exception("Creating second instance of singleton GameStateManager");
        }

        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.ShowScreen<SplashScreen>();
        Time.timeScale = 0f;
        gameRoot.SetActive(false);
    }

    // When the game has begun.
    public void StartGame()
    {
        UIManager.Instance.ShowScreen<MainGameScreen>();
        Time.timeScale = 1f;
        gameRoot.SetActive(true);
    }

    // When the player has paused the game.
    public void PauseGame()
    {
        UIManager.Instance.ShowScreen<PauseScreen>();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        UIManager.Instance.ShowScreen<MainGameScreen>();
        Time.timeScale = 1f;
    }

    // When the game has been quit prematurely.
    public void QuitGame()
    {
        UIManager.Instance.ShowScreen<TitleScreen>();
        Time.timeScale = 0f;
        gameRoot.SetActive(false);
    }
    
    // When the game has ended via game over, winning, etc.
    public void EndGame(int score)
    {
        UIManager.Instance.ShowScreen(new LeaderboardInputScreenParams {Score = score});
        Time.timeScale = 0f;
        gameRoot.SetActive(false);
    }
}
