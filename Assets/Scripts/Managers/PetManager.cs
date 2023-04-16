using System;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] public GameObject[] petsPrefab;
    public static int currentPetIndex = 4;

    public GameObject summoningMagic;

    private static GameObject[] pets;
    public static GameObject currentPet;
    private static GameObject petHeartSlider;

    private GameObject player;

    public static bool[] isPetBought = {false, false, false};

    private static PetHealth[] petHealth;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        petHeartSlider = GameObject.FindWithTag("PetHeartSlider");

        if (pets == null || pets.Length == 0 || pets[0] == null)
        {
            pets = new GameObject[petsPrefab.Length];
            // isPetBought = new bool[pets.Length];
            int i = 0;
            petHealth = new PetHealth[pets.Length];

            if (petHeartSlider != null)
            {
                petHeartSlider.SetActive(true);
            }

            foreach (var pet in petsPrefab)
            {
                if (pets[i] != null)
                {
                    continue;
                }

                pets[i] = Instantiate(pet);
                pets[i].tag = "Pet";
                // isPetBought[i] = false;
                petHealth[i] = pets[i].GetComponent<PetHealth>();
                i++;
            }
        }

        if (currentPetIndex < pets.Length)
        {
            if (pets[currentPetIndex] != null)
            {
                int i = 0;
                foreach (var pet in pets)
                {
                    pet.GetComponent<PetHealth>().currentHealth = petHealth[i].currentHealth;
                    i++;
                }

                placePet();

                pets[currentPetIndex].SetActive(true);
                currentPet = pets[currentPetIndex];
                petHeartSlider.SetActive(true);
                petHeartSlider.GetComponent<Slider>().value = petHealth[currentPetIndex].currentHealth;

                // summon();
            }
        }
        
    }

    public static void SetAllNonActive()
    {
        if (pets != null && pets.Length > 0 && pets[0] != null)
        {
            foreach (var pet in pets)
            {
                pet.SetActive(false);
            }
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
            petHeartSlider.SetActive(false);
            return;
        }

        if (pets[currentPetIndex].gameObject.GetComponent<PetHealth>().currentHealth <= 0)
        {
            // pet is dead
            petHeartSlider.SetActive(false);
            return;
        }

        placePet();

        pets[currentPetIndex].SetActive(true);
        currentPet = pets[currentPetIndex];

        summon();
    }

    private void placePet()
    {
        // place pet randomly near player.transform
        float r = UnityEngine.Random.Range(3, 5);
        float x = UnityEngine.Random.Range(-r, r);
        float z = r * r - x * x;
        z = Mathf.Sqrt(z);


        pets[currentPetIndex].transform.position = new Vector3(
            player.transform.position.x + x,
            0,
            player.transform.position.z + z
        );
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha7))
        // {
        //     ChangePet(0);
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha8))
        // {
        //     ChangePet(1);
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha9))
        // {
        //     ChangePet(2);
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     ChangePet(pets.Length);
        // }
    }

    void ChangePet(int index)
    {
        if (currentPetIndex == index) return;

        if (currentPetIndex < pets.Length)
        {
            currentPet = null;
            pets[currentPetIndex].SetActive(false);
        }

        currentPetIndex = index;
        if (index > pets.Length - 1)
        {
            petHeartSlider.SetActive(false);
            return;
        }

        if (pets[currentPetIndex].gameObject.GetComponent<PetHealth>().currentHealth <= 0)
        {
            // pet is dead
            petHeartSlider.SetActive(false);
            return;
        }

        petHeartSlider.SetActive(true);
        petHeartSlider.GetComponent<Slider>().value = petHealth[currentPetIndex].currentHealth;

        placePet();
        pets[currentPetIndex].SetActive(true);
        currentPet = pets[currentPetIndex];
        summon();
    }

    public void LoadData(GameData data)
    {
        currentPetIndex = data.pet;

        for (int i = 0; i < petHealth.Length; i++)
        {
            petHealth[i].currentHealth = data.petHealth[i];
        }

        for (int i = 0; i < isPetBought.Length; i++)
        {
            isPetBought[i] = data.petBought[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        data.pet = currentPetIndex;
        
        data.petHealth = new int[petHealth.Length];
        for (int i = 0; i < petHealth.Length; i++)
        {
            data.petHealth[i] = petHealth[i].currentHealth;
        }

        for (int i = 0; i < isPetBought.Length; i++)
        {
            data.petBought[i] = isPetBought[i];
        }
    }
}