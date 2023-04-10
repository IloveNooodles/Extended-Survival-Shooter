using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    
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
    float reloadTime = 2f;

    [SerializeField] private GameObject player;
    private PlayerQuest pq;
    
    void Awake()
    {
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
        yield return new WaitForSeconds(reloadTime);
        NumberOfBulletsManager.numberOfBullets = NumberOfBulletsManager.MAX_NUM_BULLET;
        isReloading = false;
        gunReloadAudio.Stop();
    }
    
    void Update()
    {
        
        if(isReloading){
           return;
        }
        
        timer += Time.deltaTime;
        isShooting = Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 &&
                     NumberOfBulletsManager.numberOfBullets > 0 && !isReloading;
        
        //if user press r then reload
        if(Input.GetKeyDown(KeyCode.R) && !isReloading && !isShooting)
        {
            reloadKeyPressed = true;
            return;
        }
        
        if (timer >= timeBetweenBullets * effectsDisplayTime)
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
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            if (enemyHealth)
            {
                /* kalo udah mati biarin */
                if (enemyHealth.IsDead())
                {
                    return;
                }
                
                /* Lakukan Take Damage */
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                if (enemyHealth.IsDead())
                {
                    pq.Track(GoalType.Kill, enemyHealth.Id, 1);
                }
            }

            //Set line end position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        //Kurangi jumlah peluru
        NumberOfBulletsManager.numberOfBullets--;
        pq.Track(GoalType.Spend, ItemName.ItemId(ItemName.Bullet), 1);
    }
}