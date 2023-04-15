using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public int golds;
    public TMP_Text goldsText;
    public ShopItemSO[] petItems;
    public ShopItemSO[] weaponItems;

    public ShopItemData[] shopItemDatas;

    // public GameObject[]
    private GameObject petSection, weaponSection;

    TMP_Text petSectionTxt, weaponSectionTxt;
    GameObject petSectionPanel, weaponSectionPanel;

    private int currentActiveSection = 0;

    private GameObject player;


    // public ShopTemplate shopPrefabs;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        PetManager.SetAllNonActive();
    }

    void Start()
    {
        petSection = GameObject.Find("Pet Section");
        weaponSection = GameObject.Find("Weapon Section");
        petSectionTxt = petSection.GetComponentInChildren<TMP_Text>();
        weaponSectionTxt = weaponSection.GetComponentInChildren<TMP_Text>();
        petSectionPanel = petSection.transform.GetChild(1).gameObject;
        weaponSectionPanel = weaponSection.transform.GetChild(1).gameObject;


        goldsText.text = GoldManager.Gold.ToString();


        loadPetItems();
    }


    public void addGolds()
    {
        golds += 100;
        goldsText.text = golds.ToString();
        GoldManager.addGold(100);
        loadPetItems();
    }


    public void loadPetItems()
    {
        for (int i = 0; i < shopItemDatas.Length; i++)
        {
            if (i >= petItems.Length)
                shopItemDatas[i].gameObject.SetActive(false);
            else
                shopItemDatas[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < petItems.Length; i++)
        {
            shopItemDatas[i].title.text = petItems[i].title;
            shopItemDatas[i].description.text = petItems[i].description;
            shopItemDatas[i].cost.text = petItems[i].cost.ToString();
            shopItemDatas[i].thumbnail.texture = petItems[i].thumbnail;

            if (i == PetManager.currentPetIndex)
            {
                shopItemDatas[i].buyButton.gameObject.SetActive(false);
                shopItemDatas[i].equipButton.gameObject.SetActive(false);
                shopItemDatas[i].unequipButton.gameObject.SetActive(true);
                shopItemDatas[i].equippedText.gameObject.SetActive(false);
            }
            else if (PetManager.isPetBought[i])
            {
                shopItemDatas[i].buyButton.gameObject.SetActive(false);
                shopItemDatas[i].equipButton.gameObject.SetActive(true);
                shopItemDatas[i].unequipButton.gameObject.SetActive(false);
                shopItemDatas[i].equippedText.gameObject.SetActive(false);
            }
            else
            {
                shopItemDatas[i].buyButton.gameObject.SetActive(true);
                if (GoldManager.Gold < petItems[i].cost)
                {
                    shopItemDatas[i].buyButton.interactable = false;
                }
                else
                {
                    shopItemDatas[i].buyButton.interactable = true;
                }

                shopItemDatas[i].equipButton.gameObject.SetActive(false);
                shopItemDatas[i].unequipButton.gameObject.SetActive(false);
                shopItemDatas[i].equippedText.gameObject.SetActive(false);
            }
        }
    }

    public void loadWeaponItems()
    {
        for (int i = 0; i < shopItemDatas.Length; i++)
        {
            if (i >= weaponItems.Length)
                shopItemDatas[i].gameObject.SetActive(false);
            else
                shopItemDatas[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < weaponItems.Length; i++)
        {
            shopItemDatas[i].title.text = weaponItems[i].title;
            shopItemDatas[i].description.text = weaponItems[i].description;
            shopItemDatas[i].cost.text = weaponItems[i].cost.ToString();
            shopItemDatas[i].thumbnail.texture = weaponItems[i].thumbnail;

            if (i == 1)
            {
                shopItemDatas[i].buyButton.gameObject.SetActive(false);
                shopItemDatas[i].equipButton.gameObject.SetActive(false);
                shopItemDatas[i].unequipButton.gameObject.SetActive(false);
                shopItemDatas[i].equippedText.gameObject.SetActive(true);
            }
            else
            {
                shopItemDatas[i].buyButton.gameObject.SetActive(true);
                shopItemDatas[i].equipButton.gameObject.SetActive(false);
                shopItemDatas[i].unequipButton.gameObject.SetActive(false);
                shopItemDatas[i].equippedText.gameObject.SetActive(false);
            }
        }
    }

    public void onClickPetSection()
    {
        if (currentActiveSection == 0)
            return;
        petSectionTxt.color = new Color(0.9960785f, 0.7960785f, 0.1372549f);
        weaponSectionTxt.color = Color.white;
        petSectionPanel.SetActive(true);
        weaponSectionPanel.SetActive(false);
        currentActiveSection = 0;
        loadPetItems();
    }

    public void onClickWeaponSection()
    {
        if (currentActiveSection == 1)
            return;
        petSectionTxt.color = Color.white;
        weaponSectionTxt.color = new Color(0.9960785f, 0.7960785f, 0.1372549f);
        petSectionPanel.SetActive(false);
        weaponSectionPanel.SetActive(true);
        currentActiveSection = 1;
        loadWeaponItems();
    }

    public void onPointerEnterPetSection()
    {
        if (currentActiveSection == 0)
            return;
        petSectionTxt.color = new Color(0.9960785f, 0.7960785f, 0.1372549f);
    }

    public void onPointerExitPetSection()
    {
        if (currentActiveSection == 0)
            return;
        petSectionTxt.color = Color.white;
    }

    public void onPointerEnterWeaponSection()
    {
        if (currentActiveSection == 1)
            return;
        weaponSectionTxt.color = new Color(0.9960785f, 0.7960785f, 0.1372549f);
    }

    public void onPointerExitWeaponSection()
    {
        if (currentActiveSection == 1)
            return;
        weaponSectionTxt.color = Color.white;
    }

    public void CloseShopButton()
    {
        player.SetActive(true);
        int lastScene = PlayerPrefs.GetInt("lastScene");
        PlayerPrefs.DeleteKey("lastScene");
        PlayerPrefs.SetInt("isDontPlayCutScene", 1);
        SceneManager.LoadScene(lastScene);
    }

    public void BuyButton(int itemIdx)
    {
        PetManager.isPetBought[itemIdx] = true;
        PetManager.currentPetIndex = itemIdx;

        GoldManager.Gold -= petItems[itemIdx].cost;
        golds = GoldManager.Gold;
        goldsText.text = golds.ToString();

        loadPetItems();
    }

    public void EquipButton(int itemIdx)
    {
        PetManager.currentPetIndex = itemIdx;
        loadPetItems();
    }

    public void UnequipButton(int itemIdx)
    {
        PetManager.currentPetIndex = 4;
        loadPetItems();
    }
}