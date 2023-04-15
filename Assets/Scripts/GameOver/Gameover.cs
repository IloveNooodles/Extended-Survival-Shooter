using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class Gameover : MonoBehaviour
{
    public static Gameover instance;
    public static bool isGameOver = false;
    public bool isActive = false;
    private float currentTime;
    public float delaySecond = 5f;
    public TMP_Text text;


    private void destoryAllInstance()
    {
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
 
        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
    }
    
    private void Awake()
    {
        instance = this;
        CloseGameOverScreen();
    }

    public void RestartGame()
    {
        isGameOver = false;
        CloseGameOverScreen();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
    }

    public void CloseGameOverScreen()
    {
        gameObject.SetActive(false);
    }

    public void GoBackToMenu()
    {
        isGameOver = false;
        CloseGameOverScreen();
        destoryAllInstance();
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = delaySecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            ShowGameOverScreen();
            currentTime = currentTime - Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            text.text = time.Seconds.ToString();
            if (currentTime < 0)
            {
                GoBackToMenu();
            }
        }
    }
}
