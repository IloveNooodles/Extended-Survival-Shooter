using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int golds;
    public TMP_Text goldsText;
    public ShopItemSO[] shopItems;
    public ShopItemData[] shopItemDatas;
    // public GameObject[]
    
    // public ShopTemplate shopPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemDatas.Length; i++)
        {
            if (i >= shopItems.Length)
                shopItemDatas[i].gameObject.SetActive(false);
            else
                shopItemDatas[i].gameObject.SetActive(true);
        }



        goldsText.text = golds.ToString();
        
        loadItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addGolds()
    {
        golds += 100;
        goldsText.text = golds.ToString();
        
    }

    public void loadItems()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItemDatas[i].title.text = shopItems[i].title;
            shopItemDatas[i].description.text = shopItems[i].description;
            shopItemDatas[i].cost.text = shopItems[i].cost.ToString();
            shopItemDatas[i].thumbnail.texture = shopItems[i].thumbnail;
        }
    }
}
