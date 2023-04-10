public class SpendQuestGoal : QuestGoal
{
    public SpendQuestGoal(string objective, int requiredAmount) : base(GoalType.Spend, objective, requiredAmount){}

    public override void TrackQuest(int amount)
    {
        this.currentAmount += amount;
        base.IsCompleted();
    }
}
