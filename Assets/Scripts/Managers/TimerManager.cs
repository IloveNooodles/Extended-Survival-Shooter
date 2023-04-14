using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour, IDataPersistence
{
    private static TimerManager instance;
    public static float time;
    private bool isActive;

    private Text text;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            time = 0;
            isActive = true;
        }
        
        text = GetComponent<Text>();
    }

    public void stopTimer()
    {
        isActive = false;
    }

    public void startTimer()
    {
        isActive = true;
    }
    
    private void Update()
    {
        if(!isActive) return;
        time += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        text.text = "Time: " + timeSpan.ToString(@"mm\:ss\:fff");
    }

    public void LoadData(GameData data)
    {
        time = data.time;
    }

    public void SaveData(ref GameData data)
    {
        data.time = time;
    }
}
