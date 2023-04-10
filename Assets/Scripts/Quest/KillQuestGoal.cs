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

    public override void TrackQuest(GoalType goalType, int id, int amount)
    {
        if (this.GoalType != GoalType.Kill) return;
        if (this.enemyId != id) return;
        currentAmount++;
        base.IsCompleted();
    }
        
}
