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
    public bool[] weaponBought;

    public int pet;
    public int[] petHealth;
    public bool[] petBought;

    public string lastSavedDate;
    public string saveName;

    public GameData()
    {
        this.health = 100;
        this.time = 0;
        this.gold = 0;
        this.score = 0;
        this.completedQuest = 0;

        this.weapon = 0;
        this.weaponBought = new bool[] {true, false, false, false};
        
        this.pet = 4;
        this.petHealth = new int[] {};
        this.petBought = new bool[] {false, false, false};
        
        this.lastSavedDate = DateTime.Now.ToString();
        this.saveName = "Save Slot X";
    }
}
