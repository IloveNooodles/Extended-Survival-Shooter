using System;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    [SerializeField] public GameObject[] petsPrefab;
    public int currentPetIndex = 0;

    public GameObject summoningMagic;

    private GameObject[] pets;
    Slider petHeartSlider; 

    void Awake()
    {
        petHeartSlider = GameObject.Find("PetHeartSlider").GetComponent<Slider>();
        pets = new GameObject[petsPrefab.Length];
        int i = 0;
        
        petHeartSlider.gameObject.SetActive(true);
        foreach (var pet in petsPrefab)
        {
            pets[i] = Instantiate(pet);
            i++;
        }

    }

    private void summon()
    {
        GameObject magic = Instantiate(
            summoningMagic,
            pets[currentPetIndex].transform.position,
            pets[currentPetIndex].transform.rotation
        );
        
        magic.transform.parent = pets[currentPetIndex].transform;

        ParticleSystem particle = magic.GetComponent<ParticleSystem>();
        // get duration
        float totalDuration = particle.main.duration;
        
        
        Destroy(magic, totalDuration);
    }

    public void Start()
    {
        foreach (var pet in pets)
        {
            pet.SetActive(false);
        }

        if (currentPetIndex > pets.Length - 1)
        {
            petHeartSlider.gameObject.SetActive(false);   
            return;
        }
       
        pets[currentPetIndex].SetActive(true);

        summon();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangePet(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangePet(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangePet(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangePet(pets.Length);
        }
    }

    void ChangePet(int index)
    {
        if (currentPetIndex == index) return;

        if (currentPetIndex < pets.Length)
        {
            pets[currentPetIndex].SetActive(false);
        }

        currentPetIndex = index;
        if (index > pets.Length - 1)
        {
            petHeartSlider.gameObject.SetActive(false);
            return;
        }
        
        petHeartSlider.gameObject.SetActive(true);
        pets[currentPetIndex].SetActive(true);
        summon();
    }
}