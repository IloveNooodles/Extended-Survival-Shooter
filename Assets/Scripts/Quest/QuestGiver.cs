using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private PlayerQuest playerQuest;
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text goal;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private TMP_Text description;

    public void Awake()
    {
        playerQuest = GetComponent<PlayerQuest>();
    }

    public void UpdateQuest()
    {
        questWindow.SetActive(true);
        title.text =  $"Quest\n{quest.title}";
        description.text = $"Description\n{quest.description}";
        goal.text = $"Goal\n{quest.questGoal}";
        reward.text = $"Reward\n{quest.goldReward.ToString()} Gold";
    }

    public void SetQuest()
    {
        playerQuest.quest = quest;
    }
}
