using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    private ScoreData scoreData;

    void Awake()
    {
        var scoreboardData = PlayerPrefs.GetString("scoreboardData", "{}");
        scoreData = JsonUtility.FromJson<ScoreData>(scoreboardData);
    }

    public IEnumerable<Score> GetHighScores()
    {
        return scoreData.scores.OrderBy(s => s.score);
    }

    public void AddScore(Score score)
    {
        scoreData.scores.Add(score);
        SaveScore();
    }

    public void SaveScore()
    {
        var scoreboardData = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString("scoreboardData", scoreboardData);
    }
}
