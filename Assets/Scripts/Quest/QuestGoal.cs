using System;
using UnityEngine;

[System.Serializable]
public class QuestGoal : MonoBehaviour
{
    public GoalType GoalType;
    public string[] objective;
    public bool isCompleted;
    public int requiredAmount;
    public int currentAmount;

    public QuestGoal()
    {
        this.isCompleted = false;
        this.currentAmount = 0;
    }
    
    public QuestGoal(GoalType goalType, string[] objective, int requiredAmount)
    {
        this.GoalType = goalType;
        this.objective = objective;
        this.requiredAmount = requiredAmount;
        this.isCompleted = false;
        this.currentAmount = 0;
    }
    
    public void IsCompleted()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }

    private void Complete()
    {
        isCompleted = true;
    }
    
    /*
     * Track the quest
     * Override for each the quest goal
     */
    public virtual void TrackQuest()
    {
        throw new NotImplementedException();
    }

    public virtual void TrackQuest(int id)
    {
        throw new NotImplementedException();
    }
}
