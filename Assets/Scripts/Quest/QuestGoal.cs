using System;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType GoalType;
    public string objective;
    public bool isCompleted;
    public int requiredAmount;
    public int currentAmount;

    public QuestGoal()
    {
        this.isCompleted = false;
        this.currentAmount = 0;
    }
    
    public QuestGoal(GoalType goalType, string objective, int requiredAmount)
    {
        GoalType = goalType;
        this.objective = objective;
        this.requiredAmount = requiredAmount;
        isCompleted = false;
        currentAmount = 0;
    }
    
    public void IsCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    public bool GetQuestStatus()
    {
        return isCompleted;
    }

    private void Complete()
    {
        isCompleted = true;
    }
    
    
    
    /*
     * Track the quest
     * Override for each the quest goal
     */
    public virtual void TrackQuest(GoalType goalType, int id)
    {
        throw new NotImplementedException();
    }
}
