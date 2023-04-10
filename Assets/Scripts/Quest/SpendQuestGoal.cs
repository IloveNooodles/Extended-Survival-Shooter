public class SpendQuestGoal : QuestGoal
{
    public SpendQuestGoal(string objective, int requiredAmount) : base(GoalType.Spend, objective, requiredAmount){}

    public override void TrackQuest(GoalType goalType, int amount)
    {
        if (goalType != GoalType.Spend) return;
        this.currentAmount += amount;
        base.IsCompleted();
    }
}
