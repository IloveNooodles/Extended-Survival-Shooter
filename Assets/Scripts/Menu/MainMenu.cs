using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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