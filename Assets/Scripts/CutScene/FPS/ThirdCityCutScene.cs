using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThirdCityCutScene : MonoBehaviour
{
    public CutSceneManager cutSceneManager;
    PlayableDirector playableDirector;
    GameObject cutSceneClip;
    public GameObject wizards;

    void Start(){
        cutSceneClip = gameObject.transform.GetChild(0).gameObject;
        playableDirector = cutSceneClip.GetComponent<PlayableDirector>();
        cutSceneClip.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            wizards.SetActive(false);
            cutSceneManager.startThirdCityCutScene();
        }
    }

    public void startCutScene(){
        cutSceneManager.startCutScene();
        cutSceneClip.SetActive(true);
        playableDirector.Play();
    }

    public void endCutScene(){
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}
