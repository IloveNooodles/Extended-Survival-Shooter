using System;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    private QuestGiver questGiver;
    private QuestList questList;
    public Quest quest;
    private GameObject PopupModal;

    private void Awake()
    {
        PopupModal = GameObject.FindGameObjectWithTag("QuestComplete");
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();
        questList = GameObject.FindGameObjectWithTag("QuestList").GetComponent<QuestList>();
        questList.InitQuestList();
        questGiver.SetNewQuest(0);
        quest = questGiver.GiveQuestToUser();
        questGiver.UpdateQuestWindow(quest);
    }
    
    public void Continue()
    {
        PopupModal.SetActive(false);
        TimerManager.ContinueGame();
        
    }
    
    public void Track(GoalType goalType, int id, int amount)
    {
        bool status = true;
        int size = quest.questGoal.Length;
        for (int i = 0; i < size; i++)
        {
            quest.questGoal[i].IsCompleted();
            quest.questGoal[i].TrackQuest(goalType, id, amount);
            status = status && quest.questGoal[i].GetQuestStatus();
        }
        
        questGiver.UpdateQuestWindow(quest);
        
        if (status && quest.isActive)
        {
            CompleteQuest();
        }
    }
    
    private void CompleteQuest()
    {
        QuestManager.CompletedQuest += 1;
        quest.isActive = false;

        TimerManager.StopTimer();
        /**/
        
        /* Shows popup modal */
        PopupModal.SetActive(true);
        
        /* Freeze Game */
        TimerManager.PauseGame();
        questGiver.SetNewQuest(QuestManager.CompletedQuest);

        /* Reward */
        GoldManager.addGold(quest.goldReward);
        /**/
        
        /* Pindah scene ada button save apa gak */
    }

}
