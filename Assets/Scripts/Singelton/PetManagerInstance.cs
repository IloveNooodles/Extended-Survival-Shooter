using System;
using UnityEngine;
public class PetManagerInstance : MonoBehaviour
{
    private static PetManagerInstance _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}