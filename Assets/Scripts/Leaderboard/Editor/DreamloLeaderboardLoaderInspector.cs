using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DreamloLeaderboardLoader))]
public class DreamloLeaderboardLoaderInspector : Editor
{
    private List<LeaderboardEntry> _scores = new List<LeaderboardEntry>();
    private string _errorMessage;
    private string _testName;
    private int _testScore;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Get your keys at http://dreamlo.com"))
        {
            Application.OpenURL("http://dreamlo.com");
        }

        if (serializedObject.targetObject is DreamloLeaderboardLoader leaderboardLoader)
        {
            if (!string.IsNullOrEmpty(leaderboardLoader.publicKey) && !string.IsNullOrEmpty(leaderboardLoader.privateKey))
            {
                if (GUILayout.Button("Manage your leaderboard online"))
                {
                    Application.OpenURL($"http://dreamlo.com/lb/{leaderboardLoader.privateKey}");
                }
            
                if (GUILayout.Button("Load Scores"))
                {
    
                    _errorMessage = null;
                    _scores.Clear();
                    leaderboardLoader.LoadScores(OnScoresLoaded, OnLoadError);
                }
    
                if (!string.IsNullOrEmpty(_errorMessage))
                {
                    GUIStyle errorStyle = new GUIStyle();
                    errorStyle.normal.textColor = Color.red;
                    GUILayout.Label(_errorMessage, errorStyle);
                }
    
                if (_scores.Count > 0)
                {
                    for (int i = 0; i < _scores.Count; i++)
                    {
                        LeaderboardEntry score = _scores[i];
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label($"#{(i + 1)}) {score.Score} {score.ID} {score.Time} {score.Name} {score.Date}");
                        if (GUILayout.Button("x"))
                        {
                            leaderboardLoader.DeleteScore(score.ID, OnScoresLoaded, OnLoadError);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                
                GUILayout.Label("Add Test Score");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Name: ");
                _testName = GUILayout.TextField(_testName);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Score: ");
                _testScore = int.Parse(GUILayout.TextField(_testScore.ToString()));
                GUILayout.EndHorizontal();
                if (GUILayout.Button("Submit"))
                {
                    leaderboardLoader.SubmitScore(new LeaderboardEntry {ID = _testName, Score =  _testScore}, OnScoresLoaded, OnLoadError);
                }
            
            }
        }
    }

    private void OnScoresLoaded(List<LeaderboardEntry> scores)
    {
        _scores = scores;
        if (_scores.Count == 0)
        {
            _errorMessage = "No scores yet posted.";
        }
    }

    private void OnLoadError(string message)
    {
        _errorMessage = message;
    }
}
