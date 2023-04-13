using System;
using UnityEngine;
public class HUDInstance : MonoBehaviour
{
    private static HUDInstance _instance;

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
