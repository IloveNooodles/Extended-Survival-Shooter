using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static int Quest;
    [SerializeField] private Text text;

    private void Awake()
    {
        Quest = 0;
    }

    private void Update()
    {
        text.text = $"Quest: ({Quest}/4)";
    }
}
