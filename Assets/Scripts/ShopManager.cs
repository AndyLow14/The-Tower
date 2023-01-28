using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    public int[,] shopItems = new int[5, 5];
    public float money;
    public Text MoneyText;
    public bool shopEnabled;
    public bool helpEnabled;
    public GameObject shop;
    public GameObject help;
    public GameObject[] turrets = new GameObject[5];
    public GameObject player;

    void Start()
    {
        MoneyText.text = "Money: $" + money.ToString();

        // Setting ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        // Setting Price
        shopItems[2, 1] = 100;
        shopItems[2, 2] = 100;
        shopItems[2, 3] = 500;
        shopItems[2, 4] = 0;

        // Current Level
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;

        shopEnabled = false;
        shop.SetActive(false);
        helpEnabled = true;

        turrets[0].SetActive(false);
        turrets[1].SetActive(false);
        turrets[2].SetActive(false);
        turrets[3].SetActive(false);
        turrets[4].SetActive(false);
    }

    void Update()
    {
        MoneyText.text = "Money: $" + money.ToString();
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            shopEnabled = !shopEnabled;
            shop.SetActive(shopEnabled);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            helpEnabled = !helpEnabled;
            help.SetActive(helpEnabled);
        }
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        int itemID = ButtonRef.GetComponent<ButtonInfo>().ItemID;

        // Max level reached
        if (itemID == 3 && shopItems[3, itemID] == 5)
        {
            return;
        }

        if (money >= shopItems[2, itemID])
        {
            money -= shopItems[2, itemID];

            // Increase level
            shopItems[3, itemID]++;

            // Update text
            MoneyText.text = "Money: $" + money.ToString();

            // Upgrade logic
            if (itemID == 1)
            {
                player.GetComponent<HealthController>().healthLevelUp();
            }
            else if (itemID == 2)
            {
                player.GetComponent<HealthController>().regenLevelUp();
            }
            else if (itemID == 3)
            {
                turrets[shopItems[3, itemID] - 1].SetActive(true);
            }

        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

}
