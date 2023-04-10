using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest activeQuest;
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text goal;
    [SerializeField] private TMP_Text reward;
    [SerializeField] private TMP_Text description;

    private void updateQuestWindow(Quest quest)
    {
        this.activeQuest = quest;
        questWindow.SetActive(true);
        title.text =  $"Quest: #{QuestManager.CompletedQuest + 1}" +
                      $"\n{quest.title}";
        description.text = $"\n{quest.description}";
        
        /* Create the array loop for the text */
        goal.text = "Goal" +
                    $"\n{quest.questGoal}";
        reward.text = "Reward" +
                      $"\n{quest.goldReward.ToString()} Gold";
    }

    public Quest GiveQuestToUser(){
        activeQuest.isActive = true;
        return activeQuest;
    }
}
