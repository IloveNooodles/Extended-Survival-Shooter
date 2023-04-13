using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossEndCutScene : MonoBehaviour
{
    public CutSceneManager cutSceneManager;
    public GameObject[] houses;
    public GameObject boss;
    public GameObject player;
    bool isCutScenePlaying = false;

    public GameObject noArmCutScene;
    public GameObject noLeftArmCutScene;
    public GameObject noRightArmCutScene;
    public GameObject bothArmsCutScene;
    PlayableDirector endingCutScene;

    // Start is called before the first frame update
    void Start()
    {
        noArmCutScene.SetActive(false);
        noLeftArmCutScene.SetActive(false);
        noRightArmCutScene.SetActive(false);
        bothArmsCutScene.SetActive(false);
    }
    public void startCutScene(){
        cutSceneManager.startCutScene();
        foreach(GameObject house in houses){
            house.SetActive(false);
        }
        boss.SetActive(false);
        player.SetActive(false);
        if(TitanHealth.leftArmHealth <= 0 && TitanHealth.rightArmHealth <= 0){
            noArmCutScene.SetActive(true);
            endingCutScene = noArmCutScene.GetComponent<PlayableDirector>();
            endingCutScene.Play();
        }else if(TitanHealth.leftArmHealth <= 0){
            noLeftArmCutScene.SetActive(true);
            endingCutScene = noLeftArmCutScene.GetComponent<PlayableDirector>();
            endingCutScene.Play();
        }else if(TitanHealth.rightArmHealth <= 0){
            noRightArmCutScene.SetActive(true);
            endingCutScene = noRightArmCutScene.GetComponent<PlayableDirector>();
            endingCutScene.Play();
        }else{
            bothArmsCutScene.SetActive(true);
            endingCutScene = bothArmsCutScene.GetComponent<PlayableDirector>();
            endingCutScene.Play();
        }
    }

    public void endCutScene(){
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}
