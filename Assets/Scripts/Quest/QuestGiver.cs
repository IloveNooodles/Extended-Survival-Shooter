using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    private static QuestGiver instance;
    [SerializeField] private QuestList questList;
    [SerializeField] private Quest activeQuest = new Quest();
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text goal;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private TMP_Text description;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            instance = null;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetNewQuest(int index)
    {
        activeQuest = questList.GetQuestByIndex(index);
    }

    public void UpdateQuestWindow(Quest quest)
    {
        this.activeQuest = quest;
        questWindow.SetActive(true);
        title.text =  $"Quest: #{QuestManager.CompletedQuest + 1}" +
                      $"\n{quest.title}";
        description.text = $"\n{quest.description}";
        string goalText = "Goal\n";
        for (int i = 0; i < quest.questGoal.Length; i++)
        {
            goalText +=
                $"{quest.questGoal[i].objective} {quest.questGoal[i].currentAmount.ToString()}/{quest.questGoal[i].requiredAmount.ToString()}\n";
        }

        goal.text = goalText;
        reward.text = "Reward" +
                      $"\n{quest.goldReward.ToString()} Gold";
    }

    public Quest GiveQuestToUser(){
        activeQuest.isActive = true;
        return activeQuest;
    }
}
