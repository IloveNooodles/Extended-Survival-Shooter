using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManagerLevel2 : MonoBehaviour
{

    private GameObject player;
    private GameObject playerCamera;
    public GameObject enemyManager;
    private GameObject HUD;
    public GameObject[] enemies;
    public GameObject popupQuest;
    public StartingCutScene startingCutScene;
    public PUBGToHouseCutscene pubgToHouseCutscene;
    public EnterHouse enterHouseCutscene;
    public GameObject petManager;

    private ShopKeeperManager shopKeeper;

    private int prevBuildIndex;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        shopKeeper = GameObject.FindGameObjectWithTag("ShopKeeperSpawner").GetComponent<ShopKeeperManager>();
        prevBuildIndex = PlayerPrefs.GetInt("lastScene");

        if (prevBuildIndex != 4)
        {
            StartFirstCutScene();
        }
        else
        {
            shopKeeper.Spawn();
            StartEnterHouseCutScene();
        }
        
        // delete player prefs
        PlayerPrefs.DeleteKey("lastScene");
    }

    public void startCutScene()
    {
        player.SetActive(false);
        enemyManager.SetActive(false);
        petManager.SetActive(false);
        HUD.SetActive(false);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(false);
        }
        playerCamera.SetActive(false);
    }

    public void endCutScene(){
        playerCamera.SetActive(true);
        player.SetActive(true);
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
        yield return new WaitForSeconds(0f);
        // yield return new WaitForSeconds(30f);
        startingCutScene.EndCutScene();
    }

    public void StartPUBGToHouseCutScene(){
        popupQuest.SetActive(false);
        pubgToHouseCutscene.StartCutScene();
        StartCoroutine(EndPUBGToHouseCutScene());
    }

    IEnumerator EndPUBGToHouseCutScene(){
        yield return new WaitForSeconds(0f);
        // yield return new WaitForSeconds(10f);
        pubgToHouseCutscene.EndCutScene();
        shopKeeper.Spawn();
        TimerManager.StopTimer();
    }

    public void StartEnterHouseCutScene(){
        popupQuest.SetActive(false);
        enterHouseCutscene.StartCutScene();
        StartCoroutine(EndEnterHouseCutScene());
    }

    IEnumerator EndEnterHouseCutScene()
    {
        yield return new WaitForSeconds(0f);
        // yield return new WaitForSeconds(10f);
        enterHouseCutscene.EndCutScene();
    }
}
