using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    void Start()
    {
        LoadVolume();
    }

    void Update()
    {
        SaveVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", backgroundMusic.volume);
    }
    public void LoadVolume()
    {
        backgroundMusic.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
    }
}
