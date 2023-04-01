using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentHealth : MonoBehaviour
{
    public IEnvironment environmentObject;
    public int startingHealth = 100;
    public int currentHealth;
    public Image backgroundHealthBar;
    public bool isDead = false;
    Image healthBar;
    float healthBarLength;
    ParticleSystem hitParticles;

    // Start is called before the first frame update
    void Start()
    {
        hitParticles = GetComponentInChildren<ParticleSystem>();
        environmentObject = GetComponent<IEnvironment>();

        //Set health
        currentHealth = startingHealth;
        healthBar = backgroundHealthBar.transform.GetChild(0).GetComponent<Image>();
        healthBarLength = healthBar.rectTransform.rect.width;
        backgroundHealthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if (!isDead)
            {
                environmentObject.Death();
                isDead = true;
            }
        }

    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        //return if disabled
        if (this.enabled == false)
        {
            return;
        }

        //Show health bar
        if (!backgroundHealthBar.gameObject.activeInHierarchy)
        {
            backgroundHealthBar.gameObject.SetActive(true);
        }

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
