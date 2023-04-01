using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTitan : MonoBehaviour
{
    public GameObject player;
    public GameObject smokeLocation;
    public GameObject fightingTitan;
    public AudioClip jumpClip;
    public AudioClip landingClip;
    Animator anim;
    bool isJumping = false;
    AudioSource audioSource;
    ParticleSystem smoke;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        smoke = smokeLocation.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        isJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            jump();
        }
    }

    void jump()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
        anim.SetTrigger("Jump");
        StartCoroutine(jumpAnimation());
        isJumping = false;
    }

    IEnumerator jumpAnimation()
    {
        yield return new WaitForSeconds(3f);
        audioSource.clip = landingClip;
        audioSource.Play();
        smoke.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + (player.transform.forward * 3);
        smoke.transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);
        smoke.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        fightingTitan.SetActive(true);
    }
}
