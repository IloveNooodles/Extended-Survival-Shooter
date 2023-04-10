using System;
using UnityEngine;

public class KillQuestGoal : QuestGoal
{
    private int enemyId;

    public KillQuestGoal(int enemyId, string[] objective, int requiredAmount) : base(GoalType.Kill, objective,
        requiredAmount)
    {
        this.enemyId = enemyId;
    }

    public override void TrackQuest(int enemyId)
    {
        if (enemyId == this.enemyId)
        {
            currentAmount++;
        }
        base.IsCompleted();
    }
        
}
