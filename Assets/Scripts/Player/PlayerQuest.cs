using System;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    [SerializeField] private QuestGiver questGiver;
    public Quest quest;

    private void Awake()
    { 
        quest = questGiver.GiveQuestToUser();
    }

    public void Track()
    {
        int size = quest.questGoal.Length;
        for (int i = 0; i < size; i++)
        {
            quest.questGoal[i].TrackQuest();
        }
    }
    
    public void Track(int id)
    {
        int size = quest.questGoal.Length;
        for (int i = 0; i < size; i++)
        {
            quest.questGoal[i].TrackQuest(id);
        }
    }
    
    private void CompleteQuest()
    {
        QuestManager.CompletedQuest += 1;
        quest.isActive = false;
    }
    
}
