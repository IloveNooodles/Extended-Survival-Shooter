using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManagerLevel2 : MonoBehaviour
{

    public GameObject player;
    public GameObject playerCamera;
    public GameObject enemyManager;
    public GameObject HUD;
    public GameObject[] enemies;
    public GameObject popupQuest;
    public StartingCutScene startingCutScene;
    public GameObject petManager;

    private void Start()
    {
        StartFirstCutScene();
    }

    public void startCutScene()
    {
        player.SetActive(false);
        playerCamera.SetActive(false);
        enemyManager.SetActive(false);
        petManager.SetActive(false);
        HUD.SetActive(false);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(false);
        }
    }

    public void endCutScene(){
        player.SetActive(true);
        playerCamera.SetActive(true);
        enemyManager.SetActive(true);
        HUD.SetActive(true);
        petManager.SetActive(true);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(true);
        }
    }

    public void StartFirstCutScene(){
        popupQuest.SetActive(false);
        startingCutScene.StartCutScene();
        StartCoroutine(EndCutScene());
    }

    IEnumerator EndCutScene(){
        yield return new WaitForSeconds(30f);
        startingCutScene.EndCutScene();
    }
}
