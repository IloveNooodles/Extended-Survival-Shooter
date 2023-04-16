using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        GameObject scoreboard;
        GameObject mainMenu;
        ScoreboardManager scoreboardManager;

        void Awake()
        {
                mainMenu = GameObject.FindWithTag("MainMenu");
                scoreboard = GameObject.FindWithTag("Scoreboard");
                scoreboard.SetActive(false);
        
                string toScoreboard = PlayerPrefs.GetString("toScoreboard", "false");

                if (toScoreboard == "true")
                {
                        mainMenu.SetActive(false);
                        scoreboard.SetActive(true);
                        scoreboardManager = scoreboard.GetComponentInChildren<ScoreboardManager>();
                        scoreboardManager.AddScore(new Score(PlayerPrefs.GetString("PlayerName", "Player"), (int)PlayerPrefs.GetFloat("Time", 0f)));
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