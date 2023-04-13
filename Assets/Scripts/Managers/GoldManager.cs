using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    private static GoldManager _instance;
    public static int Gold;
    [SerializeField] private Text text;

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
            Gold = 0;
        }
    }

    private void Update()
    {
        text.text = "Gold: " + Gold;
    }
}
