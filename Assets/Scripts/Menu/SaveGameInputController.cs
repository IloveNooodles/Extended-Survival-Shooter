using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveGameInputController : MonoBehaviour
{
    [SerializeField] private TMP_InputField saveGameText;

    void Update()
    {
        SaveGameName();
    }

    public void SaveGameName()
    {
        PlayerPrefs.SetString("SaveGame", saveGameText.text);
    }
}
