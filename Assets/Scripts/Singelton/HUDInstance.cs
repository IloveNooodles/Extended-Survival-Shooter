using System;
using UnityEngine;
public class HUDInstance : MonoBehaviour
{
    private static HUDInstance _instance;
    
    public static HUDInstance Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HUDInstance>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
