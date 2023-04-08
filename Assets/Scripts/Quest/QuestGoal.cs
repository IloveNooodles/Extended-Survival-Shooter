public class QuestGoal
{
    public GoalType GoalType;
    public string description;
    public bool isCompleted;
    public int requiredAmount;
    public int currentAmount;

    public void IsCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        isCompleted = true;
    }
}
