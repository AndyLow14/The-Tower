using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    
    public int ItemID;
    public Text PriceText;
    public Text LevelText;
    public GameObject ShopManager;
    void Update()
    {
        bool isMaxLevel = ItemID == 3 && ShopManager.GetComponent<ShopManager>().shopItems[3, ItemID] == 5;
        PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        LevelText.text = "Lv." + ShopManager.GetComponent<ShopManager>().shopItems[3, ItemID].ToString() + (isMaxLevel ? " (max)" : "");
    }
}
