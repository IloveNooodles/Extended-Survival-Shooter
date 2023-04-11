using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public GameObject[] weaponPrefabs;
    private int currentWeaponIndex = 0;

    private void Awake()
    {
        foreach (GameObject weapon in weaponPrefabs)
        {
            weapon.SetActive(false);
        }
    }

    private void Start()
    {
        weaponPrefabs[currentWeaponIndex].SetActive(true);
    }

    public void Update()
    {
        // Change weapon with number keys
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
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(3);
        }
    }

    public void ChangeWeapon(int index)
    {
        if (index == currentWeaponIndex)
        {
            return;
        }
        weaponPrefabs[currentWeaponIndex].SetActive(false);
        weaponPrefabs[index].SetActive(true);
        currentWeaponIndex = index;
    }
}
