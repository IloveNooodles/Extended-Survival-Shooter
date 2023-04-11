using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public GameObject[] weaponsPrefab;
    public int currentWeaponIndex = 0;

    public void Awake()
    {
        foreach (var weapon in weaponsPrefab)
        {
            weapon.SetActive(false);
        }
    }

    public void Start()
    {
        weaponsPrefab[currentWeaponIndex].SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);
        }
    }

    public void ChangeWeapon(int index)
    {
        if (currentWeaponIndex == index) return;

        weaponsPrefab[currentWeaponIndex].SetActive(false);
        weaponsPrefab[index].SetActive(true);
        currentWeaponIndex = index;
    }
}
