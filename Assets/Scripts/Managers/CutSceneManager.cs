using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{

    public GameObject player;
    public Camera playerCamera;
    public ThirdCityCutScene thirdCityCutScene;

    public void startCutScene(){
        player.SetActive(false);
        playerCamera.enabled = false;
    }

    public void endCutScene(){
        player.SetActive(true);
        playerCamera.enabled = true;
    }

    public void startThirdCityCutScene(){
        thirdCityCutScene.startCutScene();
        StartCoroutine(endThirdCityCutScene());
    }

    IEnumerator endThirdCityCutScene(){
        yield return new WaitForSeconds(17);
        thirdCityCutScene.endCutScene();
    }
}
