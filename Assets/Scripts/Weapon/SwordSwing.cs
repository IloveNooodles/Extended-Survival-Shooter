using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    float timer;
    Animator anim;
    Sword sword;
    private bool swingButtonPressed = false;
    private bool isSwinging = false;
    public AudioSource swordSwingAudio;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
        sword = GetComponentInParent<Sword>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        swingButtonPressed = Input.GetButton("Fire1") && timer >= sword.attackSpeed && Time.timeScale != 0 && !isSwinging;
    }

    void FixedUpdate()
    {
        anim.SetBool("isSwinging", swingButtonPressed);
        if (swingButtonPressed)
        {
            swordSwingAudio.Play();
            StartCoroutine(Swing());
        }
    }

    private IEnumerator Swing()
    {
        timer = 0f;
        isSwinging = true;
        yield return new WaitForSeconds(sword.attackSpeed);
        isSwinging = false;
        swordSwingAudio.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && isSwinging)
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(sword.damage + sword.buffDamage);
        }
    }
}
