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
    public StartingCutScene startingCutScene;

    private void Start()
    {
        StartFirstCutScene();
    }

    public void startCutScene()
    {
        player.SetActive(false);
        playerCamera.SetActive(true);
        enemyManager.SetActive(false);
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
        foreach(GameObject enemy in enemies){
            enemy.SetActive(true);
        }
    }

    public void StartFirstCutScene(){
        startingCutScene.StartCutScene();
        StartCoroutine(EndCutScene());
    }

    IEnumerator EndCutScene(){
        yield return new WaitForSeconds(30f);
        startingCutScene.EndCutScene();
    }
}
