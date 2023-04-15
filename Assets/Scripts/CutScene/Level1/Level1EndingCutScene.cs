using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level1EndingCutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject enemyManager;
    public GameObject EnemyFactory;
    private GameObject HUD;
    private GameObject popupQuest;
    public GameObject petManager;
    public GameObject[] enemies;
    public PlayerQuest playerQuest;
    PlayableDirector cutScene;

    GameObject cutSceneCamera;
    GameObject playerObject;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        popupQuest = GameObject.FindGameObjectWithTag("QuestComplete");

        cutSceneCamera = transform.GetChild(0).gameObject;
        playerObject = transform.GetChild(1).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.tag == "Player")
        {
            if (PlayerQuest.isSecondSceneEndingCutScenePlayed == true)
            {
                StartCutScene();
            }
        }
    }

    void StartCutScene()
    {
        //Disable all GameObjects
        player.SetActive(false);
        mainCamera.SetActive(false);
        EnemyFactory.SetActive(false);
        enemyManager.SetActive(false);
        HUD.SetActive(false);
        petManager.SetActive(false);
        popupQuest.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        //Play CutScene
        cutSceneCamera.SetActive(true);
        playerObject.SetActive(true);
        cutScene = GetComponent<PlayableDirector>();
        cutScene.Play();
        StartCoroutine(endCutScene());
    }

    IEnumerator endCutScene()
    {
        yield return new WaitForSeconds(5f);
        HUD.SetActive(true);
        playerQuest.CompleteQuest();
        yield return new WaitForSeconds(1f);
    }
}
