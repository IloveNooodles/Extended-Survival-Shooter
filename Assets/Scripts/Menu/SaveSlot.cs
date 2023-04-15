using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId;
    [SerializeField] private bool isPreventClick;

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TMP_Text saveNameText;
    [SerializeField] private TMP_Text saveDateText;

    public void SetData(GameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);

            if (isPreventClick)
            {
                GetComponent<Button>().interactable = false;
            }
            else 
            {
                GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            saveNameText.text = data.saveName;
            saveDateText.text = data.lastSavedDate;

            GetComponent<Button>().interactable = true;
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }
}
