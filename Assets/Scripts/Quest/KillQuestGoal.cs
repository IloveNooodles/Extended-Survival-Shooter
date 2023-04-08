using UnityEngine;

public class KillQuestGoal : QuestGoal
{
    public int EnemyId;

    public KillQuestGoal(int enemyId, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.EnemyId = enemyId;
        this.description = description;
        this.isCompleted = completed;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
    }

    public void Track()
    {
    }
}
