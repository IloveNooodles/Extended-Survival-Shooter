using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    private static List<Quest> questList = new List<Quest>();

    public void Awake()
    {
        InitQuestList();
    }

    public void InitQuestList() {
        string[] firstTarget = {EnemyName.Zombunny};
        List<QuestGoal> list = new List<QuestGoal>();
        for (int i = 0; i < firstTarget.Length; i++)
        {
            string target = firstTarget[i];
            KillQuestGoal killQuestGoal = new KillQuestGoal(EnemyName.GetEnemyId(target), target, 3);
            list.Add(killQuestGoal);
        }

        Quest addedQuest = new Quest("Kalahkan musuh!",
            "Ketika kamu ingin mengalahkan boss, kamu dihalangi oleh banyak zombie zombie aneh. Kalahkan mereka!", 500,
            list.ToArray());
        questList.Add(addedQuest);
    }

    public Quest GetQuestByIndex(int index)
    {
        if (index >= questList.Count) return new Quest();
        return questList[index];
    }
}
