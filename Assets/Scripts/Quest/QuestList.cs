using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    private static QuestList instance;
    private static List<Quest> questList = new List<Quest>();

    public void Awake()
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
        InitQuestList();
    }
    
    public void Init(){}

    public void InitQuestList() {

        /* Init quest title, description and reward */
        const string firstQuestTitle = "Kalahkan musuh!";
        const string firstQuestDescription =
            "Kamu ditugaskan untuk membantu warga dalam membasmi zombie-zombie yang membunuh warga!";
        const int firstQuestReward = 300;
        const string secondQuestTitle = "Bertahan!";
        const string secondQuestDescription =
            "Saat kamu mengejar orang misterius, banyak zombie muncul menyerang kamu! Bertahanlah dan jangan mati!";
        const int secondQuestReward = 200;
        const string thirdQuestTitle = "Kejar orang misterius";
        const string thirdQuestDescription = "Kejar orang misterius itu dan ungkap semua rahasianya!";
        const int thirdQuestReward = 300;
        const string fourthQuestTitle = "Kalahkan Eren";
        const string fourthQuestDescription = "Kalahkan eren dan bantu manusia bangkit kembali!";
        const int fourthQuestReward = 1000;

        string bullet = ItemName.Bullet;
        string gold = ItemName.Gold;
        string eren = ItemName.Eren;
        
        /* First Quest */
        string[] firstTarget = {EnemyName.Zombunny, EnemyName.Zombear, EnemyName.Hellepant};
        List<QuestGoal> list = new List<QuestGoal>();
        for (int i = 0; i < firstTarget.Length; i++)
        {
            string target = firstTarget[i];
            KillQuestGoal killQuestGoal = new KillQuestGoal(EnemyName.GetEnemyId(target), target, 3);
            list.Add(killQuestGoal);
        }
        
        // SpendQuestGoal spentQuestGoal = new SpendQuestGoal(ItemName.ItemId(bullet), bullet, 5);
        // list.Add(spentQuestGoal);

        Quest addedQuest = new Quest(firstQuestTitle,
            firstQuestDescription, firstQuestReward,
            list.ToArray());
        
        questList.Add(addedQuest);
        
        /* Second Quest */
        list = new List<QuestGoal>();
        SpendQuestGoal spentQuestGoal = new SpendQuestGoal( ItemName.ItemId(gold), gold, 300);
        
        list.Add(spentQuestGoal);
        addedQuest = new Quest(secondQuestTitle,
            secondQuestDescription, secondQuestReward, list.ToArray());
        
        questList.Add(addedQuest);
        
        // /* Third Quest */
        // list = new List<QuestGoal>();
        // string[] thirdTarget = { EnemyName.Wizard, EnemyName.Zombear };
        // for (int i = 0; i < thirdTarget.Length; i++)
        // {
        //     string target = thirdTarget[i];
        //     KillQuestGoal killQuestGoal = new KillQuestGoal(EnemyName.GetEnemyId(target), target, 5);
        //     list.Add(killQuestGoal);
        // }
        //
        // spentQuestGoal = new SpendQuestGoal(ItemName.ItemId(gold), gold, 25);
        // list.Add(spentQuestGoal);
        
        /* Third Quest */
        list = new List<QuestGoal>();
        spentQuestGoal = new SpendQuestGoal(ItemName.ItemId(eren), eren, 1);
        list.Add(spentQuestGoal);
        

        addedQuest = new Quest(thirdQuestTitle, thirdQuestDescription, thirdQuestReward, list.ToArray());
        questList.Add(addedQuest);

        /* Final Quest */
        list = new List<QuestGoal>();
        KillQuestGoal kq = new KillQuestGoal(EnemyName.GetEnemyId(EnemyName.Titan), EnemyName.Titan, 1);
        list.Add(kq);

        addedQuest = new Quest(fourthQuestTitle, fourthQuestDescription, fourthQuestReward, list.ToArray());
        questList.Add(addedQuest);
    }

    public Quest GetQuestByIndex(int index)
    {
        if (index >= questList.Count) return new Quest();
        return questList[index];
    }
}
