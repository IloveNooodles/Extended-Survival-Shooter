using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowUI : MonoBehaviour
{
    public Sprite[] badgeArray;
    public Image badge;
    public TMP_Text playerName;
    public TMP_Text score;

    public void ChangeBadgeFirstRank()
    {
        badge.GetComponent<Image>().overrideSprite = badgeArray[0];
    }

    public void ChangeBadgeSecondRank()
    {
        badge.GetComponent<Image>().overrideSprite = badgeArray[1];
    }

    public void ChangeBadgeThirdRank()
    {
        badge.GetComponent<Image>().overrideSprite = badgeArray[2];
    }
}
