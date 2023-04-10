public class SpendQuestGoal : QuestGoal
{
    private int objectId;

    public SpendQuestGoal(string objective,int objectId, int requiredAmount) : base(GoalType.Spend, objective,
        requiredAmount)
    {
        this.objectId = objectId;
    }

    public override void TrackQuest(GoalType goalType, int id, int amount)
    {
        if (goalType != GoalType.Spend) return;
        if (this.objectId != id) return;
        this.currentAmount += amount;
        base.IsCompleted();
    }
}
