using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class EnterHouse : MonoBehaviour
{
    public CutSceneManagerLevel2 cutSceneManager;
    public PlayableDirector playableDirector;
    GameObject cutSceneCamera;
    GameObject player;
    public PlayerQuest PlayerQuest;
    private void Awake()
    {
        cutSceneCamera = transform.GetChild(0).gameObject;
        player = transform.GetChild(1).gameObject;
        player.SetActive(false);
        cutSceneCamera.SetActive(false);
        playableDirector = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(PlayerQuest.isFirstSceneEndingCutScenePlayed){
                cutSceneManager.StartEnterHouseCutScene();
            }
        }
    }

    public void StartCutScene()
    {
        cutSceneManager.startCutScene();
        gameObject.SetActive(true);
        cutSceneCamera.SetActive(true);
        player.SetActive(true);
        playableDirector.Play();
    }

    public void EndCutScene()
    {
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
        PlayerQuest.CompleteQuest();
    }
}
