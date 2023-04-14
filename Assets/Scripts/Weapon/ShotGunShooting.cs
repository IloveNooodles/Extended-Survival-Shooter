using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShooting : MonoBehaviour
{
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    public AudioSource gunAudio;
    public AudioSource gunReloadAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    private bool reloadKeyPressed = false;
    bool isReloading = false;
    private bool isShooting = false;

    [SerializeField] private GameObject shotGun;
    private ShotGun gunScript;

    private GameObject[] bullets;
    private LineRenderer[] gunlines;

    [SerializeField] private GameObject player;
    private PlayerQuest pq;

    [SerializeField] private GameObject bullet;
    
    void Awake()
    {
        //Get Gun Script
        gunScript = shotGun.GetComponent<ShotGun>();

        /* Get player quest */
        pq = player.GetComponent<PlayerQuest>();

        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLight = GetComponent<Light>();

        //Instantiate bullet
        for (int i = 0; i < gunScript.bulletsPerShot-1; i++)
        {
            Instantiate(bullet, transform);
        }

        //Mendaapatkan child bullet
        bullets = new GameObject[gunScript.bulletsPerShot];
        gunlines = new LineRenderer[gunScript.bulletsPerShot];
        
        for (int i = 0; i < gunScript.bulletsPerShot; i++)
        {
            bullets[i] = transform.GetChild(i).gameObject;
            gunlines[i] = bullets[i].GetComponent<LineRenderer>();
        }
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
                     NumberOfBulletsManager.numberOfBullets - gunScript.bulletsPerShot >= 0 && !isReloading;
        
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
        for (int i = 0; i < gunScript.bulletsPerShot; i++)
        {
            gunlines[i].enabled = false;
        }

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

        //Decrease bullet
        WeaponManager.Attack();

        //Shoot bullet
        for (int i = 0; i < gunScript.bulletsPerShot; i++)
        {
            gunlines[i].enabled = true;
            gunlines[i].SetPosition(0, bullets[i].transform.position);

            shootRay.origin = bullets[i].transform.position;
            shootRay.direction = Quaternion.AngleAxis((i-2)*45/gunScript.bulletsPerShot, new Vector3(0,1,0)) * bullets[i].transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, gunScript.range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth)
                {
                    if (enemyHealth.IsDead())
                    {
                        return;
                    }
                    //damage based on distance
                    float distance = Vector3.Distance(shootRay.origin, shootHit.point);
                    int damage = Mathf.RoundToInt(Mathf.Lerp(20f, 0f, distance/gunScript.range));
                    enemyHealth.TakeDamage(damage, shootHit.point);
                }

                gunlines[i].SetPosition(1, shootHit.point);
            }
            else
            {
                gunlines[i].SetPosition(1, shootRay.origin + shootRay.direction * gunScript.range);
            }
        }

        pq.Track(GoalType.Spend, ItemName.ItemId(ItemName.Bullet), 1);
    }
}
