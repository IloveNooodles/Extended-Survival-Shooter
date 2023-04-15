using System;
using UnityEngine;
public class PetManagerInstance : MonoBehaviour
{
    private static PetManagerInstance _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }
}