using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreboardManager scoreboardManager;

    void Start()
    {
        // HOW TO ADD SCORES
        // scoreboardManager.AddScore(new Score("Player 1", 100));

        var scores = scoreboardManager.GetHighScores().ToArray();
        
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.playerName.text = scores[i].playerName;
            row.score.text = scores[i].score.ToString();

            if (i == 0)
                row.ChangeBadgeFirstRank();
            else if (i == 1)
                row.ChangeBadgeSecondRank();
            else if (i == 2)
                row.ChangeBadgeThirdRank();
        }
    }
}
