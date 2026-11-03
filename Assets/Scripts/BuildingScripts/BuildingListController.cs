using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using static TileType;

public class BuildingListController : MonoBehaviour
{
    public int price;
    
    public Item resource1 = null;
    public Item resource2 = null;
    public Item resource3 = null;
    public int resource1RequiredAmmount;
    public int resource2RequiredAmmount;
    public int resource3RequiredAmmount;

    public int buildingNum;
    public Button buildNowButton;
    public Button notEnoughResourceButton;
    public Button notEnoughMoneyButton;
    public Button soldOut;
    public bool isSoldOut = false;

    public Tile[] buildingSprites;

    private MoneySystem moneySystem;
    private InventoryController inventoryController;
    private BuilderController builderController;
    
    public GameObject buildMenu;
    private void Start()
    {
        inventoryController = FindAnyObjectByType<InventoryController>();
        builderController = FindAnyObjectByType<BuilderController>();
        moneySystem = FindAnyObjectByType<MoneySystem>();
    }

    public void Build()
    {
        buildMenu.SetActive(true);
        builderController.BuildScreen(buildingNum);
        buildNowButton.gameObject.SetActive(false);
    }
    public void DiplayBuildButtons()
    {
        if (isSoldOut)
        {
            buildNowButton.gameObject.SetActive(false);
            notEnoughMoneyButton.gameObject.SetActive(false);
            notEnoughResourceButton.gameObject.SetActive(false);
        }
        else
        {
            //only 1 resource requires
            if (resource2 == null && resource3 == null)
            {
                //check enough resources are held
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(true);
                }
            }
            //two resources required
            else if (resource3 == null)
            {
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource2.GetItemID()) >= resource2RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(true);
                }
            }
            //three resources required
            else
            {
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource2.GetItemID()) >= resource2RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource3.GetItemID()) >= resource3RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(true);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(true);
                }
            }

        }

    }
    public void HidBuildButtons()
    {
        if(isSoldOut)
        {
            buildNowButton.gameObject.SetActive(false);
            notEnoughMoneyButton.gameObject.SetActive(false);
            notEnoughResourceButton.gameObject.SetActive(false);
        }
        else 
        {
            //only 1 resource requires
            if (resource2 == null && resource3 == null)
            {
                //check enough resources are held
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(false);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(false);
                }
            }
            //two resources required
            else if (resource3 == null)
            {
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource2.GetItemID()) >= resource2RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(false);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(false);
                }
            }
            //three resources required
            else
            {
                if (inventoryController.GetAmmountOfItemsInInventory(resource1.GetItemID()) >= resource1RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource2.GetItemID()) >= resource2RequiredAmmount
                    && inventoryController.GetAmmountOfItemsInInventory(resource3.GetItemID()) >= resource3RequiredAmmount)
                {
                    //check the player has enough money
                    if (moneySystem.count >= price)
                    {
                        buildNowButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        notEnoughMoneyButton.gameObject.SetActive(false);
                    }
                }
                else
                {
                    notEnoughResourceButton.gameObject.SetActive(false);
                }
            }
        }
        

    }

    public void UseItemsAndMoney()
    {
        moneySystem.decrease(price);

        if (resource2 == null && resource3 == null)
        {
            inventoryController.RemoveItemsFromInventory(resource1.GetItemID(), resource1RequiredAmmount);

        }
        else if (resource3 == null)
        {
            inventoryController.RemoveItemsFromInventory(resource1.GetItemID(), resource1RequiredAmmount); 
            inventoryController.RemoveItemsFromInventory(resource2.GetItemID(), resource2RequiredAmmount);

        }
        else 
        {
            inventoryController.RemoveItemsFromInventory(resource1.GetItemID(), resource1RequiredAmmount); 
            inventoryController.RemoveItemsFromInventory(resource2.GetItemID(), resource2RequiredAmmount); 
            inventoryController.RemoveItemsFromInventory(resource3.GetItemID(), resource3RequiredAmmount);
        }
    }

}
