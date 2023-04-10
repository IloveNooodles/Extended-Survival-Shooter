using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static string name;

    private void Awake()
    {
        name = "Player";
    }

    private static void SetName(string newName)
    {
        name = newName;
    }
    
}
