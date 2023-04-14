using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level1CutScene : MonoBehaviour
{
    private GameObject player;
    public GameObject mainCamera;
    public GameObject enemyManager;
    public GameObject EnemyFactory;
    private GameObject HUD;
    private GameObject popupQuest;
    public GameObject[] enemies;

    PlayableDirector cutScene;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        popupQuest = GameObject.FindGameObjectWithTag("QuestComplete");
    }

    void Start()
    {
        //Disable all GameObjects
        player.SetActive(false);
        mainCamera.SetActive(false);
        EnemyFactory.SetActive(false);
        enemyManager.SetActive(false);
        HUD.SetActive(false);
        popupQuest.SetActive(false);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(false);
        }

        //Play CutScene
        cutScene = GetComponent<PlayableDirector>();
        cutScene.Play();
        StartCoroutine(endCutScene());
    }

    IEnumerator endCutScene(){
        yield return new WaitForSeconds(21);
        player.SetActive(true);
        mainCamera.SetActive(true);
        enemyManager.SetActive(true);
        EnemyFactory.SetActive(true);
        HUD.SetActive(true);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(true);
        }
        Destroy(gameObject);
    }
}
