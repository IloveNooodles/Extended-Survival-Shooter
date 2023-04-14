using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour, IDataPersistence
{
    private static GoldManager _instance;
    public static int Gold;
    private Text text;

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
        text = GetComponent <Text> ();
    }

    private void Update()
    {
        text.text = "Gold: " + Gold;
    }

    public void LoadData(GameData data)
    {
        Gold = data.gold;
    }

    public void SaveData(ref GameData data)
    {
        data.gold = Gold;
    }
}
