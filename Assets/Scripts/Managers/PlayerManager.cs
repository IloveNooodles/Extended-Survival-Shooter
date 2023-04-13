using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static string PlayerName;

    private void Awake()
    {
        name = "Player";
    }

    private static void SetName(string newName)
    {
        PlayerName = newName;
    }
    
}
