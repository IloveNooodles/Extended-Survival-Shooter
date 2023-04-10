using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static int CompletedQuest;
    [SerializeField] private Text text;

    private void Awake()
    {
        CompletedQuest = 0;
    }

    private void Update()
    {
        text.text = $"Quest: ({CompletedQuest}/4)";
    }
}
