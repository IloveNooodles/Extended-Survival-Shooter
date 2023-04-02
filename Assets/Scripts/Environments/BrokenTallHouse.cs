using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenTallHouse : MonoBehaviour
{
    public GameObject tallHouse;
    public GameObject rubble;
    public GameObject smokeGameObject;
    ParticleSystem smoke;
    bool isTallHouseBroken = false;
    // Start is called before the first frame update
    void Start()
    {
        smoke = smokeGameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTallHouseBroken)
        {
            tallHouse.transform.Translate(Vector3.down * Time.deltaTime * 6);
            if(tallHouse.transform.position.y < -25)
            {
                rubble.SetActive(true);
                isTallHouseBroken = false;
                Destroy(tallHouse);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Player")
        {
            isTallHouseBroken = true;
            smoke.Play();
        }
    }
}
