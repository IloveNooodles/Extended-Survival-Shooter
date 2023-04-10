using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static float time;
    private bool isActive;

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        time = 0;
        isActive = true;
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
}
