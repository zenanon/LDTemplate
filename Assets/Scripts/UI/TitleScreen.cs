using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : UIScreen
{
    public void StartGame()
    {
        GameStateManager.Instance.StartGame();
    }

    public void ShowTutorial()
    {
        UIManager.Instance.ShowScreen<TutorialScreen>();
    }

    public void ShowLeaderboard()
    {
        UIManager.Instance.ShowScreen<LeaderboardResultsScreen>();
    }
}
