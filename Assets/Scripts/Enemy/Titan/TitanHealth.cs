using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitanHealth : MonoBehaviour
{
    public TitanAudio titanAudio;
    public GameObject noLeftArmTitan;
    public GameObject noRightArmTitan;
    static public int startingHealth = 200;
    static public int startingLeftArmHealth = 40;
    static public int startingRightArmHealth = 40;
    static public int currentHealth = int.MinValue;
    static public int leftArmHealth = int.MinValue;
    static public int rightArmHealth = int.MinValue;
    public Image healthBar;
    public Image backgroundLeftArmHealthBar;
    public Image backgroundRightArmHealthBar;
    Image leftArmHealthBar;
    Image rightArmHealthBar;
    float healthBarLength;
    float leftArmHealthBarLength;
    float rightArmHealthBarLength;
    TitanAttackAndMovement titanAttackAndMovement;
    public ParticleSystem hitParticles;
    public ParticleSystem smokeParticles;
    Animator anim;
    bool isDead = false;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        titanAttackAndMovement = GetComponent<TitanAttackAndMovement>();
        healthBarLength = healthBar.rectTransform.rect.width;

        //Jika Titan baru pertama kali diinisialisasi
        if (TitanHealth.currentHealth == int.MinValue)
        {
            TitanHealth.currentHealth = TitanHealth.startingHealth;
        }
        else
        {
            //Set health bar
            healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);
        }

        //Jika pertama kali diinisialisasi
        if (leftArmHealth == int.MinValue)
        {
            leftArmHealth = startingLeftArmHealth;
            leftArmHealthBar = backgroundLeftArmHealthBar.transform.GetChild(0).GetComponent<Image>();
            leftArmHealthBarLength = leftArmHealthBar.rectTransform.rect.width;
            backgroundLeftArmHealthBar.gameObject.SetActive(false);
        }
        //Jika sudah pernah diinisialisasi dan masih hidup
        else if (leftArmHealth > 0)
        {
            leftArmHealthBar = backgroundLeftArmHealthBar.transform.GetChild(0).GetComponent<Image>();
            leftArmHealthBarLength = leftArmHealthBar.rectTransform.rect.width;

            //Jika Belum pernah terkena hit
            if (leftArmHealth == startingLeftArmHealth)
            {
                backgroundLeftArmHealthBar.gameObject.SetActive(false);
            }else{
                backgroundLeftArmHealthBar.gameObject.SetActive(true);
                leftArmHealthBar.rectTransform.sizeDelta = new Vector2(leftArmHealthBarLength * leftArmHealth / startingLeftArmHealth, leftArmHealthBar.rectTransform.rect.height);
            }
        }

        //Jika pertama kali diinisialisasi
        if (rightArmHealth == int.MinValue)
        {
            rightArmHealth = startingRightArmHealth;
            rightArmHealthBar = backgroundRightArmHealthBar.transform.GetChild(0).GetComponent<Image>();
            rightArmHealthBarLength = rightArmHealthBar.rectTransform.rect.width;
            backgroundRightArmHealthBar.gameObject.SetActive(false);
        }
        //Jika sudah pernah diinisialisasi dan masih hidup
        else if (rightArmHealth > 0)
        {
            rightArmHealthBar = backgroundRightArmHealthBar.transform.GetChild(0).GetComponent<Image>();
            rightArmHealthBarLength = rightArmHealthBar.rectTransform.rect.width;

            //Jika Belum pernah terkena hit
            if (rightArmHealth == startingRightArmHealth)
            {
                backgroundRightArmHealthBar.gameObject.SetActive(false);
            }else{
                backgroundRightArmHealthBar.gameObject.SetActive(true);
                rightArmHealthBar.rectTransform.sizeDelta = new Vector2(rightArmHealthBarLength * rightArmHealth / startingRightArmHealth, rightArmHealthBar.rectTransform.rect.height);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount, Vector3 hitPoint, GameObject titanPart)
    {
        if (isDead)
        {
            return;
        }

        if (titanPart.name == "LeftArm")
        {
            if (backgroundLeftArmHealthBar.gameObject.activeInHierarchy == false)
            {
                backgroundLeftArmHealthBar.gameObject.SetActive(true);
            }
            TitanHealth.leftArmHealth -= amount;
            leftArmHealthBar.rectTransform.sizeDelta = new Vector2(leftArmHealthBarLength * leftArmHealth / startingLeftArmHealth, leftArmHealthBar.rectTransform.rect.height);

            //Jika Left Arm Health habis
            if (TitanHealth.leftArmHealth <= 0)
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
            TitanHealth.rightArmHealth -= amount;
            rightArmHealthBar.rectTransform.sizeDelta = new Vector2(rightArmHealthBarLength * rightArmHealth / startingRightArmHealth, rightArmHealthBar.rectTransform.rect.height);

            //Jika Right Arm Health habis
            if (TitanHealth.rightArmHealth <= 0)
            {
                titanAudio.TitanHurt();
                StartCoroutine(ChangeToNoRightArm());
            }
        }

        //kurangi health
        TitanHealth.currentHealth -= amount;
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarLength * currentHealth / startingHealth, healthBar.rectTransform.rect.height);

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Play particle system
        hitParticles.Play();

        //Jika health habis
        if (TitanHealth.currentHealth <= 0)
        {
            titanAudio.TitanHurt();
            titanAttackAndMovement.Dead();
            StartCoroutine(Dead());
        }
    }

    IEnumerator ChangeToNoLeftArm()
    {
        noLeftArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSomethingBeingDestroyed", true);
        anim.SetTrigger("LeftArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        noLeftArmTitan.SetActive(true);
        gameObject.SetActive(false);
        anim.SetBool("isSomethingBeingDestroyed", false);
        anim.SetBool("isLeftArmDestroyed", true);
    }

    IEnumerator ChangeToNoRightArm()
    {
        noRightArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSomethingBeingDestroyed", true);
        anim.SetTrigger("RightArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        noRightArmTitan.SetActive(true);
        gameObject.SetActive(false);
        anim.SetBool("isSomethingBeingDestroyed", false);
        anim.SetBool("isRightArmDestroyed", true);
    }

    IEnumerator Dead()
    {
        anim.SetBool("isDead", true);
        anim.SetTrigger("Dead");
        titanAudio.TitanHurt();
        isDead = true;
        yield return new WaitForSeconds(5f);
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        titanAudio.Landing();
        smokeParticles.Play();
        Destroy(this.gameObject.GetComponent<TitanHealth>());
        Destroy(this.gameObject.GetComponent<TitanAttackAndMovement>());
        
        //Destroy all hit trigger
        TitanHitDetector[] hitDetectors = this.gameObject.GetComponentsInChildren<TitanHitDetector>();
        foreach (TitanHitDetector hitDetector in hitDetectors)
        {
            Destroy(hitDetector);
        }
    }
}
