using UnityEngine;

public class WeaponManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] public GameObject[] weaponsPrefab;
    public static int currentWeaponIndex = 0;
    public static IWeapon currentWeapon;
    public static bool[] isWeaponBought = {true, false, false, false};
    

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
        currentWeapon = weaponsPrefab[currentWeaponIndex].GetComponent<IWeapon>();
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
        if(!isWeaponBought[index]) return;
        if (currentWeaponIndex == index) return;

        weaponsPrefab[currentWeaponIndex].SetActive(false);
        weaponsPrefab[index].SetActive(true);
        currentWeapon = weaponsPrefab[index].GetComponent<IWeapon>();

        WeaponNameManager.weaponName = currentWeapon.weaponName;
        NumberOfBulletsManager.numberOfBullets = currentWeapon.numberOfBullets;

        currentWeaponIndex = index;
    }

    public static void Attack()
    {
        currentWeapon.Attack();
        NumberOfBulletsManager.numberOfBullets = currentWeapon.numberOfBullets;
    }

    public static void Reload()
    {
        if (currentWeapon.weaponName == "Sword") return; // Sword doesn't have bullets
        currentWeapon.Reload();
        NumberOfBulletsManager.numberOfBullets = currentWeapon.numberOfBullets;
    }

    public void LoadData(GameData data)
    {
        currentWeaponIndex = data.weapon;

        for (int i = 0; i < isWeaponBought.Length; i++)
        {
            isWeaponBought[i] = data.weaponBought[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        data.weapon = currentWeaponIndex;

        for (int i = 0; i < isWeaponBought.Length; i++)
        {
            data.weaponBought[i] = isWeaponBought[i];
        }
    }
}
