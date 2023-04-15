using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{

    GameObject player;
    FPSMovement fpsMovement;
    FPSShooting fpsShooting;
    GameObject HUD;
    public Camera playerCamera;
    public GameObject popupQuest;
    private PlayerQuest playerQuest;
    public FirstCityCutScene firstCityCutScene;
    public SecondCityCutScene secondCityCutScene;
    public ThirdCityCutScene thirdCityCutScene;
    public BossSpawnCutScene bossSpawnCutScene;
    public BossEndCutScene bossEndCutScene;
    public GameObject petManager;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerQuest = player.GetComponent<PlayerQuest>();
        fpsMovement = player.GetComponent<FPSMovement>();
        popupQuest = GameObject.FindGameObjectWithTag("QuestComplete");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        fpsShooting = player.GetComponentInChildren<FPSShooting>();
    }

    public void startCutScene()
    {
        fpsMovement.enabled = false;
        fpsShooting.enabled = false;
        playerCamera.enabled = false;
        petManager.SetActive(false);
        HUD.SetActive(false);
        popupQuest.SetActive(false);
    }

    public void endCutScene()
    {
        fpsMovement.enabled = true;
        fpsShooting.enabled = true;
        playerCamera.enabled = true;
        HUD.SetActive(true);
        petManager.SetActive(true);
    }

    public void startFirstCityCutScene()
    {
        firstCityCutScene.startCutScene();
        GunShooting gunShooting = player.GetComponentInChildren<GunShooting>();
        gunShooting.enabled = false;
        StartCoroutine(endFirstCityCutScene());
    }

    IEnumerator endFirstCityCutScene()
    {
        yield return new WaitForSeconds(5f);
        // yield return new WaitForSeconds(18.85f);
        playerQuest.UpdateSelfQuest();
        TimerManager.StartTimer();
        firstCityCutScene.endCutScene();
    }

    public void startSecondCityCutScene()
    {
        secondCityCutScene.startCutScene();
        StartCoroutine(endSecondCityCutScene());
    }

    IEnumerator endSecondCityCutScene()
    {
        yield return new WaitForSeconds(0f);
        // yield return new WaitForSeconds(21.5f);
        secondCityCutScene.endCutScene();
    }

    public void startThirdCityCutScene()
    {
        thirdCityCutScene.startCutScene();
        StartCoroutine(endThirdCityCutScene());
    }

    IEnumerator endThirdCityCutScene()
    {
        yield return new WaitForSeconds(0);
        // yield return new WaitForSeconds(17);
        playerQuest.Track(GoalType.Spend, ItemName.ItemId(ItemName.Eren), 1);
        thirdCityCutScene.endCutScene();
    }

    public void startBossSpawnCutScene()
    {
        bossSpawnCutScene.startCutScene();
        StartCoroutine(endBossSpawnCutScene());
    }

    IEnumerator endBossSpawnCutScene()
    {
        yield return new WaitForSeconds(0);
        // yield return new WaitForSeconds(15);
        playerQuest.UpdateQuestBoard();
        bossSpawnCutScene.endCutScene();
    }

    public void startBossEndCutScene()
    {
        bossEndCutScene.startCutScene();
        StartCoroutine(endBossEndCutScene());
    }

    IEnumerator endBossEndCutScene()
    {
        yield return new WaitForSeconds(0f);
        // yield return new WaitForSeconds(24.5f);
        playerQuest.Track(GoalType.Spend, ItemName.ItemId(ItemName.Eren), 1);
        bossEndCutScene.endCutScene();
        SceneManager.LoadScene(5);
    }
}
