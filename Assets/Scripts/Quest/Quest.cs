
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Quest
{
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;
    public QuestGoal[] questGoal;

    public Quest()
    {
        
    }
    
    public Quest(string title, string description, int reward, QuestGoal[] questGoal)
    {
        this.isActive = false;
        this.title = title;
        this.description = description;
        this.goldReward = reward;
        this.questGoal = questGoal;
    }
}
