using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitanHealth : MonoBehaviour
{
    public int startingHealth = 100000;
    public int currentHealth;
    public int leftArmHealth = 500;
    public int rightArmHealth = 500;
    public Image healthBar;
    public Image leftArmHealthBar;
    public Image rightArmHealthBar;
    float healthBarLength;
    float leftArmHealthBarLength;
    float rightArmHealthBarLength;

    Animator anim;
    ParticleSystem hitParticles;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();

        //Set health
        currentHealth = startingHealth;
        healthBarLength = healthBar.rectTransform.rect.width;
        leftArmHealthBarLength = leftArmHealthBar.rectTransform.rect.width;
        rightArmHealthBarLength = rightArmHealthBar.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount, Vector3 hitPoint, GameObject titanPart)
    {
        if(titanPart.name == "LeftArm"){
            leftArmHealth -= amount;
            leftArmHealthBar.rectTransform.sizeDelta = new Vector2(leftArmHealthBarLength * leftArmHealth / 500, leftArmHealthBar.rectTransform.rect.height);
        }

        if(titanPart.name == "RightArm"){
            rightArmHealth -= amount;
            rightArmHealthBar.rectTransform.sizeDelta = new Vector2(rightArmHealthBarLength * rightArmHealth / 500, rightArmHealthBar.rectTransform.rect.height);
        }

        //kurangi health
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();
    }
}
