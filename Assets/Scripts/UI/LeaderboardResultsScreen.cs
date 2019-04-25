using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardResultsScreen : UIScreen
{
    public DreamloLeaderboardLoader dreamloLeaderBoard;
    public GameObject loadIndicator;
    public TextMeshProUGUI errorMessage;
    public LeaderboardAdapterView leaderboard;
    
    private LeaderboardEntry _highlightedEntry;
    private List<LeaderboardEntry> _preloadedScores;
    public override void Initialize<T>(UIScreenParams<T> screenParams)
    {
        if (screenParams is LeaderboardResultsScreenParams param)
        {
            _highlightedEntry = param.PlayerEntry;
            _preloadedScores = param.Scores;
        }
    }

    public override bool HandleInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Back();
        }
        return base.HandleInput();
    }

    public override void Show()
    {
        base.Show();
        errorMessage.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        if (_preloadedScores == null || _preloadedScores.Count == 0)
        {
            loadIndicator.gameObject.SetActive(true);
            dreamloLeaderBoard.LoadScores(ShowScores, ShowError);
        }
        else
        {
            loadIndicator.gameObject.SetActive(false);
            ShowScores(_preloadedScores);
        }
    }

    public void ShowScores(List<LeaderboardEntry> scores)
    {
        loadIndicator.gameObject.SetActive(false);
        if (scores.Count > 0)
        {
            leaderboard.gameObject.SetActive(true);
            leaderboard.BindDataList(scores);
            if (_highlightedEntry != null)
            {
                int playerScoreIndex = scores.FindIndex((score) => score.ID == _highlightedEntry.ID);
                ScrollRect scroller = leaderboard.GetComponentInChildren<ScrollRect>();
                scroller.verticalNormalizedPosition = 1f - (1f * playerScoreIndex) / scores.Count;
            }
        }
        else
        {
            errorMessage.gameObject.SetActive(true);
            errorMessage.text = "No scores yet posted.";
        }
    }

    public void ShowError(string message)
    {
        loadIndicator.gameObject.SetActive(false);
        leaderboard.gameObject.SetActive(false);
        errorMessage.gameObject.SetActive(true);
        errorMessage.text = $"Error: {message}";
    }

    public void Play()
    {
        GameStateManager.Instance.StartGame();
    }

    public void Back()
    {
        UIManager.Instance.ShowScreen<TitleScreen>();
    }
}

public class LeaderboardResultsScreenParams : UIScreenParams<LeaderboardResultsScreen>
{
    public LeaderboardEntry PlayerEntry;
    public List<LeaderboardEntry> Scores;
}
