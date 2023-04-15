using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int health;
    public float time;
    public int gold;
    public int score;
    public int completedQuest;
    public int weapon;

    public int pet;
    public int petHealth;

    public string lastSavedDate;
    public string saveName;

    public GameData()
    {
        this.health = 100;
        this.time = 0;
        this.gold = 0;
        this.score = 0;
        this.completedQuest = 0;
        this.pet = 0;
        this.petHealth = 100;
        this.weapon = 0;
        this.lastSavedDate = DateTime.Now.ToString();
        this.saveName = "Save Slot X";
    }
}
