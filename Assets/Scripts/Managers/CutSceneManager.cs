using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{

    public GameObject player;
    FPSMovement fpsMovement;
    FPSShooting fpsShooting;
    public Camera playerCamera;
    public FirstCityCutScene firstCityCutScene;
    public SecondCityCutScene secondCityCutScene;
    public ThirdCityCutScene thirdCityCutScene;
    public BossSpawnCutScene bossSpawnCutScene;
    public BossEndCutScene bossEndCutScene;

    void Start(){
        fpsMovement = player.GetComponent<FPSMovement>();
        fpsShooting = player.GetComponentInChildren<FPSShooting>();
    }

    public void startCutScene(){
        fpsMovement.enabled = false;
        fpsShooting.enabled = false;
        playerCamera.enabled = false;
    }

    public void endCutScene(){
        fpsMovement.enabled = true;
        fpsShooting.enabled = true;
        playerCamera.enabled = true;
    }

    public void startFirstCityCutScene(){
        firstCityCutScene.startCutScene();
        StartCoroutine(endFirstCityCutScene());
    }

    IEnumerator endFirstCityCutScene(){
        yield return new WaitForSeconds(18.85f);
        firstCityCutScene.endCutScene();
    }

    public void startSecondCityCutScene(){
        secondCityCutScene.startCutScene();
        StartCoroutine(endSecondCityCutScene());
    }

    IEnumerator endSecondCityCutScene(){
        yield return new WaitForSeconds(21.5f);
        secondCityCutScene.endCutScene();
    }

    public void startThirdCityCutScene(){
        thirdCityCutScene.startCutScene();
        StartCoroutine(endThirdCityCutScene());
    }

    IEnumerator endThirdCityCutScene(){
        yield return new WaitForSeconds(17);
        thirdCityCutScene.endCutScene();
    }

    public void startBossSpawnCutScene(){
        bossSpawnCutScene.startCutScene();
        StartCoroutine(endBossSpawnCutScene());
    }

    IEnumerator endBossSpawnCutScene(){
        yield return new WaitForSeconds(15);
        bossSpawnCutScene.endCutScene();
    }

    public void startBossEndCutScene(){
        bossEndCutScene.startCutScene();
        StartCoroutine(endBossEndCutScene());
    }

    IEnumerator endBossEndCutScene(){
        yield return new WaitForSeconds(26.6f);
        bossEndCutScene.endCutScene();
    }
}
