using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class StartingCutScene : MonoBehaviour
{
    public CutSceneManagerLevel2 cutSceneManager;
    public PlayableDirector playableDirector;
    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        gameObject.SetActive(false);
    }

    public void StartCutScene()
    {
        cutSceneManager.startCutScene();
        gameObject.SetActive(true);
        Debug.Log(gameObject);
        playableDirector.Play();
    }

    public void EndCutScene()
    {
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}
