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
    public Image backgroundLeftArmHealthBar;
    public Image backgroundRightArmHealthBar;
    Image leftArmHealthBar;
    Image rightArmHealthBar;
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
        leftArmHealthBar = backgroundLeftArmHealthBar.transform.GetChild(0).GetComponent<Image>();
        rightArmHealthBar = backgroundRightArmHealthBar.transform.GetChild(0).GetComponent<Image>();
        leftArmHealthBarLength = leftArmHealthBar.rectTransform.rect.width;
        rightArmHealthBarLength = rightArmHealthBar.rectTransform.rect.width;
        backgroundLeftArmHealthBar.gameObject.SetActive(false);
        backgroundRightArmHealthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount, Vector3 hitPoint, GameObject titanPart)
    {
        if(titanPart.name == "LeftArm"){
            if(backgroundLeftArmHealthBar.gameObject.activeInHierarchy == false){
                backgroundLeftArmHealthBar.gameObject.SetActive(true);
            }
            leftArmHealth -= amount;
            leftArmHealthBar.rectTransform.sizeDelta = new Vector2(leftArmHealthBarLength * leftArmHealth / 500, leftArmHealthBar.rectTransform.rect.height);
        }

        if(titanPart.name == "RightArm"){
            if(backgroundRightArmHealthBar.gameObject.activeInHierarchy == false){
                backgroundRightArmHealthBar.gameObject.SetActive(true);
            }
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
