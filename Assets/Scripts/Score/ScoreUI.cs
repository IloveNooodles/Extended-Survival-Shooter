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
        scoreboardManager.AddScore(new Score("Player 1", 100));
        scoreboardManager.AddScore(new Score("Player 3", 400));
        scoreboardManager.AddScore(new Score("Player 5", 10340));
        scoreboardManager.AddScore(new Score("Aakwoawk", 100));


        print(scoreboardManager.GetHighScores());
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
