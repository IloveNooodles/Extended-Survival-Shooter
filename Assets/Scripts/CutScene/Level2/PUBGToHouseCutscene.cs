using System;
using UnityEngine;
using UnityEngine.Playables;

public class PUBGToHouseCutscene: MonoBehaviour
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
        playableDirector.Play();
    }

    public void EndCutScene()
    {
        gameObject.SetActive(false);
        cutSceneManager.endCutScene();
    }
}