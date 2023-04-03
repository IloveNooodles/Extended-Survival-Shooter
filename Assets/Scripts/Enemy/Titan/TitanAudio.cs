using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAudio : MonoBehaviour
{
    public AudioClip roarClip;
    public AudioClip jumpClip;
    public AudioClip landingClip;
    public AudioClip titanHurtClip;
    public AudioClip wooshClip;
    public AudioClip titanWalkClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Roar()
    {
        audioSource.clip = roarClip;
        audioSource.Play();
    }

    public void Jump()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
    }

    public void Landing()
    {
        audioSource.clip = landingClip;
        audioSource.Play();
    }

    public void Walk()
    {
        audioSource.clip = titanWalkClip;
        audioSource.Play();
    }

    public void TitanHurt()
    {
        audioSource.clip = titanHurtClip;
        audioSource.Play();
    }

    public void Woosh()
    {
        audioSource.clip = wooshClip;
        audioSource.Play();
    }
}
