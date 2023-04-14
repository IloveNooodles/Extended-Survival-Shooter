using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Level1CutScene : MonoBehaviour
{
    public GameObject player;
    public GameObject mainCamera;
    public GameObject enemyManager;
    public GameObject petManager;
    public GameObject HUD;
    public GameObject[] enemies;

    PlayableDirector cutScene;
    // Start is called before the first frame update
    void Start()
    {
        //Disable all GameObjects
        player.SetActive(false);
        mainCamera.SetActive(false);
        enemyManager.SetActive(false);
        petManager.SetActive(false);
        HUD.SetActive(false);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(false);
        }

        //Play CutScene
        cutScene = GetComponent<PlayableDirector>();
        cutScene.Play();
        StartCoroutine(endCutScene());
    }

    IEnumerator endCutScene(){
        yield return new WaitForSeconds(21);
        player.SetActive(true);
        mainCamera.SetActive(true);
        enemyManager.SetActive(true);
        HUD.SetActive(true);
        petManager.SetActive(true);
        foreach(GameObject enemy in enemies){
            enemy.SetActive(true);
        }
        Destroy(gameObject);
    }
}
