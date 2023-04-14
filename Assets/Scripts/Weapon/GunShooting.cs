using System;
using System.Collections;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    public AudioSource gunAudio;
    public AudioSource gunReloadAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    private bool reloadKeyPressed = false;
    bool isReloading = false;
    private bool isShooting = false;

    [SerializeField] private GameObject gun;
    private Gun gunScript;

    private GameObject player;
    
    private PlayerQuest pq;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Get Gun Script
        gunScript = gun.GetComponent<Gun>();

        /* Get player quest */
        pq = player.GetComponent<PlayerQuest>();

        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }    
    
    private void FixedUpdate()
    {
        if (reloadKeyPressed)
        {
            isReloading = true;
            reloadKeyPressed = false;
            gunReloadAudio.Play();
            StartCoroutine(Reload());
        } else if (isShooting)
        { 
            Shoot();
        }  
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunScript.reloadTime);
        WeaponManager.Reload();
        isReloading = false;
        gunReloadAudio.Stop();
    }
    
    void Update()
    {
        
        if(isReloading){
           return;
        }
        
        timer += Time.deltaTime;
        isShooting = Input.GetButton("Fire1") && timer >= gunScript.attackSpeed && Time.timeScale != 0 &&
                     NumberOfBulletsManager.numberOfBullets > 0 && !isReloading;
        
        //if user press r then reload
        if(Input.GetKeyDown(KeyCode.R) && !isReloading && !isShooting)
        {
            reloadKeyPressed = true;
            return;
        }
        
        if (timer >= gunScript.attackSpeed * effectsDisplayTime)
        {
            DisableEffects();
        }
    }


    public void DisableEffects()
    {
        //disable line renderer
        gunLine.enabled = false;

        //disable light
        gunLight.enabled = false;
    }


    void Shoot()
    {
        timer = 0f;

        //Play audio
        gunAudio.Play();

        //enable Light
        gunLight.enabled = true;

        //Play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        //enable Line renderer dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        //Set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, gunScript.range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            if (enemyHealth)
            {
                /* Lakukan Take Damage */
                enemyHealth.TakeDamage(gunScript.damage+gunScript.buffDamage, shootHit.point);
            }

            //Set line end position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * gunScript.range);
        }

        //Kurangi jumlah peluru
        WeaponManager.Attack();
        pq.Track(GoalType.Spend, ItemName.ItemId(ItemName.Bullet), 1);
    }
}