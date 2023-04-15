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

    public void ContinueQuest()
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
        if(currentScene == 2){
            player.SetActive(true);
            player.transform.position = new Vector3(-14.5f,0,-18.7999992f);
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            FPSMovement fpsMovement = player.GetComponent<FPSMovement>();
            movement.enabled = false;
            fpsMovement.enabled = true;

            GameObject gunBarrelEnd = player.transform.GetChild(1).gameObject;
            GameObject gun = player.transform.GetChild(2).gameObject;
            GameObject weaponManager = player.transform.GetChild(4).gameObject;
            gunBarrelEnd.SetActive(true);
            gun.SetActive(true);
            weaponManager.SetActive(false);
        }
        SceneManager.LoadScene(currentScene + 1);
    }
}
