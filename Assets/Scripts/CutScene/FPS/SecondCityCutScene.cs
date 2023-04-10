using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SecondCityCutScene : MonoBehaviour
{
    public CutSceneManager cutSceneManager;
    PlayableDirector playableDirector;
    GameObject cutSceneClip;
    public GameObject wizards;
    public GameObject groundEnemy;
    bool isWizardSpawned = false;

    void FixedUpdate(){
        if(isWizardSpawned){
            if(wizards.transform.position.y >= 6){
                wizards.transform.position -= new Vector3(0, 10f, 0) * Time.deltaTime;
            }
        }
    }

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
        StartCoroutine(spawnEnemy());
    }

    public void endCutScene(){
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }

    IEnumerator spawnEnemy(){
        yield return new WaitForSeconds(13f);
        wizards.SetActive(true);
        isWizardSpawned = true;
        yield return new WaitForSeconds(8f);
        groundEnemy.SetActive(true);
    }
}
