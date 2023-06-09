﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour, IDataPersistence
{
    private static ScoreManager instance;
    public static int score = 0;
    
    Text text;
    void Awake ()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        text = GetComponent <Text> ();
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }

    public void LoadData(GameData data)
    {
        score = data.score;
    }

    public void SaveData(ref GameData data)
    {
        data.score = score;
    }
}
