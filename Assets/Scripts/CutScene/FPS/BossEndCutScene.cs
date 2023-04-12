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

    GameObject cutSceneClip;
    PlayableDirector playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        cutSceneClip = gameObject.transform.GetChild(0).gameObject;
        playableDirector = cutSceneClip.GetComponent<PlayableDirector>();
        cutSceneClip.SetActive(false);
    }
    public void startCutScene(){
        cutSceneManager.startCutScene();
        foreach(GameObject house in houses){
            house.SetActive(false);
        }
        boss.SetActive(false);
        player.SetActive(false);
        cutSceneClip.SetActive(true);
        playableDirector.Play();
    }

    public void endCutScene(){
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}
