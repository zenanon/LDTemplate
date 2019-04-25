using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public class DreamloLeaderboardLoader : MonoBehaviour
{
    public string publicKey;
    public string privateKey;
    
    public const string DreamloWebserviceUrl = "http://dreamlo.com/lb/";
   
    public delegate void OnSuccessDelegate(List<LeaderboardEntry> scores);

    public delegate void OnErrorDelegate(string errorMessage);

    /**
     * Asynchronously submit a leaderboard entry and load the high score table.
     */
    public void SubmitScore(LeaderboardEntry entry, OnSuccessDelegate onScoreSubmitted, OnErrorDelegate onError)
    {
        if (!IsSetupValid())
        {
            onError?.Invoke("Dreamlo Leaderboard must be configured with a public and private key from http://dreamlo.com");
        }
        StartCoroutine(DoSubmitScore(entry, onScoreSubmitted, onError));
    }

    /**
     * Load the high score table.
     */
    public void LoadScores(OnSuccessDelegate onScoresLoaded, OnErrorDelegate onError)
    {
        StartCoroutine(DoLoadScores(onScoresLoaded, onError));
    }

    /**
     * Delete a score by ID.
     */
    public void DeleteScore(string scoreID, OnSuccessDelegate onScoresLoaded, OnErrorDelegate onError)
    {
        StartCoroutine(DoDeleteScore(scoreID, onScoresLoaded, onError));
    }

    private bool IsSetupValid()
    {
        return !string.IsNullOrEmpty(publicKey) && !string.IsNullOrEmpty(privateKey);
    }

    private IEnumerator DoSubmitScore(LeaderboardEntry entry, OnSuccessDelegate onScoreSubmitted,
        OnErrorDelegate onError)
    {
        string requestUrl =
            $"{DreamloWebserviceUrl}{privateKey}/add-pipe/{UnityWebRequest.EscapeURL(entry.ID)}/{entry.Score}/{entry.Time}/{UnityWebRequest.EscapeURL(entry.Name)}";
        
        UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl);
        yield return webRequest.SendWebRequest();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            onError?.Invoke(webRequest.error);
        }
        else
        {
            onScoreSubmitted?.Invoke(ParseLeaderboardEntries(webRequest.downloadHandler.text));
        }
    }

    private IEnumerator DoDeleteScore(string scoreID, OnSuccessDelegate onScoreRemoved, OnErrorDelegate onError)
    {
        string requestUrl = $"{DreamloWebserviceUrl}{privateKey}/delete/{scoreID}";
        UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl);
        yield return webRequest.SendWebRequest();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            onError?.Invoke(webRequest.error);
        }
        else
        {
            yield return DoLoadScores(onScoreRemoved, onError);
        }
    }
    
    private IEnumerator DoLoadScores(OnSuccessDelegate onScoreSubmitted, OnErrorDelegate onError)
    {
        string requestUrl = $"{DreamloWebserviceUrl}{publicKey}/pipe";
        
        UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl);
        yield return webRequest.SendWebRequest();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            onError?.Invoke(webRequest.error);
        }
        else
        {
            onScoreSubmitted?.Invoke(ParseLeaderboardEntries(webRequest.downloadHandler.text));
        }
    }

    private List<LeaderboardEntry> ParseLeaderboardEntries(string response)
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        if (!string.IsNullOrEmpty(response))
        {
            string[] rows = response.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
            int rowcount = rows.Length;

            if (rowcount <= 0) return null;

            for (int i = 0; i < rowcount; i++)
            {
                string[] values = rows[i].Split(new char[] {'|'}, System.StringSplitOptions.None);

                LeaderboardEntry current = new LeaderboardEntry
                {
                    ID = UnityWebRequest.UnEscapeURL(values[0]),
                    Score = 0,
                    Time = 0,
                    Name = "",
                    Date = "",
                    Position = i + 1
                };
                
                if (values.Length > 1)
                {
                    current.Score = int.Parse(values[1]);
                }

                if (values.Length > 2)
                {
                    current.Time = int.Parse(values[2]);
                }

                if (values.Length > 3)
                {
                    current.Name = UnityWebRequest.UnEscapeURL(values[3]);
                }

                if (values.Length > 4)
                {
                    current.Date = values[4];
                }
                entries.Add(current);
            }
        }
        
        return entries;
    }
}
