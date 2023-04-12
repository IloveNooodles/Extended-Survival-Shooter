using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    private static List<Quest> questList = new List<Quest>();

    public void Awake()
    {
        InitQuestList();
    }

    public void InitQuestList() {

        /* Init quest title, description and reward */
        const string firstQuestTitle = "Kalahkan musuh!";
        const string firstQuestDescription =
            "Ketika kamu ingin mengalahkan boss, kamu dihalangi oleh banyak zombie zombie aneh. Kalahkan mereka!";
        const int firstQuestReward = 200;
        const string secondQuestTitle = "Kumpulkan Harta";
        const string secondQuestDescription =
            "Kamu merasa bahwa untuk mengalahkan eren perlu persiapan yang banyak. Maka kamu berniat untuk mengumpulkan gold sebanyak mungkin untuk melawannya";
        const int secondQuestReward = 100;
        const string thirdQuestTitle = "Bunuh penyihir aneh itu";
        const string thirdQuestDescription = "Ketika kamu tiba di dareah yang tidak dikenali, kamu diserang oleh penyihir penyihir aneh bunuh mereka dan keluar dari situasi ini";
        const int thirdQuestReward = 300;
        const string fourthQuestTitle = "Kalahkan Eren";
        const string fourthQuestDescription = "Kalahkan eren dan bantu manusia bangkit kembali!";
        const int fourthQuestReward = 1000;

        string bullet = ItemName.Bullet;
        string gold = ItemName.Gold;
        
        /* First Quest */
        string[] firstTarget = {EnemyName.Zombunny, EnemyName.Hellepant};
        List<QuestGoal> list = new List<QuestGoal>();
        for (int i = 0; i < firstTarget.Length; i++)
        {
            string target = firstTarget[i];
            KillQuestGoal killQuestGoal = new KillQuestGoal(EnemyName.GetEnemyId(target), target, 5);
            list.Add(killQuestGoal);
        }
        
        SpendQuestGoal spentQuestGoal = new SpendQuestGoal(ItemName.ItemId(bullet), bullet, 100);
        list.Add(spentQuestGoal);

        Quest addedQuest = new Quest(firstQuestTitle,
            firstQuestDescription, firstQuestReward,
            list.ToArray());
        
        questList.Add(addedQuest);
        
        /* Second Quest */
        list = new List<QuestGoal>();
        spentQuestGoal = new SpendQuestGoal( ItemName.ItemId(gold), gold, 50);
        
        list.Add(spentQuestGoal);
        addedQuest = new Quest(secondQuestTitle,
            secondQuestDescription, 200, list.ToArray());
        
        questList.Add(addedQuest);
        
        /* Third Quest */
        list = new List<QuestGoal>();
        string[] thirdTarget = { EnemyName.Wizard, EnemyName.Zombear };
        for (int i = 0; i < thirdTarget.Length; i++)
        {
            string target = thirdTarget[i];
            KillQuestGoal killQuestGoal = new KillQuestGoal(EnemyName.GetEnemyId(target), target, 5);
            list.Add(killQuestGoal);
        }

        spentQuestGoal = new SpendQuestGoal(ItemName.ItemId(gold), gold, 25);
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
