using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitanHealth : MonoBehaviour
{
    public TitanAudio titanAudio;
    public GameObject noLeftArmTitan;
    public GameObject noRightArmTitan;
    static public int startingHealth = 100000;
    static public int currentHealth;
    static public int leftArmHealth = 10;
    static public int rightArmHealth = 10;
    public Image healthBar;
    public Image backgroundLeftArmHealthBar;
    public Image backgroundRightArmHealthBar;
    Image leftArmHealthBar;
    Image rightArmHealthBar;
    float healthBarLength;
    float leftArmHealthBarLength;
    float rightArmHealthBarLength;
    public ParticleSystem hitParticles;
    public ParticleSystem smokeParticles;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

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
        if (titanPart.name == "LeftArm")
        {
            if (backgroundLeftArmHealthBar.gameObject.activeInHierarchy == false)
            {
                backgroundLeftArmHealthBar.gameObject.SetActive(true);
            }
            leftArmHealth -= amount;
            leftArmHealthBar.rectTransform.sizeDelta = new Vector2(leftArmHealthBarLength * leftArmHealth / 500, leftArmHealthBar.rectTransform.rect.height);

            //Jika Left Arm Health habis
            if (leftArmHealth <= 0)
            {
                titanAudio.TitanHurt();
                StartCoroutine(ChangeToNoLeftArm());
            }
        }

        if (titanPart.name == "RightArm")
        {
            if (backgroundRightArmHealthBar.gameObject.activeInHierarchy == false)
            {
                backgroundRightArmHealthBar.gameObject.SetActive(true);
            }
            rightArmHealth -= amount;
            rightArmHealthBar.rectTransform.sizeDelta = new Vector2(rightArmHealthBarLength * rightArmHealth / 500, rightArmHealthBar.rectTransform.rect.height);
        
            //Jika Right Arm Health habis
            if (rightArmHealth <= 0)
            {
                titanAudio.TitanHurt();
                StartCoroutine(ChangeToNoRightArm());
            }
        }

        //kurangi health
        currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();
    }

    IEnumerator ChangeToNoLeftArm()
    {
        noLeftArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
        anim.SetTrigger("LeftArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        noLeftArmTitan.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator ChangeToNoRightArm()
    {
        noRightArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
        anim.SetTrigger("RightArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        noRightArmTitan.SetActive(true);
        gameObject.SetActive(false);
    }
}
