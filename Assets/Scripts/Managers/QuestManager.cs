using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static int CompletedQuest; 
    [SerializeField] private QuestGiver _questGiver;
    [SerializeField] private Text text;

    private void Awake()
    {
        CompletedQuest = 0;
        _questGiver.UpdateQuest();
    }

    private void Update()
    {
        text.text = $"Quest: ({CompletedQuest}/4)";
    }
}
