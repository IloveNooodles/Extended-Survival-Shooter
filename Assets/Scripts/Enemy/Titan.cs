using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : MonoBehaviour
{
    public AudioClip roarClip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Roar at the start of the game
        StartCoroutine(Roar());
    }

    IEnumerator Roar(){
        yield return new WaitForSeconds(3f);
        audioSource.clip = roarClip;
        audioSource.Play();
    }   
}
