using System;
using UnityEngine;
using UnityEngine.UI;

public class PetManager : MonoBehaviour
{
    [SerializeField] public GameObject[] petsPrefab;
    public static int currentPetIndex = 4;

    public GameObject summoningMagic;

    private static GameObject[] pets;
    public static GameObject currentPet;
    private static Slider petHeartSlider;

    private GameObject player;

    public static bool[] isPetBought;
    

    void Awake()
    {
        
        player = GameObject.FindWithTag("Player");
        petHeartSlider = GameObject.Find("PetHeartSlider").GetComponent<Slider>();
        if(pets == null || pets.Length == 0 || pets[0] == null){
            
            pets = new GameObject[petsPrefab.Length];
            isPetBought = new bool[pets.Length];
            int i = 0;
            
            petHeartSlider.gameObject.SetActive(true);
            foreach (var pet in petsPrefab)
            {
                if (pets[i] != null)
                {
                    continue;
                }
                pets[i] = Instantiate(pet);
                pets[i].tag = "Pet";
                isPetBought[i] = false;
                i++;
            }
        }
        if(currentPetIndex <= pets.Length && pets[currentPetIndex]!=null){
            placePet();

            pets[currentPetIndex].SetActive(true);
            currentPet = pets[currentPetIndex];

            // summon();
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
            petHeartSlider.gameObject.SetActive(false);   
            return;
        }
        
        if (pets[currentPetIndex].gameObject.GetComponent<PetHealth>().currentHealth <= 0)
        {
            // pet is dead
            petHeartSlider.gameObject.SetActive(false);
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
            currentPet = null;
            pets[currentPetIndex].SetActive(false);
        }

        currentPetIndex = index;
        if (index > pets.Length - 1)
        {
            petHeartSlider.gameObject.SetActive(false);
            return;
        }

        if (pets[currentPetIndex].gameObject.GetComponent<PetHealth>().currentHealth <= 0)
        {
            // pet is dead
            petHeartSlider.gameObject.SetActive(false);
            return;
        }
        
        petHeartSlider.gameObject.SetActive(true);
        
        placePet();
        pets[currentPetIndex].SetActive(true);
        currentPet = pets[currentPetIndex];
        summon();
    }
}