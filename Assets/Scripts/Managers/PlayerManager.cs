using System;
using UnityEngine;
using TMPro;    

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    private string playerName = "Player";
    public TMP_InputField playerNameText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerNameText.onValueChanged.AddListener(delegate { SetName(playerNameText.text); });
    }

    private void Update()
    {
        SetName(playerNameText.text);
    }

    private void SetName(string newName)
    {
        playerName = newName;
        PlayerPrefs.SetString("PlayerName", playerName);
    }

    public void LoadData(GameData data)
    {
        this.playerName = data.name;
    }

    public void SaveData(ref GameData data)
    {
        data.name = this.playerName;
    }
    
}
