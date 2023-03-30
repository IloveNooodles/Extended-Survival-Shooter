using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockDown : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public AudioClip fallingClip;
    public Image healthBar;
    float healthBarLength;
    bool isClockDown = false;

    float fallVelocity = 0f;

    ParticleSystem hitParticles;
    GameObject particleLeft;
    GameObject particleRight;
    GameObject particleDownLeft;
    GameObject particleDownRight;
    AudioSource clockAudio;


    // Start is called before the first frame update
    void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
        clockAudio = GetComponent<AudioSource>();
        particleLeft = this.gameObject.transform.GetChild(2).gameObject;
        particleRight = this.gameObject.transform.GetChild(3).gameObject;
        particleDownLeft = this.gameObject.transform.GetChild(4).gameObject;
        particleDownRight = this.gameObject.transform.GetChild(5).gameObject;

        currentHealth = startingHealth;
        healthBarLength = healthBar.rectTransform.rect.width;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isClockDown)
        {
            return;
        }
        else
        {
            if (transform.position.y > 0f)
            {
                //simulate gravity
                Vector3 pos = transform.position;
                fallVelocity += Physics.gravity.y * Time.deltaTime;
                pos.y += fallVelocity * Time.deltaTime;
                transform.position = pos;
            }else{
                particleDownLeft.SetActive(true);
                particleDownRight.SetActive(true);
            }
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Check jika dead
        if (isClockDown)
            return;

        //kurangi health
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            isClockDown = true;
            Death();
        }
    }

    void Death(){
        //Play falling audio
        clockAudio.clip = fallingClip;
        clockAudio.Play();

        particleLeft.SetActive(true);
        particleRight.SetActive(true);

        Destroy(particleLeft, 2.5f);
        Destroy(particleRight, 2.5f);
    }
}
