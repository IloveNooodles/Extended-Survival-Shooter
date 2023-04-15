using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        void Awake()
        {
                string toScoreboard = PlayerPrefs.GetString("toScoreboard", "false");

                if (toScoreboard == "true")
                {
                        GameObject scoreboard = GameObject.FindWithTag("Scoreboard");
                        ScoreboardManager scoreboardManager = scoreboard.GetComponent<ScoreboardManager>();
                        scoreboardManager.AddScore(new Score(PlayerPrefs.GetString("PlayerName", "Player"), (int)PlayerPrefs.GetFloat("Time", 0f)));
                        scoreboard.SetActive(true);
                }

                PlayerPrefs.DeleteKey("toScoreboard");
                PlayerPrefs.DeleteKey("Time");
        }

        public void NewGame()
        {
                DataPersistenceManager.instance.NewGame();
                SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {
                DataPersistenceManager.instance.LoadGame();
        }

        public void SaveGame()
        {
                DataPersistenceManager.instance.NewGame();
                DataPersistenceManager.instance.SaveGame();
        }

        public void Exit()
        {
                Application.Quit();
        }
}