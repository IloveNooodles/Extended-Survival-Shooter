using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTitan : MonoBehaviour
{
    GameObject player;
    public GameObject smokeLocation;
    public GameObject fightingTitan;
    public TitanAudio titanAudio;
    Animator anim;
    ParticleSystem smoke;
    float timer;
    bool isSmoking = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        smoke = smokeLocation.GetComponent<ParticleSystem>();
        jump();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSmoking){
            timer += Time.deltaTime;
            if(timer >= 3f){
                Destroy(gameObject);
                fightingTitan.SetActive(true);
            }
        }
    }

    void jump()
    {
        titanAudio.Jump();
        anim.SetTrigger("Jump");
        StartCoroutine(jumpAnimation());
    }

    IEnumerator jumpAnimation()
    {
        yield return new WaitForSeconds(3f);
        titanAudio.Landing();
        smoke.transform.position = new Vector3(gameObject.transform.position.x - 45, gameObject.transform.position.y+1, gameObject.transform.position.z);
        smoke.Play();
        isSmoking = true;
    }
}
