using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowShooting : MonoBehaviour
{
    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer bowLine;
    float effectsDisplayTime = 0.2f;
    public AudioSource bowPullAudio;
    public AudioSource bowShootAudio;
    public AudioSource bowReloadAudio;
    public Image powerMeter;

    [SerializeField] private GameObject bow;
    private Bow bowScript;
    private GameObject player;
    private PlayerQuest pq;

    private bool isPulling = false;
    private bool isShooting = false;
    private bool isReloading = false;
    private bool reloadKeyPressed = false;
    float currentPower = 0;
    float maxPower = 1;
    float chargeSpeed = 0.5f;
    float powerMeterLength;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Get Bow Script
        bowScript = bow.GetComponent<Bow>();

        /* Get player quest */
        pq = player.GetComponent<PlayerQuest>();

        //GetMask
        shootableMask = LayerMask.GetMask("Shootable");

        //Mendapatkan Reference component
        bowLine = GetComponent<LineRenderer>();

        powerMeterLength = powerMeter.rectTransform.rect.width;
    }

    void Update()
    {
        //Add timer
        timer += Time.deltaTime;

        //Check if player is reloading
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadKeyPressed = true;
        }

        //Check if player is pulling
        if (Input.GetMouseButton(1) && !isPulling && !isShooting && !isReloading && bowScript.numberOfBullets > 0 && timer >= bowScript.attackSpeed)
        {
            isPulling = true;
            bowPullAudio.Play();
        }

        //Check if player is shooting
        if (Input.GetMouseButtonUp(1) && isPulling && !isShooting && !isReloading)
        {
            isPulling = false;
            isShooting = true;
            bowPullAudio.Stop();
            bowShootAudio.Play();
        }

        //Check if player is reloading
        if (reloadKeyPressed && !isPulling && !isShooting && !isReloading)
        {
            isReloading = true;
            reloadKeyPressed = false;
            bowReloadAudio.Play();
        }

        if (timer >= bowScript.attackSpeed * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void FixedUpdate()
    {
        if (reloadKeyPressed)
        {
            isReloading = true;
            reloadKeyPressed = false;
            bowReloadAudio.Play();
            StartCoroutine(Reload());
        } else if (isPulling)
        {
            Pull();
        } else if (isShooting)
        {
            Shoot();
        }
    }

    void Pull() {
        currentPower += chargeSpeed * Time.deltaTime;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
        }
        //Set power meter
        powerMeter.rectTransform.sizeDelta = new Vector2((1 - (currentPower / maxPower)) * powerMeterLength, powerMeter.rectTransform.sizeDelta.y);
    }

    void Shoot()
    {
        //Reset timer
        timer = 0f;

        //Reset power meter
        powerMeter.rectTransform.sizeDelta = new Vector2(powerMeterLength, powerMeter.rectTransform.sizeDelta.y);

        //Set shootRay
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        //Set bowLine
        bowLine.enabled = true;
        bowLine.SetPosition(0, transform.position);

        //Lakukan raycast jika mendeteksi id nemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, currentPower * bowScript.range, shootableMask))
        {
            //Lakukan raycast hit hace component Enemyhealth
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            if (enemyHealth)
            {
                /* Lakukan Take Damage */
                enemyHealth.TakeDamage(bowScript.damage+bowScript.buffDamage, shootHit.point);
            }

            //Set line end position ke hit position
            bowLine.SetPosition(1, shootHit.point);
        }
        else
        {
            //set line end position ke range freom barrel
            bowLine.SetPosition(1, shootRay.origin + shootRay.direction * currentPower * bowScript.range);
        }

        WeaponManager.Attack();

        //Reset current power
        currentPower = 0;
        isShooting = false;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(bowScript.reloadTime);
        WeaponManager.Reload();
        isReloading = false;
        bowReloadAudio.Stop();
    }

    public void DisableEffects()
    {
        bowLine.enabled = false;
    }
}
