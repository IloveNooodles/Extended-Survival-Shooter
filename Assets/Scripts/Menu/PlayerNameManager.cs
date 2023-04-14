using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameText;

    void Start()
    {
        LoadPlayerName();
    }

    void Update()
    {
        SavePlayerName();
    }

    public void SavePlayerName()
    {
        PlayerPrefs.SetString("PlayerName", playerNameText.text);
    }
    public void LoadPlayerName()
    {
        playerNameText.text = PlayerPrefs.GetString("PlayerName", "Player");
    }
}
