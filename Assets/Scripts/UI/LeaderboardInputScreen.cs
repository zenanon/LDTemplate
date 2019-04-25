using System.Collections.Generic;
using TMPro;

public class LeaderboardInputScreen : UIScreen
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI scoreText;
    public DreamloLeaderboardLoader leaderboardLoader;

    private LeaderboardEntry _submittedEntry;
    private int _score;

    public override void Initialize<T>(UIScreenParams<T> screenParams)
    {
        if (screenParams is LeaderboardInputScreenParams param)
        {
            _score = param.Score;
        }

        scoreText.text = "Score: " + _score;
        errorText.gameObject.SetActive(false);
    }

    public void Submit()
    {
        _submittedEntry = new LeaderboardEntry
        {
            Name = nameInput.text,
            Score = _score
        };
        leaderboardLoader.SubmitScore(_submittedEntry, OnScoreSubmitted, OnError);
        errorText.gameObject.SetActive(false);
    }

    public void Quit()
    {
        GameStateManager.Instance.QuitGame();
    }

    private void OnScoreSubmitted(List<LeaderboardEntry> scores)
    {
        UIManager.Instance.ShowScreen(new LeaderboardResultsScreenParams
            {Scores = scores, PlayerEntry = _submittedEntry});
    }

    private void OnError(string errorMessage)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = errorMessage;
    }
}

public class LeaderboardInputScreenParams : UIScreenParams<LeaderboardInputScreen>
{
    public int Score;
}