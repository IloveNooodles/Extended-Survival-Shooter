using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SecondCityCutScene : MonoBehaviour
{
    public CutSceneManager cutSceneManager;
    PlayableDirector playableDirector;
    GameObject cutSceneClip;

    void Start(){
        cutSceneClip = gameObject.transform.GetChild(0).gameObject;
        playableDirector = cutSceneClip.GetComponent<PlayableDirector>();
        cutSceneClip.SetActive(false);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            cutSceneManager.startSecondCityCutScene();
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
