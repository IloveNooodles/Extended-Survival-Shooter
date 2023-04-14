using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
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

    static public void addGold(int gold)
    {
        int newGold = GoldManager.Gold + gold;
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
}
