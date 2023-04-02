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
    AudioSource audioSource;
    ParticleSystem smoke;
    float timer;
    bool isSmoking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        smoke = smokeLocation.GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        jump();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSmoking){
            timer += Time.deltaTime;
            smoke.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + (player.transform.forward * 3);
            smoke.transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);
            if(timer >= 3f){
                Destroy(gameObject);
                fightingTitan.SetActive(true);
            }
        }
    }

    void jump()
    {
        audioSource.clip = jumpClip;
        audioSource.Play();
        anim.SetTrigger("Jump");
        StartCoroutine(jumpAnimation());
    }

    IEnumerator jumpAnimation()
    {
        yield return new WaitForSeconds(3f);
        audioSource.clip = landingClip;
        audioSource.Play();
        smoke.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + (player.transform.forward * 3);
        smoke.transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);
        smoke.Play();
        isSmoking = true;
    }
}
