using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitanHealth : MonoBehaviour
{
    public TitanAudio titanAudio;
    public GameObject noLeftArmTitan;
    public GameObject noRightArmTitan;
    public GameObject noArmTitan;
    public float destroyingArmHealthPercentage = 0.3f;
    static public int startingHealth = 500;
    static public int startingLeftArmHealth = 200;
    static public int startingRightArmHealth = 200;
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
    UnityEngine.AI.NavMeshAgent nav;
    public ParticleSystem hitParticles;
    public ParticleSystem smokeParticles;
    public CutSceneManager cutSceneManager;
    Animator anim;
    bool isDead = false;
    bool isInvincible = false;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            }
            else
            {
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
            }
            else
            {
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

        if (isInvincible)
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
                if (TitanHealth.currentHealth - (int)(destroyingArmHealthPercentage * TitanHealth.startingHealth) <= 0)
                {
                    amount = (int)(destroyingArmHealthPercentage * TitanHealth.startingHealth);
                }
                else
                {
                    isInvincible = true;
                    titanAudio.TitanHurt();
                    StartCoroutine(ChangeToNoLeftArm());
                }

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
                if (TitanHealth.currentHealth - (int)(destroyingArmHealthPercentage * TitanHealth.startingHealth) <= 0)
                {
                    amount = (int)(destroyingArmHealthPercentage * TitanHealth.startingHealth);

                }
                else
                {
                    isInvincible = true;
                    titanAudio.TitanHurt();
                    StartCoroutine(ChangeToNoRightArm());
                }
            }
        }

        if (CheatManager.is1HitKill)
        {
            amount = TitanHealth.currentHealth;
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
            Dead();
        }
    }

    IEnumerator ChangeToNoLeftArm()
    {
        nav.enabled = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isSomethingBeingDestroyed", true);
        anim.SetTrigger("LeftArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        isInvincible = false;
        TakeDamage((int)(destroyingArmHealthPercentage * TitanHealth.startingHealth), transform.position, gameObject);
        if (TitanHealth.rightArmHealth <= 0)
        {
            noArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
            noArmTitan.SetActive(true);
        }
        else
        {
            noLeftArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
            noLeftArmTitan.SetActive(true);
        }
        gameObject.SetActive(false);
        anim.SetBool("isSomethingBeingDestroyed", false);
        anim.SetBool("isLeftArmDestroyed", true);

    }

    IEnumerator ChangeToNoRightArm()
    {
        nav.enabled = false;
        anim.SetBool("isWalking", false);
        anim.SetBool("isSomethingBeingDestroyed", true);
        anim.SetTrigger("RightArmDestroyed");
        yield return new WaitForSeconds(2.2f);
        titanAudio.Landing();
        smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        smokeParticles.Play();
        yield return new WaitForSeconds(3f);
        isInvincible = false;
        TakeDamage((int)(destroyingArmHealthPercentage * TitanHealth.startingHealth), transform.position, gameObject);
        if (TitanHealth.leftArmHealth <= 0)
        {
            noArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
            noArmTitan.SetActive(true);
        }
        else
        {
            noRightArmTitan.transform.position = new Vector3(transform.position.x + 30, transform.position.y, transform.position.z);
            noRightArmTitan.SetActive(true);
        }
        gameObject.SetActive(false);
        anim.SetBool("isSomethingBeingDestroyed", false);
        anim.SetBool("isRightArmDestroyed", true);
    }

    void Dead()
    {
        cutSceneManager.startBossEndCutScene();
        // isInvincible = true;
        // nav.enabled = false;
        // anim.SetBool("isDead", true);
        // anim.SetTrigger("Dead");
        // titanAudio.TitanHurt();
        // isDead = true;
        // yield return new WaitForSeconds(5f);
        // smokeParticles.transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        // smokeParticles.transform.rotation = new Quaternion(0, 0, 0, 0);
        // titanAudio.Landing();
        // smokeParticles.Play();
        // Destroy(this.gameObject.GetComponent<TitanHealth>());
        // Destroy(this.gameObject.GetComponent<TitanAttackAndMovement>());

        // //Destroy all hit trigger
        // TitanHitDetector[] hitDetectors = this.gameObject.GetComponentsInChildren<TitanHitDetector>();
        // foreach (TitanHitDetector hitDetector in hitDetectors)
        // {
        //     Destroy(hitDetector);
        // }
    }
}
