using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentHealth : MonoBehaviour
{
    public IEnvironment environmentObject;
    public int startingHealth = 100;
    public int currentHealth;
    public Image healthBar;
    public bool isDead = false;
    float healthBarLength;
    ParticleSystem hitParticles;

    // Start is called before the first frame update
    void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
        environmentObject = GetComponent<IEnvironment>();

        currentHealth = startingHealth;
        healthBarLength = healthBar.rectTransform.rect.width;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //Check jika dead
        if (isDead)
            return;

        //kurangi health
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Dead jika health <= 0
        if (currentHealth <= 0)
        {
            environmentObject.Death();
        }
    }
}
