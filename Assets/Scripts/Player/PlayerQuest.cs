using System;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] private QuestGiver questGiver;
    public Quest quest;

    private void Start()
    { 
        questGiver.SetNewQuest(0);
        quest = questGiver.GiveQuestToUser();
        questGiver.UpdateQuestWindow(quest);
    }
    
    public void Track(int id)
    {
        bool status = false;
        int size = quest.questGoal.Length;
        for (int i = 0; i < size; i++)
        {
            quest.questGoal[i].TrackQuest(id);
            status = status && quest.questGoal[i].GetQuestStatus();
        }
        
        questGiver.UpdateQuestWindow(quest);

        if (status)
        {
            CompleteQuest();
        }
        
    }
    
    private void CompleteQuest()
    {
        QuestManager.CompletedQuest += 1;
        quest.isActive = false;
        
        /* Pindah scene ada button save apa gak */
        questGiver.SetNewQuest(QuestManager.CompletedQuest);
    }
}
