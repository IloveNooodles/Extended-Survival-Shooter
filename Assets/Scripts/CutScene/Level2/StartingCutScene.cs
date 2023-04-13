using System;
using UnityEngine;
using UnityEngine.Playables;

public class StartingCutScene : MonoBehaviour
{
    public CutSceneManagerLevel2 cutSceneManager;
    public PlayableDirector playableDirector;
    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        gameObject.SetActive(false);
    }

    public void StartCutScene()
    {
        cutSceneManager.startCutScene();
        gameObject.SetActive(true);
        playableDirector.Play();
    }

    public void EndCutScene()
    {
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}
