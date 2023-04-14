using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour, IDataPersistence
{
    private static TimerManager instance;
    public static float time = 0;
    public static bool isActive;

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
            isActive = true;
        }
        
        text = GetComponent<Text>();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ContinueGame()
    {
        Time.timeScale = 1;
    }
    
    public static void StopTimer()
    {
         isActive = false;
    }

    public static void StartTimer()
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
