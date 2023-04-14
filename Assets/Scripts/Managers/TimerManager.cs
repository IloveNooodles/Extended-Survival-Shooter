using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    private static TimerManager instance;
    public static float time;
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
            time = 0;
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
}
