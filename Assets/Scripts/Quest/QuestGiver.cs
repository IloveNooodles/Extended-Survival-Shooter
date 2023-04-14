using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    private static QuestGiver instance;
    [SerializeField] private QuestList questList;
    [SerializeField] private Quest activeQuest = new Quest();
    private GameObject questWindow;
    private TMP_Text title;
    private TMP_Text goal;
    private TMP_Text reward;
    private TMP_Text description;

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
        questWindow = GameObject.FindGameObjectWithTag("QuestWindow");
        TMP_Text[] a = questWindow.GetComponentsInChildren<TMP_Text>();
        title = a[0];
        goal = a[3];
        description = a[1];
        reward = a[2];
    }

    public void SetNewQuest(int index)
    {
        activeQuest = questList.GetQuestByIndex(index);
    }

    public void UpdateQuestWindow()
    {
        if (!questWindow) return;
        Quest quest = this.activeQuest;
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
