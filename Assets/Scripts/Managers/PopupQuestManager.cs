using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupQuestManager : MonoBehaviour
{
    private int currentScene;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SaveButton()
    {        
        player.transform.position = new Vector3(0, 0, 0);
        ChangeScene();
        TimerManager.ContinueGame();
        TimerManager.StopTimer();
    }
    

    public void UnsaveButton()
    {
        player.transform.position = new Vector3(0, 0, 0);
        ChangeScene();
        TimerManager.ContinueGame();
        TimerManager.StopTimer();
    }

    private void ChangeScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene + 1 > 3) return;
        SceneManager.LoadScene(currentScene + 1);
    }
}
