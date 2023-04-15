using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour, IDataPersistence
{
    private static GoldManager _instance;
    public static int Gold = 0;
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
        }
        text = GetComponent <Text> ();
    }

    private void Update()
    {
        text.text = "Gold: " + Gold;
    }

    static public void addGold(int gold)
    {
        int newGold = Gold + gold;
        if(newGold > Int16.MaxValue)
        {
            Gold = Int16.MaxValue;
        }
        else
        {
            Gold = newGold;
        }
        Debug.Log("Gold: " + Gold);
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
