using UnityEngine;

public class FPSShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public Transform target;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    bool isReloading = false;
    float reloadTime = 2f;

    void Awake()
    {
        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }


    void Update()
    {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && NumberOfBulletsManager.numberOfBullets > 0 && !isReloading)
        {
            Shoot();
        }
        //if user press r then reload
        else if (Input.GetKeyDown(KeyCode.R))
        {
            timer = 0f;
            isReloading = true;
            NumberOfBulletsManager.numberOfBullets = 0;
        }

        if (isReloading && timer >= reloadTime)
        {
            isReloading = false;
            NumberOfBulletsManager.numberOfBullets = 30;
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
        shootRay.direction = target.forward;

        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            if (shootHit.collider.name == "Clock")
            {
                shootHit.collider.GetComponent<EnvironmentHealth>().TakeDamage(damagePerShot, shootHit.point);
            }
            else
            {
                //Lakukan raycast hit hace component Enemyhealth
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    //Lakukan Take Damage
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
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
    }
}