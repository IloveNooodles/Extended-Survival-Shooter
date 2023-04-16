using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level1CutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject enemyManager;
    public GameObject EnemyFactory;
    private GameObject HUD;
    private GameObject popupQuest;
    public GameObject petManager;
    public GameObject[] enemies;
    private QuestGiver questGiver;

    PlayableDirector cutScene;
    private int prevBuildIndex;

    private ShopKeeperManager shopKeeper;

    public Level1EndingCutScene endingCutScene; 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HUD = GameObject.FindGameObjectWithTag("HUD");
        popupQuest = GameObject.FindGameObjectWithTag("QuestComplete");
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Disable all GameObjects
        prevBuildIndex = PlayerPrefs.GetInt("lastScene");
        if (prevBuildIndex != 4)
        {
            
            player.SetActive(false);
            mainCamera.SetActive(false);
            EnemyFactory.SetActive(false);
            enemyManager.SetActive(false);
            HUD.SetActive(false);
            petManager.SetActive(false);
            popupQuest.SetActive(false);
            foreach(GameObject enemy in enemies){
                enemy.SetActive(false);
            }

            //Play CutScene
            cutScene = GetComponent<PlayableDirector>();
            cutScene.Play();
            StartCoroutine(endCutScene());
        }
        else
        {
            popupQuest.SetActive(false);
            questGiver.UpdateQuestWindow();
            shopKeeper = GameObject.FindGameObjectWithTag("ShopKeeperSpawner").GetComponent<ShopKeeperManager>();
            shopKeeper.Spawn();
            
            endingCutScene.StartCutScene();
            Destroy(gameObject);
        }
        PlayerPrefs.DeleteKey("lastScene");
    }

    IEnumerator endCutScene(){
        yield return new WaitForSeconds(21f);
        player.SetActive(true);
        mainCamera.SetActive(true);
        enemyManager.SetActive(true);
        EnemyFactory.SetActive(true);
        petManager.SetActive(true);
        HUD.SetActive(true);
        TimerManager.StartTimer();
        foreach(GameObject enemy in enemies){
            enemy.SetActive(true);
        }
        questGiver.UpdateQuestWindow();
        Destroy(gameObject);
    }
}
