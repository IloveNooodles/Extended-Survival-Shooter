using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public string name;
    public int health;
    public float time;
    public int gold;
    public int score;
    public int completedQuest;

    public GameData()
    {
        this.name = "Player";
        this.health = 100;
        this.time = 0;
        this.gold = 0;
        this.score = 0;
        this.completedQuest = 0;
    }
}
