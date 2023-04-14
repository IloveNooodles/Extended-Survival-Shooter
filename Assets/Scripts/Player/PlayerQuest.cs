﻿using System;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    private QuestGiver questGiver;
    private QuestList questList;
    public Quest quest;
    private GameObject PopupModal;
    static public bool isFirstSceneEndingCutScenePlayed = false;

    private void Awake()
    {
        PopupModal = GameObject.FindGameObjectWithTag("QuestComplete");
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();
        questList = GameObject.FindGameObjectWithTag("QuestList").GetComponent<QuestList>();
        questList.InitQuestList();
        questGiver.SetNewQuest(0);
        quest = questGiver.GiveQuestToUser();
        questGiver.UpdateQuestWindow();
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
        
        questGiver.UpdateQuestWindow();
        
        if (status && quest.isActive)
        {
            CompleteQuest();
        }
    }
    
    public void CompleteQuest()
    {
        Debug.Log("Quest Completed");
        QuestManager.CompletedQuest += 1;
        quest.isActive = false;

        TimerManager.StopTimer();
        /**/

        if(QuestManager.CompletedQuest == 1 && !isFirstSceneEndingCutScenePlayed){
            QuestManager.CompletedQuest -= 1;
            isFirstSceneEndingCutScenePlayed = true;
            GameObject.Find("CutSceneManager").GetComponent<CutSceneManagerLevel2>().StartPUBGToHouseCutScene();
            return;
        }
        
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
