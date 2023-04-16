using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerQuest : MonoBehaviour
{
    private QuestGiver questGiver;
    private QuestList questList;
    private int questNumber;
    public Quest quest;
    private GameObject PopupModal;
    static public bool isFirstSceneEndingCutScenePlayed = false;
    static public bool isSecondSceneEndingCutScenePlayed = false;

    private void Awake()
    {
        PopupModal = GameObject.FindGameObjectWithTag("QuestComplete");
        questGiver = GameObject.FindGameObjectWithTag("QuestGiver").GetComponent<QuestGiver>();
        questList = GameObject.FindGameObjectWithTag("QuestList").GetComponent<QuestList>();
        questList.InitQuestList();
        questNumber = QuestManager.CompletedQuest;
        questGiver.SetNewQuest(questNumber);
        quest = questGiver.GiveQuestToUser();
        questGiver.UpdateQuestWindow();
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
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                if (quest.questGoal[i].currentAmount >= quest.questGoal[i].requiredAmount)
                {
                    ShopKeeperManager shopKeeper = GameObject.FindGameObjectWithTag("ShopKeeperSpawner")
                        .GetComponent<ShopKeeperManager>();
                    shopKeeper.Spawn();
                }
            }
        }

        questGiver.UpdateQuestWindow();

        if (status && quest.isActive)
        {
            CompleteQuest();
        }
    }


    private void Update()
    {
        /* Check if not same */
        if (questNumber != QuestManager.CompletedQuest)
        {
            questNumber = QuestManager.CompletedQuest;
            questGiver.SetNewQuest(QuestManager.CompletedQuest);
            quest = questGiver.GiveQuestToUser();
        }
    }

    public void UpdateSelfQuest()
    {
        quest = questGiver.GiveQuestToUser();
    }

    public void UpdateQuestBoard()
    {
        questGiver.UpdateQuestWindow();
    }



    public void CompleteQuest()
    {
        Debug.Log("Quest Completed");
        QuestManager.CompletedQuest += 1;
        questNumber = QuestManager.CompletedQuest;
        quest.isActive = false;

        if (QuestManager.CompletedQuest == 1 && !isFirstSceneEndingCutScenePlayed)
        {
            QuestManager.CompletedQuest -= 1;
            questNumber = QuestManager.CompletedQuest;
            isFirstSceneEndingCutScenePlayed = true;
            GameObject.Find("CutSceneManager").GetComponent<CutSceneManagerLevel2>().StartPUBGToHouseCutScene();
            /* Reward */
            GoldManager.addGold(quest.goldReward);
            return;
        }

        if (QuestManager.CompletedQuest == 2 && !isSecondSceneEndingCutScenePlayed)
        {
            QuestManager.CompletedQuest -= 1;
            questNumber = QuestManager.CompletedQuest;
            isSecondSceneEndingCutScenePlayed = true;
            /* Reward */
            GoldManager.addGold(quest.goldReward);
            return;
        }

        if (QuestManager.CompletedQuest < 3 || QuestManager.CompletedQuest == 4)
        {
            TimerManager.StopTimer();
        }

        /* Shows popup modal */
        try
        {
            if (QuestManager.CompletedQuest < 3)
            {
                PopupModal.SetActive(true);
            }
        }
        catch { }

        if (isFirstSceneEndingCutScenePlayed && isSecondSceneEndingCutScenePlayed)
        {
            /* Reward */
            GoldManager.addGold(quest.goldReward);
        }
        questGiver.SetNewQuest(QuestManager.CompletedQuest);
        quest = questGiver.GiveQuestToUser();

        /* Freeze Game */
        if (QuestManager.CompletedQuest < 3)
        {
            TimerManager.PauseGame();
        }
    }
}
