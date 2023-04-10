using System;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    private Quest firstQuest;
    private Quest secondQuest;
    private Quest thridQuest;
    private Quest fourthQuest;

    private void Awake()
    {
        firstQuest = new Quest();
    }
}
