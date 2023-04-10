using System;
using UnityEngine;

public class KillQuestGoal : QuestGoal
{
    private int enemyId;

    public KillQuestGoal(int enemyId, string objective, int requiredAmount) : base(GoalType.Kill, objective,
        requiredAmount)
    {
        this.enemyId = enemyId;
    }

    public override void TrackQuest(GoalType goalType, int enemyId)
    {
        if (this.GoalType != GoalType.Kill) return;
        
        if (enemyId == this.enemyId)
        {
            currentAmount++;
        }
        base.IsCompleted();
    }
        
}
